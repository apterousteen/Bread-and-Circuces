using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class BasicUnitUI : MonoBehaviour
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
        var actionsLeft = gameManager.CurrentGame.Enemy.Mana;
        if (actionsLeft == 0 || (unitsAlive == 2 && turnManager.activatedUnits.Count < 3 && actionsLeft < 3))
        {
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
            return;
        var tilesToMove = new List<HexTile>();
        tilesToMove = distanceFinder.FindPaths(parentHex, distance);
        var tilesWithDistances = tilesToMove.
            Select(x => Tuple.Create(x, distanceFinder.GetDistanceBetweenHexes(targetHex,
            x.transform.parent.GetComponent<HexTile>()),
            distanceFinder.GetDistanceBetweenHexes(parentHex,
            x.transform.parent.GetComponent<HexTile>())))
            .Where(x => x.Item2 > 0);
        var minimalDistanceToTarget = tilesWithDistances.Select(x => x.Item2).Min();
        tilesWithDistances = tilesWithDistances.Where(x => x.Item2 == minimalDistanceToTarget);
        var minDistanceToWalk = tilesWithDistances.Select(x => x.Item3).Min();
        var hexToMove = tilesWithDistances.Where(x => x.Item3 == minDistanceToWalk)
            .Select(x => x.Item1).FirstOrDefault();
        unitControl.MoveFigureOnObject(hexToMove);
    }

    public void GenerateAttack()
    {
        var availableCards = gameManager.CurrentGame.Enemy.Deck
            .Where(x => x.Restriction == CardRestriction.Universal && x.Type == Card.CardType.Attack
            && x.StartStance == info.currentStance).ToList();
        if (availableCards.Count == 0)
            return;
        var card = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
        UseCard(card, info);
    }

    public void GenerateDefence()
    {
        var availableCards = gameManager.CurrentGame.Enemy.Deck
            .Where(x => x.Restriction == CardRestriction.Universal && x.Type == Card.CardType.Defense
            && x.StartStance == info.currentStance).ToList();
        if (availableCards.Count == 0)
            return;
        var card = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
        UseCard(card, info);
    }

    public void UseCard(Card card, UnitInfo unit)
    {
        Debug.Log("Сыграно " + card.Name);
        gameManager.ReduceMana(false, card.Manacost);

        var spellCard = card;

        unit.ChangeStance(spellCard.EndStance);
        switch (spellCard.FirstCardEff)
        {
            case Card.CardEffect.Damage://confirmed
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue));
                break;

            case Card.CardEffect.DamagePlusMovement:// скорее всего будут вместе срабатывать, нужно добавить бул перемнную в метод атаки
                {
                    turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue));

                    turnManager.AddAction(new Action(ActionType.Push, spellCard.SpellValue));
                }
                break;

            case Card.CardEffect.PlusDamageCard: // нужно добавить обнуление numberCard в методе смены хода(он пока у нас не робит)
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue));
                break;

            case Card.CardEffect.Defense:// confirmed
                unit.defence += spellCard.SpellValue;
                break;

            case Card.CardEffect.CheckDefenseStance: // нужно допилить
                break;

            case Card.CardEffect.DefensePlusType:
                {
                    unit.defence += spellCard.SpellValue;
                    if (unit.withShield)
                        unit.defence += 1;
                }
                break;

            case Card.CardEffect.Survived:
                unit.CheckForAlive();
                break;

            case Card.CardEffect.Movement:// confirmed
                turnManager.AddAction(new Action(ActionType.Push, spellCard.SpellValue));
                break;
        }
        switch (spellCard.FirstCardEffTwo)
        {
            case Card.CardEffect.Damage:// confirmed
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.IfDamage:
                break;

            case Card.CardEffect.Movement:// confirmed
                turnManager.AddAction(new Action(ActionType.Push, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.CardDrow:// confirmed
                turnManager.AddAction(new Action(ActionType.Draw, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.AliveCardDrow:
                if (unit.CheckForAlive())
                    turnManager.AddAction(new Action(ActionType.Draw, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.IfCardDrow:
                break;

            case Card.CardEffect.ResetCard:
                turnManager.AddAction(new Action(ActionType.DiscardOpponent, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.ManaAdd:
                {
                    gameManager.CurrentGame.Enemy.SpellManapool();
                    UiController.Instance.UpdateMana();
                }
                break;

            case Card.CardEffect.Type:
                if (unit.withShield)
                    unit.defence += spellCard.SecondSpellValue;
                break;

            case Card.CardEffect.Mechanics:
                break;

            case Card.CardEffect.No:
                break;
        }

        gameManager.enemyHandSize--;
    }
}
