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

        UnitDeck.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        UnitDeck.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        UnitDeck.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        UnitDeck.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        UnitDeck.Add(new SpellCard("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Укол из-за щита", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Выпад вперед", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        UnitDeck.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        UnitDeck.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Defensive, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));// Работа с передвижением
        UnitDeck.Add(new SpellCard("Тычок с отступлением", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Defensive, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));// Работа с передвижением
        UnitDeck.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        UnitDeck.Add(new SpellCard("Осторожный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        UnitDeck.Add(new SpellCard("Преследование", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));//передвижение
        UnitDeck.Add(new SpellCard("Преследование", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.Enemy));//передвижение
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
