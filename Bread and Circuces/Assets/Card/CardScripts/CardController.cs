using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Card Card;

    public bool IsPlayerCard;

    public CardInfoScript Info;
    public CardMovementScript Movement;
    public CardAbility Ability;
    public UnitInfo Unit;
    public UnitControl UnitControl;

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

        if (Card.HasAbility)
            Ability.OnCast();

        if (Card.IsSpell)
            UseSpell(null);

        UiController.Instance.UpdateMana();
    }

    public void OnTakeDamage(CardController attacker = null)
    {
        CheckForAlive();
        Ability.OnDamageTake(attacker);
    }

    public void OnDamageDeal()
    {
        Card.TimesDealedDamage++;

        if (Card.HasAbility)
            Ability.OnDamadeDeal();
    }

    public void UseSpell(CardController target)
    {
        var spellCard = (SpellCard)Card;

        switch (spellCard.FirstCardEff)
        {
            case SpellCard.FirstCardEffect.Defense:
                Unit.defence += spellCard.SpellValue;
                break;

            case SpellCard.FirstCardEffect.Damage:
                //UnitControl.MakeAtack(spellCard.SpellValue);
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
                break;

            case SpellCard.SecondCardEffect.ResetCard:
                break;

            case SpellCard.SecondCardEffect.ManaAdd:
                break;
        }

        if (target != null)
        {
            target.Ability.OnCast();
            target.CheckForAlive();
        }

        DiscardCard();
    }

    void GiveDamageTo(CardController card, int damage)
    {
        card.Card.GetDamage(damage);
        card.CheckForAlive();
        card.OnTakeDamage();
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
        gameManager.CurrentGame.PlayerDiscardPile.Add(this.Card);

        Destroy(gameObject);
    }

    void RemoveCardFromList(List<CardController> list)
    {
        if (list.Exists(x => x == this))
            list.Remove(this);
    }
}