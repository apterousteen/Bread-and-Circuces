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
        }
        else
        {
            gameManager.ReduceMana(false, Card.Manacost);
            Info.ShowCardInfo();
        }

        Card.IsPlaced = true;
        Unit = turnManager.activeUnit.GetComponent<UnitInfo>();
        UnitControl = turnManager.activeUnit.GetComponent<UnitControl>();

        UseSpell(Card, Unit);
        UiController.Instance.UpdateMana();
    }

    public void UseSpell(Card card, UnitInfo unit)
    {
        var spellCard = card;

        unit.ChangeStance(spellCard.EndStance);
        switch (spellCard.FirstCardEff)
        {
            case Card.CardEffect.Damage://confirmed
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue));
                break;

            case Card.CardEffect.DamagePlusMovement:// скорее всего будут вместе срабатывать, нужно добавить бул перемнную в метод атаки
                {
                    turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue));

                    turnManager.AddAction(new Action(ActionType.Push, spellCard.SpellValue));
                }
                break;

            case Card.CardEffect.PlusDamageCard: // нужно добавить обнуление numberCard в методе смены хода(он пока у нас не робит)
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue + numberCard));
                break;

            case Card.CardEffect.Defense:// confirmed
                unit.defence += spellCard.SpellValue;
                break;

            case Card.CardEffect.CheckDefenseStance: // нужно допилить
                break;

            case Card.CardEffect.DefensePlusType:
                {
                    unit.defence += spellCard.SpellValue;
                    if (unit.withShield)
                        unit.defence += 1;
                }
                break;

            case Card.CardEffect.Survived:
                unit.CheckForAlive();
                break;

            case Card.CardEffect.Movement:// confirmed
                turnManager.AddAction(new Action(ActionType.Push, spellCard.SpellValue));
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
                turnManager.AddAction(new Action(ActionType.Push, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.CardDrow:// confirmed
                turnManager.AddAction(new Action(ActionType.Draw, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.AliveCardDrow:
                if(unit.CheckForAlive())
                    turnManager.AddAction(new Action(ActionType.Draw, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.IfCardDrow:
                break;

            case Card.CardEffect.ResetCard:
                turnManager.AddAction(new Action(ActionType.DiscardOpponent, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.ManaAdd:
                {
                    Game.CurrentGame.Player.SpellManapool();
                    UiController.Instance.UpdateMana();
                }
                break;

            case Card.CardEffect.Type:
                if (unit.withShield)
                    unit.defence += spellCard.SecondSpellValue;
                break;

            case Card.CardEffect.Mechanics:
                break;

            case Card.CardEffect.No:
                break;
        }

        DiscardCard();
    }

    public void DiscardCard() 
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