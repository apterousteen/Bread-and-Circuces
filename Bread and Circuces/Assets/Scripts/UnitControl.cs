using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl: MonoBehaviour
{
    private float posX;
    private float posY;
    private bool activated;
    private Camera mainCamera;
    private UnitInfo info;
    private Board board;
    private DistanceFinder distanceFinder;

    void Start()
    {
        board = FindObjectOfType<Board>();
        distanceFinder = FindObjectOfType<DistanceFinder>();
        info = gameObject.GetComponent<UnitInfo>();
        mainCamera = Camera.allCameras[0];
        activated = false;
    }

    void Update()
    {
        DispathInput();
    }

    void OnMouseDown()
    {
        if (!activated)
            ActivateFigure();
        else
            DeactivateFigure();
    }

    void DispathInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);
            if(hit.collider != null)
            {
                if (activated)
                {
                    if (hit.collider.gameObject == this.gameObject)
                        DeactivateFigure();
                    if (hit.collider.tag == "Hex")
                    {
                        var hittedTile = hit.collider.gameObject.GetComponent<HexTile>();
                        if (hittedTile.isChosen)
                        {
                            if (!hittedTile.isOccupied)
                                HandleMovement(hittedTile);
                            else 
                                HandleAttack(hittedTile);
                            board.SwitchPlayerTurn();
                        }
                    }
                }
            }
        }
    }

    void HandleMovement(HexTile hittedTile)
    {
        DeactivateFigure();
        MoveFigureOnObject(hittedTile);
    }

    void HandleAttack(HexTile hittedTile)
    {
        var targetUnit = hittedTile.gameObject.GetComponentInChildren<UnitInfo>();
        if (targetUnit.IsEnemy(info))
        {
            MakeAtack(targetUnit);
            DeactivateFigure();
        }
    }

    void MoveFigureOnObject(HexTile targetHex)
    {
        posX = targetHex.transform.position.x;
        posY = targetHex.transform.position.y;
        transform.parent = targetHex.transform;
        MoveObject();
    }

    void MoveObject(){
        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    void MakeAtack(UnitInfo enemyUnit)
    {
        var damageDealt = info.damage - enemyUnit.defence;
        enemyUnit.SufferDamage(damageDealt);
    }

    void ActivateFigure()
    {
        if(board.ActiveUnitExist() || board.currTeam != info.teamSide)
            return;
        activated = true;
        var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        board.SetActiveUnit(this.gameObject);
        figureRenderer.material.SetColor("_Color", Color.yellow);

        ShowMovementArea(info.moveDistance, Color.green);
        ShowAttackArea(info.attackReachDistance, Color.red);
    }

    void DeactivateFigure()
    {
        board.ClearActiveUnit();
        activated = false;
        var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        figureRenderer.material.SetColor("_Color", Color.white);

        HideArea(info.moveDistance);
    }

    void ShowMovementArea(int distance, Color hexColor)
    {
        var tiles = distanceFinder.FindPaths(transform.parent.GetComponent<HexTile>(), distance);
        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            tileRenderer.material.SetColor("_Color", hexColor);
            tile.isChosen = true;
        }
    }

    void ShowAttackArea(int distance, Color hexColor)
    {
        var tiles = distanceFinder.GetTilesInRadius(transform.parent.GetComponent<HexTile>(), distance);
        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            if(tile.isOccupied)
            {
                if(tile.transform.GetChild(0).GetComponent<UnitInfo>().IsEnemy(info))
                {
                    tileRenderer.material.SetColor("_Color", hexColor);
                    tile.isChosen = true;
                }
            }
        }
    }

    void HideArea(int distance)
    {
        var tiles = distanceFinder.GetTilesInRadius(transform.parent.GetComponent<HexTile>(), distance);

        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            tileRenderer.material.SetColor("_Color", Color.white);
            tile.isChosen = false;
        }
    }
}
