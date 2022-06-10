using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitControl: MonoBehaviour
{
    private float posX;
    private float posY;
    private bool activated;
    public bool wasActivated;
    private Camera mainCamera;
    private UnitInfo info;
    private Board board;
    private TurnManager turnManager;
    private DistanceFinder distanceFinder;
    private ButtonsContainer buttonsContainer;

    void Start()
    {
        board = FindObjectOfType<Board>();
        turnManager = FindObjectOfType<TurnManager>();
        distanceFinder = FindObjectOfType<DistanceFinder>();
        buttonsContainer = FindObjectOfType<ButtonsContainer>();     

        info = gameObject.GetComponent<UnitInfo>();
        mainCamera = Camera.allCameras[0];
        activated = false;
        wasActivated = false;
    }

    void Update()
    {
        DispathInput();
        //DispathAction();
    }

    void OnMouseDown()
    {
        if (!turnManager.inAction)
        {
            if (!activated && !turnManager.activatedUnits.Contains(info))
                ActivateFigure();
            else if(!turnManager.activatedUnits.Contains(info))
                DeactivateFigure();
        }
        UiController.Instance.UpdateInfoPanels(gameObject);
    }

    void DispathInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if(hit.collider != null)
            {
                if (activated && !turnManager.movingEnemy)
                {
                    if (hit.collider.tag == "Hex")
                    {
                        var hittedTile = hit.collider.gameObject.GetComponent<HexTile>();
                        if (hittedTile.isChosen)
                        {
                            if (!hittedTile.isOccupied || hittedTile.transform == transform.parent)
                                HandleMovement(hittedTile);
                            else 
                                HandleAttack(hittedTile);
                        }
                    }
                }
                else if(!activated && turnManager.movingEnemy && 
                    turnManager.targetUnit != null && turnManager.targetUnit == gameObject)
                {
                    var hittedTile = hit.collider.gameObject.GetComponent<HexTile>();
                    if (hittedTile.isChosen)
                    {
                        if (!hittedTile.isOccupied)
                            HandleMovement(hittedTile);
                    }
                }
            }
        }
    }

    void DispathAction()
    {
        if(!activated)
            return;

        //int action = buttonsContainer.GetAction();

        //if(action == 1)
        //    ShowMovementArea(info.moveDistance);
        
        //if(action == -1)
        //    HideArea(info.moveDistance);

        //else if(action == 2)
        //    ShowAttackArea(info.attackReachDistance);
    }

    void HandleMovement(HexTile hittedTile)
    {
        HideArea();
        MoveFigureOnObject(hittedTile);
        //buttonsContainer.EndAction();
    }

    void HandleAttack(HexTile hittedTile)
    {
        var targetUnit = hittedTile.gameObject.GetComponentInChildren<UnitInfo>();
        if (targetUnit.IsEnemy(info))
        {
            turnManager.StartReactionWindow(targetUnit.gameObject);
            HideArea();
            //buttonsContainer.EndAction();
        }
    }

    public void MoveFigureOnObject(HexTile targetHex)
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
        info.OnMoveEnd();
        info.ChangeMotionType(MotionType.RadiusType);
        turnManager.EndAction();
    }

    public void MakeAtack(UnitInfo enemyUnit)
    {
        info.OnAttackStart(enemyUnit);
        enemyUnit.OnDefenceStart();
        Debug.Log("DMG = " + info.damage);
        Debug.Log("DEF = " + enemyUnit.defence);
        var damageDealt = info.damage - enemyUnit.defence;
        if (damageDealt < 0)
            damageDealt = 0;
        enemyUnit.SufferDamage(damageDealt);
        info.OnAttackEnd(enemyUnit);
        enemyUnit.OnDefenceEnd();
        turnManager.EndAction();
    }

    public void TriggerAttack(int damage)
    {
        ShowAttackArea(info.attackReachDistance);
        info.damage += damage;
    }

    public void TriggerMove(int distance)
    {
        ShowMovementArea(distance);
    }

    public void ActivateFigure()
    {
        if(turnManager.ActiveUnitExist() || turnManager.currTeam != info.teamSide)
            return;

        if(info.teamSide == Team.Player)
            FindObjectOfType<GameManagerScript>().ShowPlayableCards(Card.CardType.Attack, info);
        buttonsContainer.ActivateUnitButtons();
        activated = true;
        //var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        turnManager.SetActiveUnit(this.gameObject);
        //figureRenderer.material.SetColor("_Color", Color.yellow);
        var hexToColor = gameObject.transform.parent.GetComponent<HexTile>().GetComponent<SpriteRenderer>();
        hexToColor.material.SetColor("_Color", Color.grey);
    }

    public void DeactivateFigure()
    {
        buttonsContainer.DeactivateUnitButtons();
        turnManager.ClearActiveUnit();
        activated = false;
        //var figureRenderer = gameObject.GetComponent<SpriteRenderer>();
        //figureRenderer.material.SetColor("_Color", Color.white);

        HideArea();
    }

    void ShowMovementArea(int distance)
    {
        List<HexTile> tiles = new List<HexTile>();
        if (info.OnMoveStart())
            distance++;
        if (info.motionType == MotionType.RadiusType)
            tiles = distanceFinder.FindPaths(transform.parent.GetComponent<HexTile>(), distance);
        else if(info.motionType == MotionType.StraightType)
            tiles = distanceFinder.FindStraightPaths(transform.parent.GetComponent<HexTile>(), distance);

        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            tileRenderer.material.SetColor("_Color", Color.gray);
            tile.isChosen = true;
        }
    }

    void ShowAttackArea(int distance)
    {
        var tiles = distanceFinder.GetTilesInRadius(transform.parent.GetComponent<HexTile>(), distance);
        int foundEnemies = 0;
        foreach (var tile in tiles)
        {
            var tileRenderer = tile.gameObject.GetComponent<SpriteRenderer>();
            if(tile.isOccupied)
            {
                if(tile.transform.GetChild(0).GetComponent<UnitInfo>().IsEnemy(info))
                {
                    tileRenderer.material.SetColor("_Color", Color.black);
                    tile.isChosen = true;
                    foundEnemies++;
                }
            }
            else
            {
                tileRenderer.material.SetColor("_Color", Color.red);
            }
        }
        if(foundEnemies == 0)
        {
            HideArea();
            turnManager.EndAction();
        }
    }

    public bool CheckForEnemiesInBTB()
    {
        var tiles = distanceFinder.GetTilesInRadius(transform.parent.GetComponent<HexTile>(), 1);
        foreach (var tile in tiles)
        {
            if (tile.isOccupied)
            {
                if (tile.transform.GetChild(0).GetComponent<UnitInfo>().IsEnemy(info))
                {
                    return true;
                }
            }
        }
        return false;
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

    public void HighlighParenttHex()
    {
        HideArea();
        var hexToColor = gameObject.transform.parent.GetComponent<HexTile>().GetComponent<SpriteRenderer>();
        hexToColor.material.SetColor("_Color", Color.grey);
    }
}
