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

        UnitDeck.Add(new SpellCard("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 0, SpellCard.CardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//+механика
        UnitDeck.Add(new SpellCard("Оглушение щитом", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 0, SpellCard.CardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//+механика
        UnitDeck.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        UnitDeck.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        UnitDeck.Add(new SpellCard("Блок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 2, SpellCard.CardEffect.Type, 1, SpellCard.TargetType.This));//Работа с типами карт
        UnitDeck.Add(new SpellCard("Уворот", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Defensive, SpellCard.Stance.Advance, SpellCard.CardEffect.Defense, 3, SpellCard.CardEffect.Movement, 1, SpellCard.TargetType.This));
        UnitDeck.Add(new SpellCard("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Mechanics, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        UnitDeck.Add(new SpellCard("Заверщающий рубец", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.Mechanics, 1, SpellCard.TargetType.Enemy));//Работа со способностью карты
        UnitDeck.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        UnitDeck.Add(new SpellCard("Прикрыться", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Defensive, SpellCard.CardEffect.Defense, 1, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.This));// Работа со способностью карты
        UnitDeck.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
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
