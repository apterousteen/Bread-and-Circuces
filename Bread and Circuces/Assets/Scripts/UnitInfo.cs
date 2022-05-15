using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player,
    Enemy
}

public enum Stance
{
    Defensive,
    Advance,
    Attacking,
    Raging
}

public enum MotionType
{
    StraightType,
    RadiusType
}

public class UnitInfo : MonoBehaviour
{
    public int health;
    public int moveDistance;
    public int attackReachDistance;
    public int damage;
    public int defence;
    public Team teamSide;
    public Stance currentStance;
    public MotionType motionType;

    void Start()
    {
        motionType = MotionType.RadiusType;
    }

    public bool IsEnemy(UnitInfo otherUnit)
    {
        return this.teamSide != otherUnit.teamSide;
    }

    public void ChangeStance(Stance newStance)
    {
        if (currentStance == Stance.Raging && newStance == Stance.Attacking)
            return;
        currentStance = newStance;
    }    

    public void ChangeMotionType(MotionType typeIn)
    {
        motionType = typeIn;
    }

    public void SufferDamage(int damageValue)
    {
        health -= damageValue;
        displayInfo();
        CheckForAlive();
    }

    public void CheckForAlive()
    {
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void displayInfo()
    {
        Debug.Log(teamSide.ToString() + " " + health);
    }
}
