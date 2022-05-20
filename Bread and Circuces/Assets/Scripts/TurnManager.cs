using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ActionType
{
    Attack,
    Move,
    Push,
    Draw,
    DiscardActivePlayer,
    DiscardOpponent
}

public class Action
{
    public ActionType type;
    public int value;

    public Action(ActionType type, int value)
    {
        this.type = type;
        this.value = value;
    }
}

public class TurnManager : MonoBehaviour
{
    public Team currTeam;
    private Team teamWithInitiative;
    private GameManagerScript gameManager;
    public bool isReactionTime;

    public GameObject activeUnit = null;
    public GameObject targetUnit = null;

    public List<UnitInfo> activatedUnits;

    public int Turn = 0, activationNum = 0;
    int TurnTime = 60, stoppedTurnTime = 0;

    public Queue<Action> actionQueue;
    public bool inAction;

    void Start()
    {
        gameManager = FindObjectOfType<GameManagerScript>();
        teamWithInitiative = (Team)Random.Range(0, 1);
        currTeam = teamWithInitiative;
        isReactionTime = false;
        inAction = false;
        actionQueue = new Queue<Action>();
        activatedUnits = new List<UnitInfo>();
        gameManager.MakeAllCardsUnplayable();
        StartCoroutine(TurnFunc());
    }

    void Update()
    {
        if (actionQueue.Count != 0 && !inAction)
            ResolveAction();
    }

    public void AddAction(Action action)
    {
        actionQueue.Enqueue(action);
    }

    public void ResolveAction()
    {
        if(!activatedUnits.Contains(activeUnit.GetComponent<UnitInfo>()))
            activatedUnits.Add(activeUnit.GetComponent<UnitInfo>());
        inAction = true;
        var action = actionQueue.Dequeue();
        switch(action.type)
        {
            case ActionType.Attack:
                activeUnit.GetComponent<UnitControl>().TriggerAttack(action.value);
                break;
            case ActionType.Move:
                activeUnit.GetComponent<UnitInfo>().ChangeMotionType(MotionType.RadiusType);
                activeUnit.GetComponent<UnitControl>().TriggerMove(action.value);
                break;
            case ActionType.Push:
                activeUnit.GetComponent<UnitInfo>().ChangeMotionType(MotionType.StraightType);
                activeUnit.GetComponent<UnitControl>().TriggerMove(action.value);
                break;
            case ActionType.Draw:
                gameManager.DrawCards(currTeam, action.value);
                break;
            case ActionType.DiscardOpponent:
                if (currTeam == Team.Enemy)
                {
                    Debug.Log("Start discard");
                    UiController.Instance.MakeDiscardWindowActive(true);
                    var discardWindow = FindObjectOfType<DiscardWindow>();
                    discardWindow.SetNum(action.value);
                    discardWindow.SetCards();
                }
                else gameManager.enemyHandSize -= action.value;
                break;
            case ActionType.DiscardActivePlayer:
                if (currTeam == Team.Player)
                {
                    UiController.Instance.MakeDiscardWindowActive(true);
                    var discardWindow = FindObjectOfType<DiscardWindow>();
                    discardWindow.SetCards();
                    discardWindow.SetNum(action.value);
                }
                else gameManager.enemyHandSize -= action.value;
                break;
        }
    }

    public void EndAction()
    {
        inAction = false;
        if (activeUnit != null && currTeam == Team.Player)
            gameManager.ShowPlayableCards(Card.CardType.Attack, activeUnit.GetComponent<UnitInfo>());
    }

    public Team GetCurrTeam()
    {
        return currTeam;
    }

    public void SetCurrTeam(Team team)
    {
        currTeam = team;
    }

    public IEnumerator TurnFunc()
    {
        if (stoppedTurnTime != 0)
            TurnTime = System.Math.Min(60, stoppedTurnTime + 10);
        else TurnTime = 60;
        stoppedTurnTime = 0;

        UiController.Instance.UpdateTurnTime(TurnTime);

        gameManager.CheckCardsForManaAvaliability();

        if (gameManager.IsPlayerTurn)
        {
            while (TurnTime-- > 0)
            {
                UiController.Instance.UpdateTurnTime(TurnTime);
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            while (TurnTime-- > 0)
            {
                UiController.Instance.UpdateTurnTime(TurnTime);
                yield return new WaitForSeconds(1);
            }
        }
        EndPlayerActivation();
    }

    bool CheckIfPlayerCanActivate(Player player)
    {
        return player.activatedUnits < 2 && player.Mana != 0;
    }

    public void EndPlayerActivation()
    {
        activationNum++;
        StopAllCoroutines();

        UiController.Instance.DisableTurnBtn();

        if (currTeam == Team.Player)
        {
            gameManager.CurrentGame.Player.activatedUnits++;
            gameManager.MakeAllCardsUnplayable();
        }
        else if (currTeam == Team.Enemy)
        {
            gameManager.CurrentGame.Enemy.activatedUnits++;
        }

        var playerCanActivate = CheckIfPlayerCanActivate(gameManager.CurrentGame.Player);
        var enemyCanActivate = CheckIfPlayerCanActivate(gameManager.CurrentGame.Enemy);

        //activatedUnits.Add(activeUnit.GetComponent<UnitInfo>());
        activeUnit.GetComponent<UnitControl>().DeactivateFigure();

        if (enemyCanActivate && (currTeam == Team.Player || !playerCanActivate))
        {
            currTeam = Team.Enemy;
        }
        else if (playerCanActivate && (currTeam == Team.Enemy || !enemyCanActivate))
        {
            currTeam = Team.Player;
        }

        if (playerCanActivate || enemyCanActivate)
        {
            StartCoroutine(TurnFunc());
        }
        else ChangeTurn();
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;
        activationNum = 0;
        currTeam = teamWithInitiative;

        UiController.Instance.DisableTurnBtn();

        activatedUnits.Clear();
        gameManager.GiveNewCards();
        gameManager.CurrentGame.Player.RestoreRoundMana();
        gameManager.CurrentGame.Player.activatedUnits = 0;
        gameManager.CurrentGame.Enemy.RestoreRoundMana();
        gameManager.CurrentGame.Enemy.activatedUnits = 0;

        UiController.Instance.UpdateMana();

        StartCoroutine(TurnFunc());
    }

    public void StartReactionWindow(GameObject target)
    {
        StopAllCoroutines();
        var activeUnitInfo = activeUnit.GetComponent<UnitInfo>();
        var targetInfo = target.GetComponent<UnitInfo>();
        isReactionTime = true;
        targetUnit = target;
        UiController.Instance.ChangeEndButtonText();
        if (activeUnitInfo.teamSide == Team.Player)
        {
            targetInfo.defence += Random.Range(0, 4);
            //targetInfo.gameObject.GetComponent<BasicUnitUI>().GenerateDefence();
            Debug.Log("Target's defence: " + targetInfo.defence);
            EndReactionWindow();
        }
        else
        {
            stoppedTurnTime = TurnTime;
            StartCoroutine(ReactionFunc());
        }
    }

    public void EndReactionWindow()
    {
        StopAllCoroutines();
        activeUnit.GetComponent<UnitControl>().MakeAtack(targetUnit.GetComponent<UnitInfo>());
        isReactionTime = false;
        targetUnit = null;
        if (currTeam == Team.Enemy)
            gameManager.MakeAllCardsUnplayable();
        UiController.Instance.ChangeEndButtonText();
        StartCoroutine(TurnFunc());
    }

    public IEnumerator ReactionFunc()
    {
        TurnTime = 30;

        UiController.Instance.UpdateTurnTime(TurnTime);

        gameManager.ShowPlayableCards(Card.CardType.Defense, targetUnit.GetComponent<UnitInfo>());

        while (TurnTime-- > 0)
        {
            UiController.Instance.UpdateTurnTime(TurnTime);
            yield return new WaitForSeconds(1);
        }

        EndReactionWindow();
    }

    public void StopTurnCoroutines()
    {
        stoppedTurnTime = TurnTime;
        StopAllCoroutines();
    }

    public void ContinueTurnCoroutine()
    {
        StartCoroutine(TurnFunc());
    }

    public void HandleEndTurnButton()
    {
        if (isReactionTime)
            EndReactionWindow();
        else EndPlayerActivation();
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
