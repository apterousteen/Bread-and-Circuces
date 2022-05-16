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
        if (IsPlayerCard)
        {
            gameManager.PlayerHandCards.Remove(this);
            gameManager.PlayerFieldCards.Add(this);
            gameManager.ReduceMana(true, Card.Manacost);
            gameManager.CheckCardsForManaAvaliability();
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
            case Card.CardEffect.Defense:
                Unit.defence += spellCard.SpellValue;
                break;

            case Card.CardEffect.Damage:
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue));
                break;

            case Card.CardEffect.Survived:
                Unit.CheckForAlive();
                break;

            case Card.CardEffect.Movement:
                turnManager.AddAction(new Action(ActionType.Move, spellCard.SpellValue));
                break;
        }
        switch (spellCard.FirstCardEffTwo)
        {
            case Card.CardEffect.No:
                break;

            case Card.CardEffect.Type:
                break;

            case Card.CardEffect.CardDrow:
                turnManager.AddAction(new Action(ActionType.Draw, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.Movement:
                turnManager.AddAction(new Action(ActionType.Move, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.Damage:
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SecondSpellValue));
                break;

            case Card.CardEffect.ResetCard:
                break;

            case Card.CardEffect.ManaAdd:
                {
                    Game.CurrentGame.Player.SpellManapool();
                    UiController.Instance.UpdateMana();
                }
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