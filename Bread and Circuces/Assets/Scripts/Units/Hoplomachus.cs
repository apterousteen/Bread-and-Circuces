using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoplomachus : UnitInfo
{
    private DistanceFinder distanceFinder;
    protected override void Start()

    {
        damage = 0;

        health = 15;
        defence = 0;
        attackReachDistance = 2;
        moveDistance = 3;
        withShield = true;
        distanceFinder = FindObjectOfType<DistanceFinder>();
        base.Start();
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

    public override bool OnMoveStart()
    {
        if (motionType == MotionType.StraightType)
            return true;
        else return false;
    }

    public override void OnMoveEnd()
    {

    }
}
