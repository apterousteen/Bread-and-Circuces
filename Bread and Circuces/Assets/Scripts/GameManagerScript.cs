using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public Player Player, Enemy;

    public Game()
    {
        if(RunInfo.Instance != null)
        {
            RunInfo.Instance.isTutorial = false;
            Player = RunInfo.Instance.Player;
            Player.UpdateForNewGame();
        }
        else
        {
            Player = new Player();
            Player.team = Team.Player;
            Player.units.SelectUnits("Hoplomachus", "Murmillo");
        }

        Enemy = new Player();
        Enemy.team = Team.Enemy;

        GenerateUnits(Enemy);

        Enemy.Deck = GiveDeckCard(Enemy);
        Player.Deck = GiveDeckCard(Player);
        var allPlayerCards = Player.Deck.Count;
        var notUniversalCards = Player.Deck.Where(x => x.Restriction == CardRestriction.Scissor).Count();
        Enemy.DiscardPile = new List<Card>();
        Player.DiscardPile = new List<Card>();
    }

    void GenerateUnits(Player player)
    {
        var units = new string[4] { "Hoplomachus", "Murmillo", "Scissor", "Retiarius" };
        int first = Random.Range(0, 4);
        var second = -1;
        while (second == first || second < 0)
            second = Random.Range(0, 4);
        player.units.SelectUnits(units[first], units[second]);
    }

    List<Card> GiveDeckCard(Player player)
    {
        List<Card> list = new List<Card>();
        Debug.Log(CardManager.AllCards.Where(x => x.Restriction != CardRestriction.Universal).Count());
        foreach (var unit in player.units.units)
        {
            var unitDeck = CardManager.AllCards.Where(x => x.Set.ToString() == unit);
            foreach (var card in unitDeck)
                list.Add(card.GetCopy());
        }
        ShuffleDeck(list);
        return list;
    }

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
    public Transform EnemyHand, PlayerHand, PlayerCardPanel, PlayerInfoPanel, EnemyCardPanel, EnemyInfoPanel, PlayerDiscardPanel;
    public CardInfoScript CardInfo;
    public Card card;
    public GameObject CardPref;

    int Turn;

    public int StartHandSize = 6;
    public int playerDeckSize;
    public int enemyHandSize = 6;
    public int discardedCards;

    private TurnManager turnManager;
    private Board board;

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
        board = FindObjectOfType<Board>();
        StartGame();
    }

    private void Update()
    {
        playerDeckSize = CurrentGame.Player.Deck.Count;
        if (CurrentGame.Enemy.units.unitsAlive == 0)
            MenuManager.Instance.CheckWinCondition();
    }

    void StartGame()
    {
        Turn = 0;
        CurrentGame = new Game();
        board.SpawnUnits(CurrentGame.Player);
        board.SpawnUnits(CurrentGame.Enemy);
        var enemyUnits = FindObjectsOfType<UnitInfo>().Where(x => x.teamSide == Team.Enemy).ToList();
        foreach (var unit in enemyUnits)
            unit.gameObject.AddComponent<BasicUnitAI>();

        //GiveHandCards(CurrentGame.Enemy, EnemyHand);
        GiveHandCards(CurrentGame.Player, PlayerHand);
        UiController.Instance.StartGame();
        turnManager.StartActivity();
    }

    void GiveHandCards(Player player, Transform hand) //Функция выдачи стартовых карт в руку
    {
        int i = 0;
        while (i++ < StartHandSize)
            GiveCardToHand(player, hand);
    }

    void GiveCardToHand(Player player, Transform hand)
    {
        if (player.Deck.Count == 0)
            ReshuffleDiscardPile(player);

        CreateCardPref(player.Deck[0], hand);

        player.Deck.RemoveAt(0);
    }


    void ReshuffleDiscardPile(Player player)
    {
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
            CurrentGame.Player.HandCards.Add(cardC);
        else
            CurrentGame.Enemy.HandCards.Add(cardC);
        
    }

    void DrawFullHand(Player player, Transform hand)
    {
        int i = CurrentGame.Player.HandCards.Count;
        Debug.Log("Players hand: " + i);
        while (i++ < StartHandSize)
            GiveCardToHand(player, hand);
    }

    public void DrawCards(Team team, int num)
    {
        for (int i = 0; i < num; i++)
            if (team == Team.Player)
                GiveCardToHand(CurrentGame.Player, PlayerHand);
            else enemyHandSize++;
        MakeAllCardsUnplayable();
        turnManager.EndAction();
    }

    public void GiveNewCards()
    {
        enemyHandSize = 6;
        DrawFullHand(CurrentGame.Player, PlayerHand);
    }

    public void ReduceMana(bool playerMana, int manacost)
    {
        if (playerMana)
            CurrentGame.Player.Mana -= manacost;
        else
            CurrentGame.Enemy.Mana -= manacost;

        UiController.Instance.UpdateMana();
    }

    public void ShowPlayableCards(Card.CardType type, UnitInfo unit)
    {
        if (type == Card.CardType.Attack && CurrentGame.Player.Mana < 1)
        {
            MakeAllCardsUnplayable();
            return;
        }
        foreach (var card in CurrentGame.Player.HandCards)
        {
            var cardInfo = card.Card;
            if (cardInfo.Type == type && (cardInfo.StartStance == unit.currentStance || unit.currentStance == Stance.Raging && cardInfo.StartStance == Stance.Attacking) 
                && (cardInfo.Restriction == CardRestriction.Universal|| cardInfo.Restriction.ToString() == unit.gameObject.tag.ToString()))
            {
                card.Info.HiglightCard(true);
                card.Movement.canBePlayed = true;
            }
            else
            {
                card.Info.HiglightCard(false);
                card.Movement.canBePlayed = false;
            }

        }

    }

    public void MakeAllCardsUnplayable()
    {
        foreach (var card in CurrentGame.Player.HandCards)
        {
            card.Info.HiglightCard(false);
            card.Movement.canBePlayed = false;
        }
    }
}
