using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AI_stuff;
using Card;
using Meta;
using Ui;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;

    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand, PlayerCardPanel, PlayerInfoPanel, EnemyCardPanel, EnemyInfoPanel, PlayerDiscardPanel;
    public CardInfoScript CardInfo;
    public Card.Card card;
    public GameObject CardPref;
    public int level;

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
        switch(level)
        {
            case 0:
                StartGame();
                break;

            case 1:
                StartTutorial01();
                break;

            case 2:
                StartTutorial02();
                break;
        }
    }

    private void Update()
    {
        playerDeckSize = CurrentGame.Player.Deck.Count;
        if (CurrentGame.Enemy.units.unitsAlive == 0)
            MenuManager.Instance.CheckWinCondition();
    }

    void StartTutorial01()
    {
        Turn = 0;
        CurrentGame = new Game(true);
        board.SpawnUnits(CurrentGame.Player);
        CurrentGame.Enemy.units.SelectUnits("Hoplomachus");
        board.SpawnUnits(CurrentGame.Enemy);
        var enemyUnits = FindObjectsOfType<UnitInfo>().Where(x => x.teamSide == Team.Enemy).ToList();
        foreach (var unit in enemyUnits)
            unit.gameObject.AddComponent<BasicUnitAI>();

        //GiveHandCards(CurrentGame.Enemy, EnemyHand);
        var handOfCards = new List<string>() { "Внезапный удар", "Прикрыться", "Завершающий рубец", "Удар клинком", "Прикрыться", "Блок" };
        GivePresetedHand(handOfCards, CurrentGame.Player, PlayerHand);
        UiController.Instance.StartGame();
        turnManager.StartActivity();
    }

    void StartTutorial02()
    {
        Turn = 0;
        CurrentGame = new Game(false);
        board.SpawnUnits(CurrentGame.Player);
        board.SpawnUnits(CurrentGame.Enemy);
        var enemyUnits = FindObjectsOfType<UnitInfo>().Where(x => x.teamSide == Team.Enemy).ToList();
        foreach (var unit in enemyUnits)
            unit.gameObject.AddComponent<BasicUnitAI>();

        //GiveHandCards(CurrentGame.Enemy, EnemyHand);
        var handOfCards = new List<string>();
        GivePresetedHand(handOfCards, CurrentGame.Player, PlayerHand);
        UiController.Instance.StartGame();
        turnManager.StartActivity();
    }

    void GivePresetedHand(List<string> cards, Player player, Transform hand)
    {
        foreach (var cardName in cards)
            GiveCard(player, hand, cardName);
    }

    void GiveCard(Player player, Transform hand, string Name)
    {
        var presetCard = player.Deck.Find(x => x.Name == Name);
        CreateCardPref(presetCard, hand);
    }

    void StartGame()
    {
        Turn = 0;
        CurrentGame = new Game(false);
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

     void CreateCardPref(Card.Card card, Transform hand)
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

    public void ShowPlayableCards(Card.Card.CardType type, UnitInfo unit)
    {
        if (type == Card.Card.CardType.Attack && CurrentGame.Player.Mana < 1)
        {
            MakeAllCardsUnplayable();
            return;
        }
        foreach (var card in CurrentGame.Player.HandCards)
        {
            var cardInfo = card.Card;
            if (cardInfo.Type == type && (cardInfo.StartStance.Contains(unit.currentStance) || unit.currentStance == Stance.Raging && cardInfo.StartStance.Contains(Stance.Attacking)) 
                && (cardInfo.Restriction == EnumCard.CardRestriction.Universal|| cardInfo.Restriction.ToString() == unit.gameObject.tag.ToString()))
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
