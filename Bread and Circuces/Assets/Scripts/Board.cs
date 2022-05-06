using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject gridObject;
    public GameObject Unit;
    public GameObject enemyUnit;

    public int gridSizeX = 10;
    public int gridSizeY = 10;

    private float dx = 0.86f;
    private float dy = 0.74f;

    private Team currTeam = Team.Player;

    private GameObject activeUnit = null;

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
        SpawnUnit(Unit, new Vector2(3, 5));
        SpawnUnit(enemyUnit, new Vector2(3, 6));
        SpawnUnit(Unit, new Vector2(1, 5));
        SpawnUnit(enemyUnit, new Vector2(1, 6));
        SpawnUnit(Unit, new Vector2(1, 3));
        SpawnUnit(enemyUnit, new Vector2(2, 3));
    }

    void SpawnUnit(GameObject unit, Vector2 coordinates)
    {
        var startPos = board[(int)coordinates.x][(int)coordinates.y];
        var newUnit = Instantiate(unit, startPos.transform.position, gridObject.transform.rotation);
        newUnit.transform.parent = startPos.gameObject.transform;
    }

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
        result.Remove(tile);
        return result;
    }

    public Team GetCurrTeam()
    {
        return currTeam;
    }

    public void SetCurrTeam(Team team)
    {
        currTeam = team;
    }

    public void SwitchPlayerTurn()
    {
        if(currTeam == Team.Player)
            currTeam = Team.Enemy;
        else if(currTeam == Team.Enemy)
            currTeam = Team.Player;
    }

    public bool ActiveUnitExist()
    {
        return activeUnit != null;
    }

    public void ClearActiveUnit()
    {
        activeUnit = null;
    }

    public void SetActiveUnit(GameObject unit)
    {
        activeUnit = unit;
    }

    public GameObject GetActiveUnit()
    {
        return activeUnit;
    }

    void Update()
    {
        
    }
}
