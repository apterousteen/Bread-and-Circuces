using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Card Card;
    public CardInfoScript Info;
    public CardMovementScript Movement;
    public UnitInfo Unit;
    public UnitControl UnitControl;
    public GameManagerScript Game;
    private TurnManager turnManager;

    public int numberCard = 0;
    public bool IsPlayerCard;

    GameManagerScript gameManager;
    public void Init(Card card, bool isPlayerCard)
    {
        Card = card;
        gameManager = GameManagerScript.Instance;
        turnManager = FindObjectOfType<TurnManager>();
        IsPlayerCard = isPlayerCard;

        if (isPlayerCard)
            Info.ShowCardInfo();
    }

    public void OnCast()
    {
        numberCard += 1;

        if (IsPlayerCard)
        {
            gameManager.PlayerHandCards.Remove(this);
            gameManager.PlayerFieldCards.Add(this);
            gameManager.ReduceMana(true, Card.Manacost);
            //gameManager.ShowAvailableAttackCards();
        }
        else
        {
            gameManager.ReduceMana(false, Card.Manacost);
            Info.ShowCardInfo();
        }

        Card.IsPlaced = true;
        Unit = turnManager.activeUnit.GetComponent<UnitInfo>();
        UnitControl = turnManager.activeUnit.GetComponent<UnitControl>();

        UseSpell(null);
        UiController.Instance.UpdateMana();
    }

    public void UseSpell(CardController target)
    {
        var spellCard = Card;

        switch (spellCard.EndStance)
        {
            case Card.Stance.Defensive:
                Unit.ChangeStance(Stance.Defensive);
                break;
            case Card.Stance.Advance:
                Unit.ChangeStance(Stance.Advance);
                break;
            case Card.Stance.Attacking:
                Unit.ChangeStance(Stance.Attacking);
                break;
            case Card.Stance.Raging:
                Unit.ChangeStance(Stance.Raging);
                break;
        }
        switch (spellCard.FirstCardEff)
        {
            case Card.CardEffect.Damage://confirmed
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue));
                break;

            case Card.CardEffect.DamagePlusMovement:// скорее всего будут вместе срабатывать, нужно добавить бул перемнную в метод атаки
                {
                    turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue));

                    turnManager.AddAction(new Action(ActionType.Move, spellCard.SpellValue));
                }
                break;

            case Card.CardEffect.PlusDamageCard: // нужно добавить обнуление numberCard в методе смены хода(он пока у нас не робит)
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue + numberCard));
                break;

            case Card.CardEffect.Defense:// confirmed
                Unit.defence += spellCard.SpellValue;
                break;

            case Card.CardEffect.CheckDefenseStance: // нужно допилить
                break;

            case Card.CardEffect.DefensePlusType:
                {
                    Unit.defence += spellCard.SpellValue;
                    if (Unit.withShield)
                        Unit.defence += 1;
                }
                break;

            case Card.CardEffect.Survived:
                Unit.CheckForAlive();
                break;

            case Card.CardEffect.Movement:// confirmed
                turnManager.AddAction(new Action(ActionType.Move, spellCard.SpellValue));
                break;
        }
        switch (spellCard.FirstCardEffTwo)
        {
            case Card.CardEffect.Damage:// confirmed
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.IfDamage:
                break;

            case Card.CardEffect.Movement:// confirmed
                turnManager.AddAction(new Action(ActionType.Move, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.CardDrow:// confirmed
                turnManager.AddAction(new Action(ActionType.Draw, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.AliveCardDrow:
                if(Unit.CheckForAlive())
                    turnManager.AddAction(new Action(ActionType.Draw, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.IfCardDrow:
                break;

            case Card.CardEffect.ResetCard:
                break;

            case Card.CardEffect.ManaAdd:
                {
                    Game.CurrentGame.Player.SpellManapool();
                    UiController.Instance.UpdateMana();
                }
                break;

            case Card.CardEffect.Type:
                if (Unit.withShield)
                    Unit.defence += spellCard.SecondSpellValue;
                break;

            case Card.CardEffect.Mechanics:
                break;

            case Card.CardEffect.No:
                break;
        }

        DiscardCard();
    }

    public void DiscardCard() // Дестрой карты работает криво
    {
        Movement.OnEndDrag(null);

        RemoveCardFromList(gameManager.PlayerFieldCards);
        RemoveCardFromList(gameManager.PlayerHandCards);
        gameManager.CurrentGame.Player.DiscardPile.Add(this.Card);

        Destroy(gameObject);
    }

    void RemoveCardFromList(List<CardController> list)
    {
        if (list.Exists(x => x == this))
            list.Remove(this);
    }
}