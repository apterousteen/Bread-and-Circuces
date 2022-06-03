using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

class SpawnPoint
{
    public Vector2 point;
    public bool occupied;

    public SpawnPoint(int x, int y)
    {
        point = new Vector2(x, y);
        occupied = false;
    }
}

public class Board : MonoBehaviour
{
    public GameObject gridObject;
    public List<GameObject> Units;

    public int gridSizeX = 10;
    public int gridSizeY = 7;

    private float dx = 0.86f;
    private float dy = 0.74f;

    public HexTile[][] board;

    private List<SpawnPoint> spawnPoints;

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

        spawnPoints = new List<SpawnPoint>();
        spawnPoints.Add(new SpawnPoint(1, 1));
        spawnPoints.Add(new SpawnPoint(1, 5));
        spawnPoints.Add(new SpawnPoint(8, 1));
        spawnPoints.Add(new SpawnPoint(8, 5));
    }

    public void SpawnUnits(Player player)
    {
        foreach(var unitTag in player.units.units)
        {
            var unit = Units.Where(x => x.tag == unitTag).First();
            var spawnPoint = spawnPoints.Where(x => !x.occupied).First();
            spawnPoint.occupied = true;
            SpawnUnit(unit, spawnPoint.point, player.team);
        }
        player.units.unitsAlive = 2;
    }

    void SpawnUnit(GameObject unit, Vector2 coordinates, Team team)
    {
        var startPos = board[(int)coordinates.x][(int)coordinates.y];
        var newUnit = Instantiate(unit, startPos.transform.position, gridObject.transform.rotation);
        newUnit.GetComponent<UnitInfo>().teamSide = team;
        newUnit.transform.parent = startPos.gameObject.transform;

        if (newUnit.GetComponent<UnitInfo>().teamSide == Team.Enemy)
        {
            newUnit.transform.localRotation = Quaternion.Euler(0, 180, 0);
            newUnit.GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<UnitInfo>().altSkin;
        }
    }
}
