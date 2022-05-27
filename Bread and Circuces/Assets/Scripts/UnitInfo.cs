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

public abstract class UnitInfo : MonoBehaviour
{
    public string unitName;
    public int health;
    public int moveDistance;
    public int attackReachDistance;
    public int damage;
    public int defence;
    public Team teamSide;
    public Stance currentStance;
    public MotionType motionType;
    public bool withShield;
    public List<Card> UnitDeck;

    private int baseDamage;
    private int baseDefence;

    protected virtual void Start()
    {
        motionType = MotionType.RadiusType;
        currentStance = Stance.Advance;
        baseDamage = damage;
        baseDefence = defence;
        UnitDeck = new List<Card>();
    }

    public bool IsEnemy(UnitInfo otherUnit)
    {
        return this.teamSide != otherUnit.teamSide;
    }

    public virtual void ChangeStance(Stance newStance)
    {
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

    public bool CheckForAlive()
    {
        if (health <= 0)
        {
            Die();
            return false;
        }
        else
            return true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void displayInfo()
    {
        Debug.Log(teamSide.ToString() + " " + health);
    }

    public abstract void OnAttackStart(UnitInfo target);

    public virtual void OnAttackEnd(UnitInfo target)
    {
        damage = baseDamage;
    }

    public abstract void OnMove();

    public abstract void OnDefenceStart();

    public virtual void OnDefenceEnd()
    {
        defence = baseDefence;
    }
}
