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
        LastCard(this.Card, gameManager.CurrentGame.Player, gameManager.PlayerCardPanel);
        if (IsPlayerCard)
        {
            gameManager.CurrentGame.Player.HandCards.Remove(this);
            gameManager.ReduceMana(true, Card.Manacost);
        }
        else
        {
            gameManager.CurrentGame.Enemy.HandCards.Remove(this);
            gameManager.ReduceMana(false, Card.Manacost);
            Info.ShowCardInfo();
        }

        Card.IsPlaced = true;
        if(turnManager.currTeam == Team.Player)
            Unit = turnManager.activeUnit.GetComponent<UnitInfo>();
        else Unit = turnManager.targetUnit.GetComponent<UnitInfo>();

        if (turnManager.currTeam == Unit.teamSide)
            turnManager.playedCards++;

        UseSpell(Card, Unit);
        UiController.Instance.UpdateMana();
    }

    public void UseSpell(Card card, UnitInfo unit)
    {
        unit.ChangeStance(card.EndStance);
        switch (card.FirstCardEff)
        {
            case Card.CardEffect.Damage://confirmed
                turnManager.AddAction(new Action(ActionType.Attack, Team.Player, card.SpellValue));
                break;

            case Card.CardEffect.PushBackEnemy:
                {
                    turnManager.AddAction(new Action(ActionType.Attack, Team.Player, card.SpellValue));

                    turnManager.AddAction(new Action(ActionType.PushEnemy, Team.Player, card.SpellValue));
                }
                break;

            case Card.CardEffect.DamageFinisher: // нужно добавить обнуление numberCard в методе смены хода(он пока у нас не робит)
                turnManager.AddAction(new Action(ActionType.FinisherAttack, Team.Player, card.SpellValue));
                break;

            case Card.CardEffect.Defense:// confirmed
                unit.defence += card.SpellValue;
                break;

            case Card.CardEffect.ShieldedDefense:
                {
                    unit.defence += card.SpellValue;
                    if (unit.withShield)
                        unit.defence += 1;
                }
                break;

            case Card.CardEffect.Movement:// confirmed
                turnManager.AddAction(new Action(ActionType.Push, Team.Player, card.SpellValue));
                break;

            case Card.CardEffect.ChargeStart:
                turnManager.AddAction(new Action(ActionType.ChargeStart, Team.Player, card.SpellValue));
                break;

            case Card.CardEffect.DiscardSelf:
                turnManager.AddAction(new Action(ActionType.DiscardActivePlayer, Team.Player, card.SpellValue));
                break;
        }
        switch (card.FirstCardEffTwo)
        {
            case Card.CardEffect.Damage:// confirmed
                turnManager.AddAction(new Action(ActionType.Attack, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.DamageAfterDiscard:
                turnManager.AddAction(new Action(ActionType.AttackWithDiscardBuff, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.Movement:// confirmed
                turnManager.AddAction(new Action(ActionType.Push, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.CardDrow:// confirmed
                turnManager.AddAction(new Action(ActionType.Draw, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.AliveCardDrow:
                turnManager.AddAction(new Action(ActionType.DrawIfAlive, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.Stun:
                turnManager.AddAction(new Action(ActionType.ChangeEnemyStance, Team.Player, card.SecondSpellValue));
                turnManager.AddAction(new Action(ActionType.DiscardOpponent, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.NearCardDrow:
                turnManager.AddAction(new Action(ActionType.NearDraw, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.DiscardEnemy:
                turnManager.AddAction(new Action(ActionType.DiscardOpponent, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.ManaAdd:
                {
                    gameManager.CurrentGame.Player.SpellManapool();
                    UiController.Instance.UpdateMana();
                }
                break;

            case Card.CardEffect.ChargeEnd:
                turnManager.AddAction(new Action(ActionType.ChargeEnd, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.CancelCard:
                turnManager.AddAction(new Action(ActionType.CancelCard, Team.Player, card.SecondSpellValue));
                break;

            case Card.CardEffect.No:
                turnManager.AddAction(new Action(ActionType.Skip, Team.Player, card.SecondSpellValue));
                break;
        }

        DiscardCard();
    }

    public void DiscardCard()
    {
        gameManager.CurrentGame.Player.DiscardPile.Add(this.Card);
        GiveCardToDiscardPile(this.Card, gameManager.CurrentGame.Player, gameManager.PlayerDiscardPanel);
        Movement.OnEndDrag(null);
        RemoveCardFromList(gameManager.CurrentGame.Player.HandCards);

        Destroy(gameObject);
    }

    void GiveCardToDiscardPile(Card card, Player player, Transform discardPilePanel)
    {
        GameObject cardGG = Instantiate(gameManager.CardPref, discardPilePanel);
        CardController cardCard = cardGG.GetComponent<CardController>();
        cardCard.Init(card, true);
        /*
        GameObject cardGG = Instantiate(gameManager.CardPref, DiscardPilePanel);
        CardController cardCard = cardGG.GetComponent<CardController>();
        cardCard.Init(card, true);
        if (gameManager.CurrentGame.Player.DiscardPile.Count > oldList)
        {
            oldList = gameManager.CurrentGame.Player.DiscardPile.Count;
            GiveCardToDiscardPile(card, player, gameManager.PlayerCardPanel);
        }
        */
    }
    void LastCard(Card card, Player player, Transform playerCardPanel)
    {
        Destroy(gameObject);
        if(playerCardPanel.childCount != 0)
            Destroy(playerCardPanel.GetChild(0).gameObject);
        GameObject cardGG = Instantiate(gameManager.CardPref, playerCardPanel);
        CardController cardCard = cardGG.GetComponent<CardController>();
        cardCard.Init(card, true);
        cardCard.transform.localScale += new Vector3(0.5f,0.7f,0);
        cardCard.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void RemoveCardFromList(List<CardController> list)
    {
        if (list.Exists(x => x == this))
            list.Remove(this);
    }
}