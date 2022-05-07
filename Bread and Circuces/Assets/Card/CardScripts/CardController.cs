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
        if (Card.IsSpell && ((SpellCard)Card).SpellTarget != SpellCard.TargetType.NO_TARGET)
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

        switch (spellCard.Spell)
        {
            case SpellCard.SpellType.AOE_HEAL: //AOE AOE_HEAL ALLY

                var allyCards = IsPlayerCard ?
                                gameManager.PlayerFieldCards :
                                gameManager.EnemyFieldCards;
                foreach (var card in allyCards)
                {
                    card.Card.Defense += spellCard.SpellValue;
                    card.Info.RefreshData();
                }

                break;

            case SpellCard.SpellType.AOE_DAMAGE:

                var enemyCards = IsPlayerCard ?
                                 new List<CardController>(gameManager.EnemyFieldCards) :
                                 new List<CardController>(gameManager.PlayerFieldCards);

                foreach (var card in enemyCards)
                    GiveDamageTo(card, spellCard.SpellValue);

                break;

            case SpellCard.SpellType.HEAL_ALLY_CARD:
                target.Card.Defense += spellCard.SpellValue;
                break;

            case SpellCard.SpellType.DAMAGE_TARGET:
                GiveDamageTo(target, spellCard.SpellValue);
                break;

            case SpellCard.SpellType.BUFF_CARD_DAMAGE:
                target.Card.Attack += spellCard.SpellValue;
                break;

            case SpellCard.SpellType.DEBUFF_CARD_DAMAGE:
                target.Card.Attack = Mathf.Clamp(target.Card.Attack - spellCard.SpellValue, 0, int.MaxValue);
                break;

            case SpellCard.SpellType.ARMOR:
                break;

            case SpellCard.SpellType.DRAW_CART:
                break;

        }

        if (target != null)
        {
            target.Ability.OnCast();
            target.CheckForAlive();
        }

        DestroyCard();
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
            DestroyCard();
    }

    public void DestroyCard() // ”ничтожаем карты или героев мб пригодитьс€ 
    {
        Movement.OnEndDrag(null);

        RemoveCardFromList(gameManager.PlayerFieldCards);
        RemoveCardFromList(gameManager.PlayerHandCards);

        Destroy(gameObject);
    }

    void RemoveCardFromList(List<CardController> list)
    {
        if (list.Exists(x => x == this))
            list.Remove(this);
    }
}
