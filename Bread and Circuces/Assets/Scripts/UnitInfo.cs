using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player,
    Enemy
}

public class UnitInfo : MonoBehaviour
{
    public int health;
    public int moveDistance;
    public int attackReachDistance;
    public int damage;
    public int defence;
    public Team teamSide;

    public bool IsEnemy(UnitInfo otherUnit)
    {
        return this.teamSide != otherUnit.teamSide;
    }

    public void SufferDamage(int damageValue)
    {
        health -= damageValue;
        CheckForAlive();
    }

    private void CheckForAlive()
    {
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
