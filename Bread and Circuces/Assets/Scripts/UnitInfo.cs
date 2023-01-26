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
    Raging,
    Any
}

public enum MotionType
{
    StraightType,
    RadiusType
}

public enum Animation
{
    Attack,
    Hit,
    Block
}

public abstract class UnitInfo : MonoBehaviour
{
    [SerializeField] private AudioSource AttackSound;
    [SerializeField] private AudioSource BlockSound;
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

    protected int baseDamage;
    protected int baseDefence;
    protected int baseAttackReachDistance;

    public Sprite altSkin;
    public HealthBar healthbar;
    public float maxHealth;
    public Vector3 offset = new Vector3(0f, 0f, 0f);

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        motionType = MotionType.RadiusType;
        currentStance = Stance.Advance;
        stanceToChange = Stance.Advance;
        baseDamage = damage;
        baseDefence = defence;
        baseAttackReachDistance = attackReachDistance;
        UnitDeck = new List<Card.Card>();

        maxHealth = health;
        healthbar.SetHealth(health, maxHealth);
    }

    protected void ChangeAnimation(string characterName, Animation animation)
    {
        if (animation == Animation.Attack)
        {
            spriteRenderer.sortingOrder = 2;
            characterName += "InGameAttack";

            animator.Play(characterName);
            AttackSound.Play();
        }

        if (animation == Animation.Hit)
        {
            if (spriteRenderer.sortingOrder > 1)
            {
                spriteRenderer.sortingOrder = 1;
            }

            characterName += "InGameHit";
            animator.Play(characterName);
        }

        if (animation == Animation.Block)
        {
            characterName += "InGameBlock";
            animator.Play(characterName);
            float i = 0;
            while (i < 1)
            {
                i += Time.deltaTime;
            }
            BlockSound.Play();
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
        attackReachDistance = baseAttackReachDistance;
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