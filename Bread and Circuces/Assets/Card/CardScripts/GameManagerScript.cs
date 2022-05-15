using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    //может перенести колоду и стопку сброса пр€мо в класс игрока(мб и руки)?
    //так в некоторых методах достаточно будет передавать только игрока, а не отдельно его колоду, руку и пр
    //а может вообще перенести в него некоторые методы?(добор и пр)
    public Player Player, Enemy;

    public Game()
    {
        Player = new Player();
        Player.team = Team.Player;
        Enemy = new Player();
        Enemy.team = Team.Enemy;

        Enemy.Deck = GiveDeckCard();
        Player.Deck = GiveDeckCard();
        Enemy.DiscardPile = new List<Card>();
        Player.DiscardPile = new List<Card>();        
    }


    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();

        for (int i = 0; i < 10; i++)
        {
            var card = CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)];

            if (card.IsSpell)
                list.Add(((SpellCard)card).GetCopy());
            else
                list.Add(card.GetCopy());
        }
        return list;
    }

    //List<Card> GiveDeckCard(Player player)
    //{
    //    List<Card> list = new List<Card>();
    //    foreach(var unit in player.band)
    //    {
    //        list.AddRange(unit.UnitDeck);
    //    }
    //    ShuffleDeck(list);
    //    return list;
    //}

    public void ShuffleDeck(List<Card> deck)
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            var value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }
}

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;

    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand;

    public GameObject CardPref;

    int Turn, TurnTime = 30;

    int StartHandSize = 6;

    public List<CardController> PlayerHandCards = new List<CardController>(),
                                PlayerFieldCards = new List<CardController>(),
                                EnemyFieldCards = new List<CardController>();
    private TurnManager turnManager;

    public bool IsPlayerTurn
    {
        get
        {
            return Turn % 2 == 0;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        StartGame();
    }

    void StartGame()
    {
        Turn = 0;
        CurrentGame = new Game();

        GiveHandCards(CurrentGame.Enemy.Deck, EnemyHand);
        GiveHandCards(CurrentGame.Player.Deck, PlayerHand);

        UiController.Instance.StartGame();
    }

    void GiveHandCards(List<Card> deck, Transform hand) //‘ункци€ выдачи стартовых карт в руку
    {
        int i = 0;
        while (i++ < StartHandSize)
            GiveCardToHand(deck, hand);
    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;
            //ReshufflDiscardPile(deck) - нужно эффективнее передавать дискард, без удал€ющихс€ карт он сейчас бесполезен и вызовет ошибку

        CreateCardPref(deck[0], hand);

        deck.RemoveAt(0);
    }


    void ReshuffleDiscardPile(Player player)
    {  //перенос сброса и колоды в класс игрока тут бы очень помог
        player.Deck.AddRange(player.DiscardPile);
        player.DiscardPile.Clear();
        CurrentGame.ShuffleDeck(player.Deck);
    }

    void CreateCardPref(Card card, Transform hand)
    {
        GameObject cardFF = Instantiate(CardPref, hand, false);
        CardController cardC = cardFF.GetComponent<CardController>();

        cardC.Init(card, hand == PlayerHand);

        if (cardC.IsPlayerCard)
            PlayerHandCards.Add(cardC);
    }

    void DrawFullHand(List<Card> deck, Transform hand) // вместо добора одной карты на начало хода добираетс§ полна§ рука из 6 карт
    {
        int i = PlayerHandCards.Count;
        Debug.Log("Players hand: " + i);
        while (i++ < StartHandSize)
            GiveCardToHand(deck, hand);
    }

    public void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.Enemy.Deck, EnemyHand);
        DrawFullHand(CurrentGame.Player.Deck, PlayerHand);
    }

    public void CardFight(CardController attacker, CardController defender)
    {
        defender.Card.GetDamage(attacker.Card.Attack);
        attacker.OnDamageDeal();
        defender.OnTakeDamage(attacker);

        attacker.Card.GetDamage(defender.Card.Attack);
        attacker.OnTakeDamage();

        attacker.CheckForAlive();
        defender.CheckForAlive();
    }


    public void ReduceMana(bool playerMana, int manacost)
    {
        if (playerMana)
            CurrentGame.Player.Mana -= manacost;
        else
            CurrentGame.Enemy.Mana -= manacost;

        UiController.Instance.UpdateMana();
    }

    public void CheckCardsForManaAvaliability()
    {
        foreach (var card in PlayerHandCards)
            card.Info.HiglightManaAvaliability(CurrentGame.Player.Mana);
    }

    public void HighlightTargets(CardController attacker, bool highlight)
    {
        List<CardController> targets = new List<CardController>();

        if (attacker.Card.IsSpell)
        {
            var spellCard = (SpellCard)attacker.Card;

            if (spellCard.SpellTarget == SpellCard.TargetType.NoTarget)
                targets = new List<CardController>();
            else if (spellCard.SpellTarget == SpellCard.TargetType.Ally)
                targets = PlayerFieldCards;
            else
                targets = EnemyFieldCards;
        }
        else
        {
            if (EnemyFieldCards.Exists(x => x.Card.IsProvocation))
                targets = EnemyFieldCards.FindAll(x => x.Card.IsProvocation);
            else
            {
                targets = EnemyFieldCards;
            }
        }

        foreach (var card in targets)
        {
            if (attacker.Card.IsSpell)
                card.Info.HighlightAsSpellTarget(highlight);
        }
    }
}
