using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoplomachus : UnitInfo
{
    private DistanceFinder distanceFinder;
    protected override void Start()
    
    {
        damage = 3;

        health = 15;
        defence = 0;
        attackReachDistance = 2;
        moveDistance = 3;
        withShield = true;

        distanceFinder = FindObjectOfType<DistanceFinder>();

        base.Start();

        UnitDeck.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        UnitDeck.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        UnitDeck.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Работа с типами карт
        UnitDeck.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Работа с типами карт
        UnitDeck.Add(new Card("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Прикрыться", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardEffect.Defense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This));// Работа со способностью карты
        UnitDeck.Add(new Card("Прикрыться", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardEffect.Defense, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This));// Работа со способностью карты
        UnitDeck.Add(new Card("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));// Работа с передвижением
        UnitDeck.Add(new Card("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardEffect.Damage, 2, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));// Работа с передвижением
        UnitDeck.Add(new Card("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));//Работа со способностью карты
        UnitDeck.Add(new Card("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));//Работа со способностью карты
        UnitDeck.Add(new Card("Преследование", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));//передвижение
        UnitDeck.Add(new Card("Преследование", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 1, Card.TargetType.Enemy));//передвижение
    }

    public override void OnAttackEnd(UnitInfo target)
    {
        base.OnAttackEnd(target);
    }

    public override void OnAttackStart(UnitInfo target)
    {
        var occupiedHex = transform.parent.GetComponent<HexTile>();
        var targetHex = target.transform.parent.GetComponent<HexTile>();
        var distance = distanceFinder.GetDistanceBetweenHexes(occupiedHex, targetHex);
        if (distance == 1 && currentStance == Stance.Attacking)
            damage += 1;
    }

    public override void OnDefenceStart()
    {

    }

    public override void OnDefenceEnd()
    {
        base.OnDefenceEnd();
    }

    public override void OnMove()
    {

    }
}
