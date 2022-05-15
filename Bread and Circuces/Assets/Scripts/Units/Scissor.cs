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

        UnitDeck.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Raging, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Raging, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new SpellCard("Яростный рывок", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Raging, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.Movement, 0, SpellCard.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Внезапный удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Advance_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Rip and tear", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Raging_Advance, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.ResetCard, 2, SpellCard.TargetType.Enemy));//Работа со способностью
        UnitDeck.Add(new SpellCard("Яростная серия", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Raging_Raging, SpellCard.FirstCardEffect.Damage, 2, SpellCard.SecondCardEffect.ManaAdd, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Raging, SpellCard.FirstCardEffect.Defense, 0, SpellCard.SecondCardEffect.CardDrow, 2, SpellCard.TargetType.This));// + механика
        UnitDeck.Add(new SpellCard("Зацепить оружие", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Raging, SpellCard.FirstCardEffect.Defense, 0, SpellCard.SecondCardEffect.CardDrow, 2, SpellCard.TargetType.This));// + механика
        UnitDeck.Add(new SpellCard("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Advance, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Разрезающий удар", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Advance, SpellCard.FirstCardEffect.Damage, 4, SpellCard.SecondCardEffect.CardDrow, 1, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.No, 0, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.No, 0, SpellCard.TargetType.Enemy));
        UnitDeck.Add(new SpellCard("Удар клинком", "Sprites/LogoCards/CHto-to", 1, SpellCard.Stance.Attacking_Attacking, SpellCard.FirstCardEffect.Damage, 3, SpellCard.SecondCardEffect.No, 0, SpellCard.TargetType.Enemy));
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
