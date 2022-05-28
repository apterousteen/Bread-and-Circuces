using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ActionType
{
    Attack,
    FinisherAttack,
    AttackWithDiscardBuff,
    Move,
    Push,
    PushEnemy,
    Draw,
    DrawDiscardedCards,
    DiscardActivePlayer,
    DiscardForMove,
    DiscardOpponent,
    NearDraw,
    CancelCard,
    ChargeStart,
    ChargeEnd,
    ChangeEnemyStance,
    DrawIfAlive,
    Skip
}

public class Action
{
    public ActionType type;
    public Team team;
    public int value;

    public Action(ActionType type, Team team, int value)
    {
        this.type = type;
        this.value = value;
        this.team = team;
    }
}

public class TurnManager : MonoBehaviour
{

    public Team currTeam;
    private Team teamWithInitiative;
    private GameManagerScript gameManager;
    private DistanceFinder distanceFinder;
    private ButtonsContainer buttonsContainer;
    public bool isReactionTime;

    public GameObject activeUnit = null;
    public GameObject targetUnit = null;

    public List<UnitInfo> activatedUnits;

    public int Turn = 0, activationNum = 0;
    int TurnTime = 60, stoppedTurnTime = 0;

    public Queue<Action> actionQueue;
    public bool inAction;
    public bool movingEnemy = false;
    public bool defCardPlayed;
    public int playedCards = 0;
    private HexTile startHex;

    public int actionsLeft;

    public void StartActivity()
    {
        gameManager = FindObjectOfType<GameManagerScript>();
        distanceFinder = FindObjectOfType<DistanceFinder>();
        buttonsContainer = FindObjectOfType<ButtonsContainer>();
        teamWithInitiative = (Team)Random.Range(0, 2);
        currTeam = teamWithInitiative;
        isReactionTime = false;
        inAction = false;
        defCardPlayed = false;
        actionQueue = new Queue<Action>();
        activatedUnits = new List<UnitInfo>();
        gameManager.MakeAllCardsUnplayable();
        StartCoroutine(TurnFunc());
        StartMulligan();
    }

    void Update()
    {
        actionsLeft = actionQueue.Count;
        if (actionQueue.Count != 0 && (!inAction || isReactionTime))
            ResolveAction();
    }

    public void StartMulligan()
    {
        AddAction(new Action(ActionType.DiscardActivePlayer, Team.Player, gameManager.StartHandSize));
        AddAction(new Action(ActionType.DrawDiscardedCards, Team.Player, 0));
    }    

    public void AddAction(Action action)
    {
        if (isReactionTime && action.type != ActionType.DrawIfAlive)
        {
            //defCardPlayed = true;
            var temp = new Queue<Action>();
            for (int i = 0; i < actionQueue.Count; i++)
                temp.Enqueue(actionQueue.Dequeue());
            actionQueue.Enqueue(action);
            for (int i = 0; i < temp.Count; i++)
                actionQueue.Enqueue(temp.Dequeue());
        }
        else actionQueue.Enqueue(action);
    }

    public void ResolveAction()
    {
        if(activeUnit != null && !activatedUnits.Contains(activeUnit.GetComponent<UnitInfo>()))
            activatedUnits.Add(activeUnit.GetComponent<UnitInfo>());
        inAction = true;
        var action = actionQueue.Dequeue();
        switch(action.type)
        {
            case ActionType.Attack:
                if (action.team == Team.Player)
                    activeUnit.GetComponent<UnitControl>().TriggerAttack(action.value);
                else
                {
                    activeUnit.GetComponent<UnitInfo>().damage += action.value;
                    StartReactionWindow(targetUnit);
                }
                break;

            case ActionType.Move:
                activeUnit.GetComponent<UnitInfo>().ChangeMotionType(MotionType.RadiusType);
                activeUnit.GetComponent<UnitControl>().TriggerMove(action.value);
                break;

            case ActionType.Push:
                var unitToMove = activeUnit;
                if (isReactionTime)
                    unitToMove = targetUnit;
                unitToMove.GetComponent<UnitInfo>().ChangeMotionType(MotionType.StraightType);
                if (action.team == Team.Player)
                    unitToMove.GetComponent<UnitControl>().TriggerMove(action.value);
                else if (isReactionTime)
                    unitToMove.GetComponent<BasicUnitAI>().MoveAwayFromClosestPlayerUnit(action.value);
                else unitToMove.GetComponent<BasicUnitAI>().MoveToClosestPlayerUnit(action.value);
                break;

            case ActionType.Draw:
                gameManager.DrawCards(action.team, action.value);
                break;

            case ActionType.DiscardOpponent:
                if (action.team == Team.Enemy)
                {
                    UiController.Instance.MakeDiscardWindowActive(true);
                    var discardWindow = FindObjectOfType<DiscardWindow>();
                    discardWindow.SetParams(action.value);
                    discardWindow.SetCards();
                }
                else gameManager.enemyHandSize -= action.value;
                break;

            case ActionType.DiscardActivePlayer:
                if (action.team == Team.Player)
                {
                    UiController.Instance.MakeDiscardWindowActive(true);
                    var discardWindow = FindObjectOfType<DiscardWindow>();
                    discardWindow.SetCards();
                    discardWindow.SetParams(action.value, true);
                }
                else gameManager.enemyHandSize -= 1;
                break;

            case ActionType.DiscardForMove:
                if (action.team == Team.Player)
                {
                    UiController.Instance.MakeDiscardWindowActive(true);
                    var discardWindow = FindObjectOfType<DiscardWindow>();
                    discardWindow.SetCards();
                    discardWindow.SetParams(action.value, false);
                }
                break;

            case ActionType.DrawDiscardedCards:
                gameManager.DrawCards(action.team, gameManager.discardedCards);
                break;

            case ActionType.DrawIfAlive:
                if (targetUnit != null)
                    gameManager.DrawCards(action.team, action.value);
                else EndAction();
                break;

            case ActionType.AttackWithDiscardBuff:
                if (action.team == Team.Player)
                    activeUnit.GetComponent<UnitControl>().TriggerAttack(action.value + gameManager.discardedCards * 2);
                else
                {
                    activeUnit.GetComponent<UnitInfo>().damage += action.value + 2;
                    StartReactionWindow(targetUnit);
                }
                break;

            case ActionType.FinisherAttack:
                if (action.team == Team.Player)
                    activeUnit.GetComponent<UnitControl>().TriggerAttack(action.value + playedCards);
                else
                {
                    activeUnit.GetComponent<UnitInfo>().damage += action.value + playedCards;
                    StartReactionWindow(targetUnit);
                }
                break;

            case ActionType.CancelCard:
                actionQueue.Clear();
                EndAction();
                break;

            case ActionType.ChangeEnemyStance:
                targetUnit.GetComponent<UnitInfo>().ChangeStance(Stance.Advance);
                EndAction();
                break;

            case ActionType.ChargeStart:
                startHex = activeUnit.transform.parent.gameObject.GetComponent<HexTile>();
                activeUnit.GetComponent<UnitInfo>().ChangeMotionType(MotionType.StraightType);
                if (action.team == Team.Player)
                    activeUnit.GetComponent<UnitControl>().TriggerMove(action.value);
                else activeUnit.GetComponent<BasicUnitAI>().MoveToClosestPlayerUnit(action.value);
                break;

            case ActionType.ChargeEnd:
                var endHex = activeUnit.transform.parent.gameObject.GetComponent<HexTile>();
                var dmg = 4 - distanceFinder.GetDistanceBetweenHexes(startHex, endHex);
                Debug.Log("DMG = " + dmg);
                if (action.team == Team.Player)
                    activeUnit.GetComponent<UnitControl>().TriggerAttack(dmg);
                else
                {
                    activeUnit.GetComponent<UnitInfo>().damage += dmg;
                    StartReactionWindow(targetUnit);
                }
                break;

            case ActionType.NearDraw:
                var enemyIsNear = activeUnit.GetComponent<UnitControl>().CheckForEnemiesInBTB();
                if (!enemyIsNear)
                    gameManager.DrawCards(action.team, action.value);
                else EndAction();
                break;

            case ActionType.PushEnemy:
                if(currTeam == Team.Player)
                {
                    movingEnemy = true;
                    targetUnit.GetComponent<UnitControl>().TriggerMove(action.value);
                }
                break;

            case ActionType.Skip:
                EndAction();
                break;
        }
        if (isReactionTime && currTeam == Team.Player && actionQueue.Where(x => x.team == Team.Enemy).Count() == 0)
            EndReactionWindow();
        
    }

    public void EndAction()
    {
        inAction = false;
        movingEnemy = false;
        //if (isReactionTime && currTeam == Team.Enemy && actionQueue.Where(x => x.team == Team.Player).Count() == 0)
        //    EndReactionWindow();
        if(actionQueue.Count == 0)
        {
            if (activeUnit != null)
            {
                activeUnit.GetComponent<UnitInfo>().UpdateStance();
                activeUnit.GetComponent<UnitControl>().HighlighParenttHex();
            }
            if (targetUnit != null)
                targetUnit.GetComponent<UnitInfo>().UpdateStance();
        }
        if (currTeam == Team.Player && gameManager.CurrentGame.Player.Mana == 0)
            buttonsContainer.DeactivateUnitButtons();
        if (actionQueue.Count == 0 && activeUnit != null && currTeam == Team.Player)
        {
            Debug.Log("Refreshed Cards");
            gameManager.ShowPlayableCards(Card.CardType.Attack, activeUnit.GetComponent<UnitInfo>());
        }
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
            TurnTime = System.Math.Min(90, stoppedTurnTime + 10);
        else TurnTime = 90;
        stoppedTurnTime = 0;

        UiController.Instance.UpdateTurnTime(TurnTime);

        //gameManager.CheckCardsForManaAvaliability();

        if (currTeam == Team.Player)
        {
            while (TurnTime-- > 0)
            {
                UiController.Instance.UpdateTurnTime(TurnTime);
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            if(activeUnit == null)
            {
                var unit = FindObjectsOfType<UnitInfo>()
                    .Where(x => x.teamSide == Team.Enemy && !activatedUnits.Contains(x))
                    .FirstOrDefault().gameObject;
                unit.GetComponent<UnitControl>().ActivateFigure();
            }
            while (TurnTime-- > 0)
            {
                if (actionQueue.Count == 0)
                {
                    activeUnit.GetComponent<BasicUnitAI>().GenerateAction();
                }
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
        targetUnit = null;

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

        playedCards = 0;
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
        gameManager.MakeAllCardsUnplayable();
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
            //targetInfo.defence += Random.Range(0, 4);
            targetInfo.gameObject.GetComponent<BasicUnitAI>().GenerateDefence();
            //EndReactionWindow();
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
        defCardPlayed = false;
        Debug.Log("Reaction window ended");
        activeUnit.GetComponent<UnitControl>().MakeAtack(targetUnit.GetComponent<UnitInfo>());
        isReactionTime = false;
        if (currTeam == Team.Enemy)
            gameManager.MakeAllCardsUnplayable();
        //else gameManager.ShowPlayableCards(Card.CardType.Attack, activeUnit.GetComponent<UnitInfo>());
        UiController.Instance.ChangeEndButtonText();
        StartCoroutine(TurnFunc());
    }

    public IEnumerator ReactionFunc()
    {
        TurnTime = 45;

        UiController.Instance.UpdateTurnTime(TurnTime);
        //gameManager.ShowPlayableCards(Card.CardType.Defense, targetUnit.GetComponent<UnitInfo>());

        while (TurnTime-- > 0)
        {
            gameManager.ShowPlayableCards(Card.CardType.Defense, targetUnit.GetComponent<UnitInfo>());
            if (defCardPlayed && actionQueue.Count == 0)
                break;
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
        else
        {
            if (!activatedUnits.Contains(activeUnit.GetComponent<UnitInfo>()))
                activatedUnits.Add(activeUnit.GetComponent<UnitInfo>());
            EndPlayerActivation();
        }
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
