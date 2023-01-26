using UnityEngine;

public class Veles : UnitInfo
{
    private TurnManager turnManager;

    protected override void Start()
    {
        damage = 0;

        health = 14;
        defence = 0;
        attackReachDistance = 1;
        moveDistance = 2;
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
        ChangeAnimation("Veles", animation: Animation.Attack);
    }

    public override void OnDefenceStart()
    {
    }

    public override void OnDefenceEnd(float blockDamage)
    {
        if (blockDamage == 0)
        {
            ChangeAnimation("Veles", animation: Animation.Block);
        }
        else
        {
            ChangeAnimation("Veles", animation: Animation.Hit);
        }

        base.OnDefenceEnd(blockDamage);
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