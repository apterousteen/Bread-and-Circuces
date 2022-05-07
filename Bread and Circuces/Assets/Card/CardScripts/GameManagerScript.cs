using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck, EnemyDiscard, PlayerDiscard;
    //самих операций с дискардом в итоге не добавил, т.к. у нас в тестовом билде вроде нет удалени€ активных карт

    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();
        EnemyDiscard = new List<Card>();
        PlayerDiscard = new List<Card>();
    }

    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        return list;
    }
}

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;

    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand;
    public GameObject CardPref;
    int Turn, TurnTime = 30;
    public TextMeshProUGUI TurnTimeTxt;
    public Button EndTurnBtn;

    public int PlayerMana = 4, EnemyMana = 4;
    public int StartHandSize = 6;
    public TextMeshProUGUI PlayerManaTxt, EnemyManaTxt;

    public List<CardController> PlayerHandCards = new List<CardController>(),
                                PlayerFieldCards = new List<CardController>();

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

    private void Start()
    {
        Turn = 0;
        CurrentGame = new Game();

        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        ShowMana();

        StartCoroutine(TurnFunc());
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

        CreateCardPref(deck[0], hand);

        deck.RemoveAt(0);
    }

    void CreateCardPref(Card card, Transform hand)
    {
        GameObject cardFF = Instantiate(CardPref, hand, false);
        CardController cardC = cardFF.GetComponent<CardController>();

        cardC.Init(card, hand == PlayerHand);

        if (cardC.IsPlayerCard)
            PlayerHandCards.Add(cardC);
    }

    // к комменту ниже - выдача одновременно и так правильно, т.к. по задумке мана и руки у игроков обновл€ютс€ одновременно,
    // а передача хода между игроками происходит дл€ поочерЄдной активации юнитов на поле, вместо классического "сходи всеми в течение хода"

    IEnumerator TurnFunc() // Ќјƒќ —ƒ≈Ћј“№ ¬џƒј„”  ј∆ƒќћ” »√–ќ ” ќ“ƒ≈Ћ№Ќќ (сейчас обоим одновременно)
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();

        CheckCardsForManaAvaliability();

        if (IsPlayerTurn)
        {
            while (TurnTime-- > 0)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            while (TurnTime-- > 27)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        ChangeTurn();
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;

        EndTurnBtn.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
        {
            GiveNewCards();

            PlayerMana = EnemyMana = 4;
            ShowMana();
        }

        StartCoroutine(TurnFunc());
    }

    void DrawFullHand(List<Card> deck, Transform hand) // вместо добора одной карты на начало хода добираетс€ полна€ рука из 6 карт
    {
        int i = PlayerHandCards.Count;
        while (i++ < StartHandSize)
            GiveCardToHand(deck, hand);
    }

    void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        //GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
        DrawFullHand(CurrentGame.PlayerDeck, PlayerHand);
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

    void ShowMana()
    {
        PlayerManaTxt.text = PlayerMana.ToString();
        EnemyManaTxt.text = EnemyMana.ToString();
    }

    public void ReduceMana(bool playerMana, int manacost)
    {
        if (playerMana)
            PlayerMana = Mathf.Clamp(PlayerMana - manacost, 0, int.MaxValue);
        else
            EnemyMana = Mathf.Clamp(EnemyMana - manacost, 0, int.MaxValue);

        ShowMana();
    }

    public void CheckCardsForManaAvaliability()
    {
        foreach (var card in PlayerHandCards)
            card.Info.HiglightManaAvaliability(PlayerMana);
    }
}
