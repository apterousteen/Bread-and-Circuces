using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thraex : UnitInfo
{
    private TurnManager turnManager;

    protected override void Start()
    {
        damage = 0;

        health = 15;
        defence = 0;
        attackReachDistance = 1;
        moveDistance = 3;
        withShield = true;

        offset = new UnityEngine.Vector3(0f, 0.45f);
        turnManager = FindObjectOfType<TurnManager>();
        base.Start();
    }

    public override void OnAttackEnd(UnitInfo target)
    {
        base.OnAttackEnd(target);
    }

    public override void OnAttackStart(UnitInfo target)
    {
        if (turnManager.defCardPlayed && target.withShield)
            damage += 2;
        ChangeAnimation("Thraex", animation: Animation.Attack);
    }

    public override void OnDefenceStart()
    {
    }

    public override void OnDefenceEnd(float blockDamage)
    {
        if (blockDamage == 0)
        {
            ChangeAnimation("Thraex", animation: Animation.Block);
        }
        else
        {
            ChangeAnimation("Thraex", animation: Animation.Hit);
        }

        base.OnDefenceEnd(blockDamage);
    }

    public override void OnMoveEnd()
    {
    }
}
