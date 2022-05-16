using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissor : UnitInfo
{
    protected override void Start()
    {
        damage = 3;

        health = 14;
        defence = 1;
        attackReachDistance = 1;
        moveDistance = 2;
        withShield = false;

        base.Start();

        UnitDeck.Add(new Card("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Raging, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 0, Card.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new Card("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Raging, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 0, Card.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new Card("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Raging, Card.CardEffect.Damage, 4, Card.CardEffect.Movement, 0, Card.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Advance, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Rip and tear", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Raging, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.ResetCard, 2, Card.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new Card("Яростная серия", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Raging, Card.Stance.Raging, Card.CardEffect.Damage, 2, Card.CardEffect.ManaAdd, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Raging, Card.CardEffect.Defense, 0, Card.CardEffect.CardDrow, 2, Card.TargetType.This));// + механика
        UnitDeck.Add(new Card("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Raging, Card.CardEffect.Defense, 0, Card.CardEffect.CardDrow, 2, Card.TargetType.This));// + механика
        UnitDeck.Add(new Card("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 4, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Advance, Card.CardEffect.Damage, 4, Card.CardEffect.CardDrow, 1, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
        UnitDeck.Add(new Card("Удар клинком", "Sprites/LogoCards/CHto-to", 1, Card.Stance.Attacking, Card.Stance.Attacking, Card.CardEffect.Damage, 3, Card.CardEffect.No, 0, Card.TargetType.Enemy));
    }

    public override void ChangeStance(Stance newStance)
    {
        if (currentStance == Stance.Raging && newStance == Stance.Attacking)
            return;
        if (newStance == Stance.Defensive)
            newStance = Stance.Advance;
        if (newStance == Stance.Raging)
            damage += 1;
        else damage -= 1;
        base.ChangeStance(newStance);
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

    }

    public override void OnDefenceEnd()
    {
        base.OnDefenceEnd();
    }

    public override void OnMove()
    {

    }
}
