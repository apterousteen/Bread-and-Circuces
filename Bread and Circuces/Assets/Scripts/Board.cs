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

    public Team currTeam = Team.Player;

    public GameObject activeUnit = null;

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
    }

    void SpawnUnit(GameObject unit, Vector2 coordinates)
    {
        var startPos = board[(int)coordinates.x][(int)coordinates.y];
        var newUnit = Instantiate(unit, startPos.transform.position, gridObject.transform.rotation);
        newUnit.transform.parent = startPos.gameObject.transform;
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
}
