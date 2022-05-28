using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DistanceFinder : MonoBehaviour
{
    private Vector2[] directionVectors;
    private Board board;

    private void Start()
    {
        board = board = FindObjectOfType<Board>();
        directionVectors = new Vector2[6]{new Vector2(2,0), new Vector2(1, -1), new Vector2(-1, -1),
        new Vector2(-2, 0), new Vector2(-1, 1), new Vector2(1, 1)};
    }

    private HexTile GetNeighbor(HexTile hex, int direction)
    {
        var vector = new Vector2(hex.gridX, hex.gridY) + directionVectors[direction];
        if (vector.x < 0 || vector.y < 0
            || vector.x >= board.gridSizeX * 2 || vector.y >= board.gridSizeY)
            return null;
        return board.board[(int)(vector.x / 2)][(int)vector.y];
    }

    public List<HexTile> FindPaths(HexTile start, int distance)
    {
        var visited = new List<HexTile>();
        var fringes = new List<HexTile[]>();
        var startFringe = new List<HexTile>();
        fringes.Add(new HexTile[1]{start});
        for(int i = 1; i <= distance; i++)
        {
            var newFringe = new List<HexTile>();
            foreach(var hex in fringes[i - 1])
                for(int j = 0; j < 6; j++)
                {
                    var neighbor = GetNeighbor(hex, j);
                    if(neighbor != null)
                    {
                        if (!neighbor.isOccupied
                        && !visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            newFringe.Add(neighbor);
                        }
                    }
                }
            var arr = newFringe.ToArray();
            fringes.Add(arr);
        }
        return visited;
    }

    public List<HexTile> FindStraightPaths(HexTile start, int distance)
    {
        var result = new List<HexTile>();
        result.Add(start);
        for(int i = 0; i < 6; i++)
        {
            var hex = start;
            var count = 0;
            do
            {
                var neighbor = GetNeighbor(hex, i);
                if (neighbor == null || neighbor.isOccupied)
                    break;
                count++;
                result.Add(neighbor);
                hex = neighbor;
            }
            while (count < distance);
        }
        return result;
    }

    int ComputeDistanceHexGrid(Vector2 tileA, Vector2 tileB)
    {
        var xDistance = Mathf.Abs(tileA.x - tileB.x);
        var yDistance = Mathf.Abs(tileA.y - tileB.y);
        return (int)(yDistance + Mathf.Max(0, (xDistance - yDistance) / 2));
    }

    public int GetDistanceBetweenHexes(HexTile tileA, HexTile tileB)
    {
        var vectorA = new Vector2(tileA.gridX, tileA.gridY);
        var vectorB = new Vector2(tileB.gridX, tileB.gridY);
        return ComputeDistanceHexGrid(vectorA, vectorB);
    }

    public List<HexTile> GetTilesInRadius(HexTile tile, int distance)
    {
        var result = new List<HexTile>();
        result.Add(tile);
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
                if (x < 0 || y < 0 || x >= board.gridSizeX || y >= board.gridSizeY)
                    continue;
                var hex = board.board[(int)x][(int)y];
                var p = new Vector2(hex.gridX, hex.gridY);
                if (ComputeDistanceHexGrid(selectionCenter, p) <= distance)
                    result.Add(hex);
            }
        }
        result.Remove(tile);
        return result;
    }
}
