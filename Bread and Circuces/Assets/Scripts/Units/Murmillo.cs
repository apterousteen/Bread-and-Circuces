using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murmillo : UnitInfo
{
    protected override void Start()
    {
        damage = 3;

        health = 15;
        defence = 0;
        attackReachDistance = 1;
        moveDistance = 3;
        withShield = true;

        base.Start();

        UnitDeck.Add(new Card("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Attacking, Card.CardType.Attack, Card.CardEffect.CheckDefenseStance, 0, Card.CardEffect.ResetCard, 2, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Attacking, Card.CardType.Attack, Card.CardEffect.CheckDefenseStance, 0, Card.CardEffect.ResetCard, 2, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardType.Defense, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Тип карты
        UnitDeck.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardType.Defense, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Тип карты
        UnitDeck.Add(new Card("Блок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Defensive, Card.CardType.Defense, Card.CardEffect.Defense, 2, Card.CardEffect.Type, 1, Card.TargetType.This));//Тип карты
        UnitDeck.Add(new Card("Уворот", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Defensive, Card.Stance.Advance, Card.CardType.Defense, Card.CardEffect.Defense, 3, Card.CardEffect.Movement, 1, Card.TargetType.This));
        UnitDeck.Add(new Card("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardType.Attack, Card.CardEffect.Damage, 2, Card.CardEffect.Mechanics, 1, Card.TargetType.Enemy));//нужен метод
        UnitDeck.Add(new Card("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardType.Attack, Card.CardEffect.Damage, 2, Card.CardEffect.Mechanics, 1, Card.TargetType.Enemy));//нужен метод
        UnitDeck.Add(new Card("Отталкивание", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardType.Attack, Card.CardEffect.Damage, 2, Card.CardEffect.Mechanics, 1, Card.TargetType.Enemy));//нужен метод
        UnitDeck.Add(new Card("Отталкивание", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Defensive, Card.CardType.Attack, Card.CardEffect.Damage, 2, Card.CardEffect.Mechanics, 1, Card.TargetType.Enemy));//нужен метод
        UnitDeck.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Прикрыться", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardType.Defense, Card.CardEffect.DefenseType, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This));// Добавить case
        UnitDeck.Add(new Card("Прикрыться", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Defensive, Card.CardType.Defense, Card.CardEffect.DefenseType, 1, Card.CardEffect.CardDrow, 1, Card.TargetType.This));// Добавить case
        UnitDeck.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardType.Attack, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
    }

    public override void OnAttackEnd(UnitInfo target)
    {
        base.OnAttackEnd(target);
    }

    public override void OnAttackStart(UnitInfo target)
    {

    }

    public override void OnDefenceStart()
    {
        defence++;
    }

    public override void OnDefenceEnd()
    {
        base.OnDefenceEnd();
    }

    public override void OnMove()
    {

    }
}
