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
        {
            Info.ShowCardInfo();
        }
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

        if (Card.IsSpell)
            UseSpell(null);

        UiController.Instance.UpdateMana();
    }

    public void UseSpell(CardController target)
    {
        var spellCard = (SpellCard)Card;

        switch (spellCard.StanceType)
        {
            case SpellCard.Stance.Defensive_Defensive:
                Unit.ChangeStance(Stance.Defensive);
                break;
            case SpellCard.Stance.Defensive_Advance:
                Unit.ChangeStance(Stance.Advance);
                break;
            case SpellCard.Stance.Defensive_Attacking:
                Unit.ChangeStance(Stance.Attacking);
                break;
            case SpellCard.Stance.Defensive_Raging:
                Unit.ChangeStance(Stance.Raging);
                break;
            case SpellCard.Stance.Advance_Defensive:
                Unit.ChangeStance(Stance.Defensive);
                break;
            case SpellCard.Stance.Advance_Advance:
                Unit.ChangeStance(Stance.Advance);
                break;
            case SpellCard.Stance.Advance_Attacking:
                Unit.ChangeStance(Stance.Attacking);
                break;
            case SpellCard.Stance.Advance_Raging:
                Unit.ChangeStance(Stance.Raging);
                break;
            case SpellCard.Stance.Attacking_Defensive:
                Unit.ChangeStance(Stance.Defensive);
                break;
            case SpellCard.Stance.Attacking_Advance:
                Unit.ChangeStance(Stance.Advance);
                break;
            case SpellCard.Stance.Attacking_Attacking:
                Unit.ChangeStance(Stance.Attacking);
                break;
            case SpellCard.Stance.Attacking_Raging:
                Unit.ChangeStance(Stance.Raging);
                break;
            case SpellCard.Stance.Raging_Defensive:
                Unit.ChangeStance(Stance.Defensive);
                break;
            case SpellCard.Stance.Raging_Advance:
                Unit.ChangeStance(Stance.Advance);
                break;
            case SpellCard.Stance.Raging_Attacking:
                Unit.ChangeStance(Stance.Attacking);
                break;
            case SpellCard.Stance.Raging_Raging:
                Unit.ChangeStance(Stance.Raging);
                break;
        }
        switch (spellCard.FirstCardEff)
        {
            case SpellCard.FirstCardEffect.Defense:
                Unit.defence += spellCard.SpellValue;
                break;

            case SpellCard.FirstCardEffect.Damage:
                turnManager.AddAction(new Action(ActionType.Attack, spellCard.SpellValue));
                break;

            case SpellCard.FirstCardEffect.Survived:
                Unit.CheckForAlive();
                break;

            case SpellCard.FirstCardEffect.Movement:
                turnManager.AddAction(new Action(ActionType.Move, spellCard.SpellValue));
                break;
        }
        switch (spellCard.SecondCardEff)
        {
            case SpellCard.SecondCardEffect.Type:
                break;

            case SpellCard.SecondCardEffect.CardDrow:
                turnManager.AddAction(new Action(ActionType.Draw, spellCard.SecondSpellValue));
                break;

            case SpellCard.SecondCardEffect.Movement:
                turnManager.AddAction(new Action(ActionType.Move, spellCard.SecondSpellValue));
                break;

            case SpellCard.SecondCardEffect.ResetCard:
                break;

            case SpellCard.SecondCardEffect.ManaAdd:
                {
                    Game.CurrentGame.Player.SpellManapool();
                    UiController.Instance.UpdateMana();
                }
                break;
        }

        DiscardCard();
    }

    public void DiscardCard() // ”ничтожаем карты или героев мб пригодитьс€ 
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