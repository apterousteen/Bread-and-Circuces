using System.Collections.Generic;
using UnityEngine;

namespace Card
{
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
            LastCard(Card, gameManager.CurrentGame.Player, gameManager.PlayerCardPanel);
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
                case EnumCard.CardEffect.Damage://confirmed
                    turnManager.AddAction(new Action(ActionType.Attack, Team.Player, card.SpellValue));
                    break;

                case EnumCard.CardEffect.PushBackEnemy:
                {
                    turnManager.AddAction(new Action(ActionType.Attack, Team.Player, card.SpellValue));

                    turnManager.AddAction(new Action(ActionType.PushEnemy, Team.Player, card.SpellValue));
                }
                    break;

                case EnumCard.CardEffect.DamageFinisher: // ????? ???????? ????????? numberCard ? ?????? ????? ????(?? ???? ? ??? ?? ?????)
                    turnManager.AddAction(new Action(ActionType.FinisherAttack, Team.Player, card.SpellValue));
                    break;

                case EnumCard.CardEffect.Defense:// confirmed
                    unit.defence += card.SpellValue;
                    break;

                case EnumCard.CardEffect.ShieldedDefense:
                {
                    unit.defence += card.SpellValue;
                    if (unit.withShield)
                        unit.defence += 1;
                }
                    break;

                case EnumCard.CardEffect.Movement:// confirmed
                    turnManager.AddAction(new Action(ActionType.Push, Team.Player, card.SpellValue));
                    break;

                case EnumCard.CardEffect.ChargeStart:
                    turnManager.AddAction(new Action(ActionType.ChargeStart, Team.Player, card.SpellValue));
                    break;

                case EnumCard.CardEffect.DiscardSelf:
                    turnManager.AddAction(new Action(ActionType.DiscardActivePlayer, Team.Player, card.SpellValue));
                    break;

                case EnumCard.CardEffect.DealRawDamage:
                    turnManager.AddAction(new Action(ActionType.DealRawDamage, Team.Player, card.SpellValue));
                    break;

                case EnumCard.CardEffect.ShieldedRush:
                    turnManager.AddAction(new Action(ActionType.Move, Team.Player, card.SpellValue));
                    if(turnManager.activeUnit.GetComponent<UnitInfo>().withShield)
                        turnManager.AddAction(new Action(ActionType.DiscardOpponent, Team.Player, card.SpellValue));
                    break;

                case EnumCard.CardEffect.RangedAttack:
                    turnManager.AddAction(new Action(ActionType.RangedAttack, Team.Player, card.SpellValue));
                    break;

                case EnumCard.CardEffect.DoubleDamage:
                    turnManager.AddAction(new Action(ActionType.DoubleDamage, Team.Player, card.SpellValue));
                    break;
                
            }
            switch (card.FirstCardEffTwo)
            {
                case EnumCard.CardEffect.Damage:// confirmed
                    turnManager.AddAction(new Action(ActionType.Attack, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.DamageAfterDiscard:
                    turnManager.AddAction(new Action(ActionType.AttackWithDiscardBuff, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.Movement:// confirmed
                    turnManager.AddAction(new Action(ActionType.Push, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.CardDrow:// confirmed
                    turnManager.AddAction(new Action(ActionType.Draw, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.AliveCardDrow:
                    turnManager.AddAction(new Action(ActionType.DrawIfAlive, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.Stun:
                    turnManager.AddAction(new Action(ActionType.ChangeEnemyStance, Team.Player, card.SecondSpellValue));
                    turnManager.AddAction(new Action(ActionType.DiscardOpponent, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.NearCardDrow:
                    turnManager.AddAction(new Action(ActionType.NearDraw, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.DiscardEnemy:
                    turnManager.AddAction(new Action(ActionType.DiscardOpponent, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.ManaAdd:
                {
                    gameManager.CurrentGame.Player.SpellManapool();
                    UiController.Instance.UpdateMana();
                }
                    break;

                case EnumCard.CardEffect.ChargeEnd:
                    turnManager.AddAction(new Action(ActionType.ChargeEnd, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.CancelCard:
                    turnManager.AddAction(new Action(ActionType.CancelCard, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.DealRawDamage:
                    turnManager.AddAction(new Action(ActionType.DealRawDamage, Team.Player, card.SecondSpellValue));
                    break;

                case EnumCard.CardEffect.WhirlwindDamage:
                    turnManager.AddAction(new Action(ActionType.WhirlwindDamage, Team.Player, card.SpellValue));
                    break;

                case EnumCard.CardEffect.No:
                    turnManager.AddAction(new Action(ActionType.Skip, Team.Player, card.SecondSpellValue));
                    break;
            }

            DiscardCard();
        }

        public void DiscardCard()
        {
            gameManager.CurrentGame.Player.DiscardPile.Add(Card);
            GiveCardToDiscardPile(Card, gameManager.CurrentGame.Player, gameManager.PlayerDiscardPanel);
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
}