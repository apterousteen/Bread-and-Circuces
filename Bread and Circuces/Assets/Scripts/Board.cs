using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject gridObject;
    public GameObject Unit;

    public int gridSizeX = 10;
    public int gridSizeY = 10;

    private float dx = 0.86f;
    private float dy = 0.74f;

    public HexTile[][] board;

    void Start()
    {
        board = new HexTile[gridSizeX][];
        var spawnStartPosition = transform.position;
        for (int i = 0; i < gridSizeX; ++i)
        {
            var tempArr = new HexTile[gridSizeY];
            for (int j = 0; j < gridSizeY; ++j)
            {
                var xcomponent = dx * i;
                int diff = 0;
                if (j % 2 != 0) //continue;
                {
                    xcomponent = (dx * i) + 0.43f;
                    diff = 1;
                }

                var ycomponent = -dy * j;

                var newTile = Instantiate(gridObject, spawnStartPosition
                                        + new Vector3(xcomponent, ycomponent, 0), gridObject.transform.rotation);
                tempArr[j] = newTile.GetComponent<HexTile>();
                newTile.GetComponent<HexTile>().gridX = 2 * i + diff;
                newTile.GetComponent<HexTile>().gridY = j;
                newTile.transform.parent = this.gameObject.transform;
                board[i] = tempArr;
            }
        }
        var startPos = board[4][6];
        var newUnit = Instantiate(Unit, startPos.transform.position, gridObject.transform.rotation);
        newUnit.transform.parent = startPos.gameObject.transform;
    }

    //public List<HexTile> GetTilesInRadius(HexTile tile, int distance)
    //{
    //    var result = new List<HexTile>();
    //    int n = board.IndexOf(tile);
    //    int a = gridSizeX;
    //    int b = gridSizeY;
    //    if(n / gridSizeX % 2 != 0)
    //    {
    //        a++;
    //        b--;
    //    }
    //    for(int i = -distance; i <= distance; i++)
    //    {
    //        var num1 = n + i;
    //        if (num1 >= 0 && num1 < board.Count)
    //            result.Add(board[num1]);
    //    }
    //    for (int i = 1; i <= distance; i++)
    //    {
    //        for(int j = i - distance; j <= distance; j++)
    //        {
    //            var dif = i / 2;
    //            if(n / gridSizeX % 2 == 0)
    //                dif *= -1;
    //            var num1 = n - i * a + j + dif;
    //            var num2 = n + i * b + j + dif;
    //            if(num1 >= 0 && num1 < board.Count)
    //                result.Add(board[num1]);
    //            if (num2 >= 0 && num2 < board.Count)
    //                result.Add(board[num2]);
    //        }
    //    }
    //    return result;
    //}


    //int ComputeDistanceHexGrid(Vector2 tileA, Vector2 tileB)
    //{
    //    var distance = new Vector2(tileA.x - tileB.x, tileA.y - tileB.y);
    //    var diagonalMovement = new Vector2();
    //    var lesserCoordinate = Mathf.Abs(distance.x) < Mathf.Abs(distance.y) ? Mathf.Abs(distance.x) : Mathf.Abs(distance.y);
    //    diagonalMovement.x = (distance.x < 0) ? -lesserCoordinate : lesserCoordinate;
    //    diagonalMovement.y = (distance.y < 0) ? -lesserCoordinate : lesserCoordinate;
    //    var straightMovement = new Vector2(distance.x - diagonalMovement.x, distance.y - diagonalMovement.y);

    //    var straightDistance = Mathf.Abs(straightMovement.x) + Mathf.Abs(straightMovement.y);
    //    var diagonalDistance = Mathf.Abs(diagonalMovement.x);

    //    if ((diagonalMovement.x < 0 && diagonalMovement.y > 0) ||
    //   (diagonalMovement.x > 0 && diagonalMovement.y < 0))
    //    {
    //        diagonalDistance *= 2;
    //    }

    //    return (int)(straightDistance + diagonalDistance);
    //}

    int ComputeDistanceHexGrid(Vector2 tileA, Vector2 tileB)
    {
        var xDistance = Mathf.Abs(tileA.x - tileB.x);
        var yDistance = Mathf.Abs(tileA.y - tileB.y);
        return (int)(yDistance + Mathf.Max(0, (xDistance - yDistance) / 2));
    }

    public List<HexTile> GetTilesInRadius(HexTile tile, int distance)
    {
        var result = new List<HexTile>();

        var selectionCenter = new Vector2(tile.gridX, tile.gridY);
        var pointOnBoard = new Vector2(tile.gridX / 2, tile.gridY);

        for (var x = pointOnBoard.x - distance;
                  x <= pointOnBoard.x + distance;
                  x++)
        {
            for (var y = pointOnBoard.y - distance;
                      y <= pointOnBoard.y + distance;
                      y++)
            {
                if (x < 0 || y < 0 || x >= gridSizeX || y >= gridSizeY)
                    continue;
                var hex = board[(int)x][(int)y];
                var p = new Vector2(hex.gridX, hex.gridY);
                if (ComputeDistanceHexGrid(selectionCenter, p) <= distance)
                    result.Add(hex);
            }
        }
        return result;
    }

    void Update()
    {
        
    }
}
