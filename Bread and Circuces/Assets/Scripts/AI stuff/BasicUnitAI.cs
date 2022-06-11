using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class BasicUnitAI : MonoBehaviour
{
    private GameManagerScript gameManager;
    private TurnManager turnManager;
    private UnitControl unitControl;
    private UnitInfo info;
    private DistanceFinder distanceFinder;
    private UnitInfo targetUnit;

    private void Start()
    {
        unitControl = GetComponent<UnitControl>();
        info = GetComponent<UnitInfo>();
        gameManager = FindObjectOfType<GameManagerScript>();
        turnManager = FindObjectOfType<TurnManager>();
        distanceFinder = FindObjectOfType<DistanceFinder>();
    }

    public void GenerateAction()
    {
        var unitsAlive = FindObjectsOfType<UnitInfo>()
            .Where(x => x.teamSide == Team.Enemy).Count();
        var playerUnits = FindObjectsOfType<UnitInfo>()
            .Where(x => x.teamSide == Team.Player).Count();
        var actionsLeft = gameManager.CurrentGame.Enemy.Mana;
        var cardsInHand = gameManager.enemyHandSize;

        if (playerUnits == 0)
        {
            MenuManager.Instance.CheckWinCondition();
            return;
        }
        if (cardsInHand == 0 || actionsLeft == 0 || (unitsAlive == 2 && turnManager.activationNum < 2 && actionsLeft < 3))
        {
            Debug.Log("Returned");
            turnManager.EndPlayerActivation();
            return;
        }
        if(CheckForPlayerUnitsInDistance(info.attackReachDistance))
        {
            turnManager.targetUnit = targetUnit.gameObject;
            GenerateAttack();
        }
        else
        {
            MakeMove();
        }
    }

    public bool CheckForPlayerUnitsInDistance(int distance)
    {
        var tiles = new List<HexTile>();
        tiles = distanceFinder.GetTilesInRadius(transform.parent.GetComponent<HexTile>(), distance);
        foreach (var tile in tiles)
        {
            if (tile.isOccupied)
            {
                var possibleUnit = tile.transform.GetChild(0).GetComponent<UnitInfo>();
                if (tile.transform.GetChild(0).GetComponent<UnitInfo>().IsEnemy(info))
                {
                    targetUnit = possibleUnit;
                    return true;
                }
            }
        }
        return false;
    }

    public void MakeMove()
    {
        gameManager.ReduceMana(false, 1);
        MoveToClosestPlayerUnit(info.moveDistance);
    }

    public UnitInfo GetClosestPlayerUnit()
    {
        var units = FindObjectsOfType<UnitInfo>()
            .Where(x => x.teamSide == Team.Player)
            .Select(x => Tuple.Create(x, distanceFinder.GetDistanceBetweenHexes(transform.parent.GetComponent<HexTile>(),
            x.transform.parent.GetComponent<HexTile>())));
        var lowestDistance = units
            .Select(x => x.Item2).Min();
        var unit = units
            .Where(x => x.Item2 == lowestDistance)
            .Select(x => x.Item1).First();
        return unit;
    }

    public void MoveToClosestPlayerUnit(int distance)
    {
        var target = GetClosestPlayerUnit();
        var parentHex = transform.parent.GetComponent<HexTile>();
        var targetHex = target.transform.parent.GetComponent<HexTile>();
        if (distanceFinder.GetDistanceBetweenHexes(targetHex, parentHex) == 1)
        {
            turnManager.EndAction();
            return;
        }
        var tilesToMove = new List<HexTile>();
        tilesToMove = distanceFinder.FindPaths(parentHex, distance);
        var tilesWithDistances = tilesToMove.
            Select(x => Tuple.Create(x, distanceFinder.GetDistanceBetweenHexes(targetHex,
            x),
            distanceFinder.GetDistanceBetweenHexes(parentHex,
            x)))
            .Where(x => x.Item2 > 0);
        var minimalDistanceToTarget = tilesWithDistances.Select(x => x.Item2).Min();
        tilesWithDistances = tilesWithDistances.Where(x => x.Item2 == minimalDistanceToTarget);
        var minDistanceToWalk = tilesWithDistances.Select(x => x.Item3).Min();
        var hexToMove = tilesWithDistances.Where(x => x.Item3 == minDistanceToWalk)
            .Select(x => x.Item1).FirstOrDefault();
        unitControl.MoveFigureOnObject(hexToMove);
        Debug.Log("Moved");
        turnManager.EndAction();
    }

    public void MoveAwayFromClosestPlayerUnit(int distance)
    {
        var target = GetClosestPlayerUnit();
        var parentHex = transform.parent.GetComponent<HexTile>();
        var targetHex = target.transform.parent.GetComponent<HexTile>();
        if (distanceFinder.GetDistanceBetweenHexes(targetHex, parentHex) == 1)
        {
            turnManager.EndAction();
            return;
        }
        var tilesToMove = new List<HexTile>();
        tilesToMove = distanceFinder.FindPaths(parentHex, distance);
        var tilesWithDistances = tilesToMove.
            Select(x => Tuple.Create(x, distanceFinder.GetDistanceBetweenHexes(targetHex,
            x),
            distanceFinder.GetDistanceBetweenHexes(parentHex,
            x)))
            .Where(x => x.Item2 > 0);
        var maximumDistanceToTarget = tilesWithDistances.Select(x => x.Item2).Max();
        tilesWithDistances = tilesWithDistances.Where(x => x.Item2 == maximumDistanceToTarget);
        var maxDistanceToWalk = tilesWithDistances.Select(x => x.Item3).Max();
        var hexToMove = tilesWithDistances.Where(x => x.Item3 == maxDistanceToWalk)
            .Select(x => x.Item1).FirstOrDefault();
        unitControl.MoveFigureOnObject(hexToMove);
        Debug.Log("Moved");
        turnManager.EndAction();
    }

    public void GenerateAttack()
    {
        var availableCards = gameManager.CurrentGame.Enemy.Deck
            .Where(x => x.Restriction == CardRestriction.Universal && x.Type == Card.CardType.Attack
            && x.StartStance == info.currentStance).ToList();
        Debug.Log("All cards = " + gameManager.CurrentGame.Enemy.Deck.Count);
        Debug.Log("Available cards = " + availableCards.Count);
        if (availableCards.Count == 0 || !CanPlayCard())
        {
            turnManager.EndPlayerActivation();
            return;
        }
        var card = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
        UseCard(card, info);
    }

    public void GenerateDefence()
    {
        var availableCards = gameManager.CurrentGame.Enemy.Deck
            .Where(x => x.Restriction == CardRestriction.Universal && x.Type == Card.CardType.Defense
            && x.StartStance == info.currentStance).ToList();
        if (availableCards.Count == 0 || !CanPlayCard())
        {
            turnManager.AddAction(new Action(ActionType.Skip, Team.Enemy, 0));
            return;
        }
        var card = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
        turnManager.defCardPlayed = true;
        UseCard(card, info);
    }

    public void UseCard(Card card, UnitInfo unit)
    {
        Debug.Log("—ыграно " + card.Name);
        gameManager.ReduceMana(false, card.Manacost);

        var spellCard = card;

        unit.ChangeStance(spellCard.EndStance);

        switch (card.FirstCardEff)
        {
            case Card.CardEffect.Damage://confirmed
                turnManager.AddAction(new Action(ActionType.Attack, Team.Enemy, card.SpellValue));
                break;

            case Card.CardEffect.PushBackEnemy:
                {
                    turnManager.AddAction(new Action(ActionType.Attack, Team.Enemy, card.SpellValue));

                    turnManager.AddAction(new Action(ActionType.PushEnemy, Team.Enemy, card.SpellValue));
                }
                break;

            case Card.CardEffect.DamageFinisher: // нужно добавить обнуление numberCard в методе смены хода(он пока у нас не робит)
                turnManager.AddAction(new Action(ActionType.FinisherAttack, Team.Enemy, card.SpellValue));
                break;

            case Card.CardEffect.Defense:// confirmed
                unit.defence += card.SpellValue;
                //turnManager.defCardPlayed = true;
                break;

            case Card.CardEffect.ShieldedDefense:
                {
                    unit.defence += card.SpellValue;
                    if (unit.withShield)
                        unit.defence += 1;
                    //turnManager.defCardPlayed = true;
                }
                break;

            case Card.CardEffect.Movement:// confirmed
                turnManager.AddAction(new Action(ActionType.Push, Team.Enemy, card.SpellValue));
                break;

            case Card.CardEffect.ChargeStart:
                turnManager.AddAction(new Action(ActionType.ChargeStart, Team.Enemy, card.SpellValue));
                break;

            case Card.CardEffect.DiscardSelf:
                turnManager.AddAction(new Action(ActionType.DiscardActivePlayer, Team.Enemy, card.SpellValue));
                break;
        }
        switch (card.FirstCardEffTwo)
        {
            case Card.CardEffect.Damage:// confirmed
                turnManager.AddAction(new Action(ActionType.Attack, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.DamageAfterDiscard:
                turnManager.AddAction(new Action(ActionType.AttackWithDiscardBuff, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.Movement:// confirmed
                turnManager.AddAction(new Action(ActionType.Push, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.CardDrow:// confirmed
                turnManager.AddAction(new Action(ActionType.Draw, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.AliveCardDrow:
                turnManager.AddAction(new Action(ActionType.DrawIfAlive, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.Stun:
                turnManager.AddAction(new Action(ActionType.ChangeEnemyStance, Team.Enemy, card.SecondSpellValue));
                turnManager.AddAction(new Action(ActionType.DiscardOpponent, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.NearCardDrow:
                turnManager.AddAction(new Action(ActionType.NearDraw, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.DiscardEnemy:
                turnManager.AddAction(new Action(ActionType.DiscardOpponent, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.ManaAdd:
                {
                    gameManager.CurrentGame.Enemy.SpellManapool();
                    UiController.Instance.UpdateMana();
                }
                break;

            case Card.CardEffect.ChargeEnd:
                turnManager.AddAction(new Action(ActionType.ChargeEnd, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.CancelCard:
                turnManager.AddAction(new Action(ActionType.CancelCard, Team.Enemy, card.SecondSpellValue));
                break;

            case Card.CardEffect.No:
                turnManager.AddAction(new Action(ActionType.Skip, Team.Enemy, card.SecondSpellValue));
                break;
        }

        LastCard(card);
        gameManager.enemyHandSize--;
    }

    void LastCard(Card card)
    {
        if (gameManager.EnemyCardPanel.childCount != 0)
            Destroy(gameManager.EnemyCardPanel.GetChild(0).gameObject);
        GameObject cardGG = Instantiate(gameManager.CardPref, gameManager.EnemyCardPanel);
        CardController cardCard = cardGG.GetComponent<CardController>();
        cardCard.Init(card, true);
    }

    bool CanPlayCard()
    {
        var randomizedNum = UnityEngine.Random.Range(0,3);
        return gameManager.enemyHandSize > randomizedNum;
    }
}
