using System.Collections.Generic;
using Ui;
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
    public List<Card.Card> UnitDeck;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    private int baseDamage;
    private int baseDefence;

    public Sprite altSkin;
    public HealthBar healthbar;
    public float maxHealth;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        motionType = MotionType.RadiusType;
        currentStance = Stance.Advance;
        stanceToChange = Stance.Advance;
        baseDamage = damage;
        baseDefence = defence;
        UnitDeck = new List<Card.Card>();

        maxHealth = health;
        healthbar.SetHealth(health, maxHealth);
    }

    protected void ChangeAnimationAttack(string characterName)
    {
        spriteRenderer.sortingOrder += 1;
        if (characterName == "Hoplomachus(Clone)")
        {
            Debug.Log("Анимация работает");
            animator.Play("HoplmachusInGameAttack");
        }
        else if (characterName == "Murmillo(Clone)")
        {
            animator.Play("MurmilloInGameAttack");
        }
        else if (characterName == "Retiarius(Clone)")
        {
            animator.Play("RetiariusInGameAttack");
        }
        else if (characterName == "Scisssor(Clone)")
        {
            animator.Play("ScissorInGameAttack");
        }
    }

    protected void ChangeAnimationHit(string characterName)
    {
        if (spriteRenderer.sortingOrder > 1)
        {
            spriteRenderer.sortingOrder -= 1;
        }

        if (characterName == "Hoplomachus(Clone)")
            animator.Play("HoplmachusInGameHit");
        else if (characterName == "Murmillo(Clone)")
        {
            animator.Play("MurmilloInGameHit");
        }
        else if (characterName == "Retiarius(Clone)")
        {
            animator.Play("RetiariusInGameHit");
        }
        else if (characterName == "Scisssor(Clone)")
        {
            animator.Play("ScissorInGameHit");
        }
    }

    protected void ChangeAnimationBlock(string characterName)
    {
        if (name == "Hoplomachus(Clone)")
            animator.Play("HoplmachusInGameBlock");
        else if (name == "Murmillo(Clone)")
        {
            animator.Play("MurmilloInGameBlock");
        }
        else if (name == "Retiarius(Clone)")
        {
            animator.Play("RetiariusInGameBlock");
        }
        else if (name == "Scisssor(Clone)")
        {
            animator.Play("ScissorInGameBlock");
        }
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

    public virtual void OnDefenceEnd(float blockDamage)
    {
        defence = baseDefence;
    }

    public void UpdateStance()
    {
        currentStance = stanceToChange;
    }
}