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
    private TurnManager turnManager;
    private DistanceFinder distanceFinder;
    public ButtonsContainer buttonsContainer;

    void Start()
    {
        board = FindObjectOfType<Board>();
        turnManager = FindObjectOfType<TurnManager>();
        distanceFinder = FindObjectOfType<DistanceFinder>();
        buttonsContainer = FindObjectOfType<ButtonsContainer>();     

        info = gameObject.GetComponent<UnitInfo>();
        mainCamera = Camera.allCameras[0];
        activated = false;
    }

    void Update()
    {
        DispathInput();
        DispathAction();
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
                    if (hit.collider.tag == "Hex")
                    {
                        var hittedTile = hit.collider.gameObject.GetComponent<HexTile>();
                        if (hittedTile.isChosen)
                        {
                            if (!hittedTile.isOccupied)
                                HandleMovement(hittedTile);
                            else 
                                HandleAttack(hittedTile);
                        }
                    }
                }
            }
        }
    }

    void DispathAction()
    {
        if(!activated)
            return;

        int action = buttonsContainer.GetAction();

        if(action == 1)
            ShowMovementArea(info.moveDistance);
        
        //if(action == -1)
        //    HideArea(info.moveDistance);

        //else if(action == 2)
        //    ShowAttackArea(info.attackReachDistance);
    }

    void HandleMovement(HexTile hittedTile)
    {
        HideArea();
        MoveFigureOnObject(hittedTile);
        buttonsContainer.EndAction();
    }

    void HandleAttack(HexTile hittedTile)
    {
        var targetUnit = hittedTile.gameObject.GetComponentInChildren<UnitInfo>();
        if (targetUnit.IsEnemy(info))
        {
            turnManager.StartReactionWindow(targetUnit.gameObject);
            HideArea();
            buttonsContainer.EndAction();
        }
    }

    void MoveFigureOnObject(HexTile targetHex)
    {
        posX = targetHex.transform.position.x;
        posY = targetHex.transform.position.y;
        var previoisHex = transform.parent.gameObject.GetComponent<HexTile>();
        transform.parent = targetHex.transform;
        previoisHex.isOccupied = false;
        MoveObject();
    }

    void MoveObject(){
        transform.position = new Vector3(posX, posY, transform.position.z);
        info.ChangeMotionType(MotionType.RadiusType);
        turnManager.inAction = false;
    }

    public void MakeAtack(UnitInfo enemyUnit)
    {
        info.OnAttackStart(enemyUnit);
        enemyUnit.OnDefenceStart();
        var damageDealt = info.damage - enemyUnit.defence;
        enemyUnit.SufferDamage(damageDealt);
        info.OnAttackEnd(enemyUnit);
        enemyUnit.OnDefenceEnd();
        turnManager.inAction = false;
    }

    public void TriggerAttack(int damage)
    {
        ShowAttackArea(info.attackReachDistance);
        info.damage += damage;
    }

    public void TriggerMove(int distance)
    {
        info.ChangeMotionType(MotionType.StraightType);
        ShowMovementArea(distance);
    }

    void ActivateFigure()
    {
        if(turnManager.ActiveUnitExist() || turnManager.currTeam != info.teamSide)
            return;

        buttonsContainer.ActivateUnitButtons();
        activated = true;
        var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        turnManager.SetActiveUnit(this.gameObject);
        figureRenderer.material.SetColor("_Color", Color.yellow);
    }

    public void DeactivateFigure()
    {
        buttonsContainer.DeactivateUnitButtons();
        turnManager.ClearActiveUnit();
        activated = false;
        var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        figureRenderer.material.SetColor("_Color", Color.white);

        HideArea();
    }

    void ShowMovementArea(int distance)
    {
        List<HexTile> tiles = new List<HexTile>();
        if(info.motionType == MotionType.RadiusType)
            tiles = distanceFinder.FindPaths(transform.parent.GetComponent<HexTile>(), distance);
        else if(info.motionType == MotionType.StraightType)
            tiles = distanceFinder.FindStraightPaths(transform.parent.GetComponent<HexTile>(), distance);

        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            tileRenderer.material.SetColor("_Color", Color.green);
            tile.isChosen = true;
        }
    }

    void ShowAttackArea(int distance)
    {
        var tiles = distanceFinder.GetTilesInRadius(transform.parent.GetComponent<HexTile>(), distance);
        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            if(tile.isOccupied)
            {
                if(tile.transform.GetChild(0).GetComponent<UnitInfo>().IsEnemy(info))
                {
                    tileRenderer.material.SetColor("_Color", Color.red);
                    tile.isChosen = true;
                }
            }
            else
            {
                tileRenderer.material.SetColor("_Color", Color.blue);
            }
        }
    }

    void HideArea()
    {
        var tiles = distanceFinder.GetTilesInRadius(transform.parent.GetComponent<HexTile>(), 9);

        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            tileRenderer.material.SetColor("_Color", Color.white);
            tile.isChosen = false;
        }
    }
}
