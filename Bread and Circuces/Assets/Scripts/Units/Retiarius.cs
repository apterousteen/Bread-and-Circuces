using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retiarius : UnitInfo
{
    private GameManagerScript gameManager;

    protected override void Start()
    {
        damage = 0;

        health = 16;
        defence = 0;
        attackReachDistance = 2;
        moveDistance = 3;
        withShield = false;

        base.Start();

        gameManager = FindObjectOfType<GameManagerScript>();
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

    public override void OnMoveEnd()
    {
        if (motionType == MotionType.StraightType)
            gameManager.DrawCards(teamSide, 1); // Draw Card
    }
}
