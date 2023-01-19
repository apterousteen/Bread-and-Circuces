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
    public Stance stanceToChange;
    public MotionType motionType;
    public bool withShield;
    public List<Card> UnitDeck;

    private int baseDamage;
    private int baseDefence;

    public Sprite altSkin;
    public HealthBar healthbar;
    public float maxHealth;

    protected virtual void Start()
    {
        motionType = MotionType.RadiusType;
        currentStance = Stance.Advance;
        stanceToChange = Stance.Advance;
        baseDamage = damage;
        baseDefence = defence;
        UnitDeck = new List<Card>();

        maxHealth = health;
        healthbar.SetHealth(health, maxHealth);
    }

    public bool IsEnemy(UnitInfo otherUnit)
    {
        return this.teamSide != otherUnit.teamSide;
    }

    public virtual void ChangeStance(Stance newStance)
    {
        if (teamSide == Team.Player)
            Debug.Log("Changed stance on " + newStance.ToString());
        stanceToChange = newStance;
    }

    public void ChangeMotionType(MotionType typeIn)
    {
        motionType = typeIn;
    }

    public void SufferDamage(int damageValue)
    {
        health -= damageValue;

        healthbar.SetHealth(health, maxHealth);
        displayInfo();
        CheckForAlive();
    }

    public bool CheckForAlive()
    {
        if (health <= 0)
        {
            MenuManager.Instance.CheckWinCondition();
            Die();
            return false;
        }
        else
            return true;
    }

    private void Die()
    {
        transform.parent.GetComponent<HexTile>().isOccupied = false;
        UiController.Instance.UpdateIcons(gameObject);
        if (teamSide == Team.Player)
            GameManagerScript.Instance.CurrentGame.Player.units.unitsAlive--;
        else GameManagerScript.Instance.CurrentGame.Enemy.units.unitsAlive--;
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

    public virtual bool OnMoveStart()
    {
        return false;
    }

    public abstract void OnMoveEnd();

    public abstract void OnDefenceStart();

    public virtual void OnDefenceEnd()
    {
        defence = baseDefence;
    }

    public void UpdateStance()
    {
        currentStance = stanceToChange;
    }
}
