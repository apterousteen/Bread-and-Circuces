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

    void Start()
    {
        info = gameObject.GetComponent<UnitInfo>();
        mainCamera = Camera.allCameras[0];
        activated = false;
    }

    void Update()
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
                        deactivateFigure();
                    if (hit.collider.tag == "Hex")
                    {
                        var hittedTile = hit.collider.gameObject.GetComponent<HexTile>();
                        if (hittedTile.isChosen)
                            if (!hittedTile.isOccupied)
                                HandleMovement(hittedTile);
                            else HandleAttack(hittedTile);
                    }
                }
                else if (hit.collider.gameObject == this.gameObject)
                    activateFigure();
            }
        }
    }

    void HandleMovement(HexTile hittedTile)
    {
        deactivateFigure();
        MoveFigureOnObject(hittedTile);
    }

    void HandleAttack(HexTile hittedTile)
    {
        Debug.Log("Entered HandleAttack");
        var targetUnit = hittedTile.gameObject.GetComponentInChildren<UnitInfo>();
        if (targetUnit.IsEnemy(info))
        {
            Debug.Log("Attack was made");
            MakeAtack(targetUnit);
            deactivateFigure();
        }
    }

    void MoveFigureOnObject(HexTile targetHex)
    {
        posX = targetHex.transform.position.x;
        posY = targetHex.transform.position.y;
        transform.parent = targetHex.transform;
        moveObject();
    }

    void moveObject(){
        /*animation and shit*/
        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    void MakeAtack(UnitInfo enemyUnit)
    {
        var damageDealt = info.damage - enemyUnit.defence;
        enemyUnit.SufferDamage(damageDealt);
    }

    void OnMouseDown()
    {
        if (!activated)
            activateFigure();
        else
            deactivateFigure();
    }

    //called when figure being activated
    void activateFigure()
    {
        activated = true;
        var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        figureRenderer.material.SetColor("_Color", Color.yellow);

        ShowMovementArea(info.moveDistance, Color.green);
        ShowAttackArea(info.attackReachDistance, Color.red);
    }

    //called when figure being deactivated
    void deactivateFigure()
    {
        activated = false;
        var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        figureRenderer.material.SetColor("_Color", Color.white);

        HideArea(info.moveDistance);
    }

    void ShowMovementArea(int distance, Color hexColor)
    {
        var tiles = FindObjectOfType<Board>().GetTilesInRadius(transform.parent.GetComponent<HexTile>(), distance);

        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            tileRenderer.material.SetColor("_Color", hexColor);
            tile.isChosen = true;
        }
    }

    void ShowAttackArea(int distance, Color hexColor)
    {
        var tiles = FindObjectOfType<Board>().GetTilesInRadius(transform.parent.GetComponent<HexTile>(), distance);

        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            if(tile.isOccupied)
            {
                if(tile.transform.GetChild(0).GetComponent<UnitInfo>().IsEnemy(info))
                {
                    Debug.Log("Highlight enemy");
                    tileRenderer.material.SetColor("_Color", hexColor);
                    tile.isChosen = true;
                }
            }
        }
    }

    void HideArea(int distance)
    {
        var tiles = FindObjectOfType<Board>().GetTilesInRadius(transform.parent.GetComponent<HexTile>(), distance);

        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            tileRenderer.material.SetColor("_Color", Color.white);
            tile.isChosen = false;
        }
    }

}
