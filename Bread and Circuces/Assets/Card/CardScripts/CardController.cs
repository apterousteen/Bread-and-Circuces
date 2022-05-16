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

    public bool IsPlayerCard;

    GameManagerScript gameManager;
    public void Init(Card card, bool isPlayerCard)
    {
        Card = card;
        gameManager = GameManagerScript.Instance;
        IsPlayerCard = isPlayerCard;

        if (isPlayerCard)
        {
            Info.ShowCardInfo();
        }
    }

    public void OnCast()
    {
        if (Card.IsSpell && ((SpellCard)Card).SpellTarget != SpellCard.TargetType.NoTarget)
            return;

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

        if (Card.IsSpell)
            UseSpell(null);

        UiController.Instance.UpdateMana();
    }

    public void UseSpell(CardController target)
    {
        gameManager.ReduceMana(true, Card.Manacost);

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
                UnitControl.TriggerAttack(spellCard.SpellValue);
                break;

            case SpellCard.FirstCardEffect.Survived:
                Unit.CheckForAlive();
                break;
        }
        switch (spellCard.SecondCardEff)
        {
            case SpellCard.SecondCardEffect.Type:
                break;

            case SpellCard.SecondCardEffect.CardDrow:

                break;

            case SpellCard.SecondCardEffect.Movement:
                UnitControl.TriggerMove(spellCard.SpellValue);
                break;

            case SpellCard.SecondCardEffect.ResetCard:
                break;

            case SpellCard.SecondCardEffect.ManaAdd:
                Game.CurrentGame.Player.SpellManapool();
                break;
        }

        

        if (target != null)
        {
            target.CheckForAlive();
        }

        DiscardCard();
        UiController.Instance.UpdateMana();
    }

    public void CheckForAlive()
    {
        if (Card.IsAlive)
            Info.RefreshData();
        else
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