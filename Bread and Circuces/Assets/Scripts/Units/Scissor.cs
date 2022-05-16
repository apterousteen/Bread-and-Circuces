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

        UnitDeck.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Raging, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Raging, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Raging, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Rip and tear", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Raging, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new SpellCard("Яростная серия", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Raging, SpellCard.Stance.Raging, SpellCard.CardEffect.Damage, 2, SpellCard.CardEffect.ManaAdd, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Raging, SpellCard.CardEffect.Defense, 0, SpellCard.CardEffect.CardDrow, 2, SpellCard.TargetType.This));// + механика
        UnitDeck.Add(new SpellCard("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Raging, SpellCard.CardEffect.Defense, 0, SpellCard.CardEffect.CardDrow, 2, SpellCard.TargetType.This));// + механика
        UnitDeck.Add(new SpellCard("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Advance, SpellCard.CardEffect.Damage, 4, SpellCard.CardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking, SpellCard.Stance.Attacking, SpellCard.CardEffect.Damage, 3, SpellCard.CardEffect.No, 0, SpellCard.TargetType.Enemy));
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
