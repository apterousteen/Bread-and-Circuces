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
