using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck;

    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();
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
    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand;
    public GameObject CardPref;
    int Turn, TurnTime = 30;
    public TextMeshProUGUI TurnTimeTxt;
    public Button EndTurnBtn;

    public List<CardInfoScript> PlayerHandCards = new List<CardInfoScript>(),
                                PlayerFieldCards = new List<CardInfoScript>();

    public bool IsPlayerTurn
    {
        get
        {
            return Turn % 2 == 0;
        }
    }

    private void Start()
    {
        Turn = 0;
        CurrentGame = new Game();

        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        StartCoroutine(TurnFunc());
    }

    void GiveHandCards(List<Card> deck, Transform hand) //Функция выдачи стартовых карт в руку
    {
        int i = 0;
        while (i++ < 6)
            GiveCardToHand(deck, hand);

    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;

        Card card = deck[0];

        GameObject cardFF = Instantiate(CardPref, hand, false);

        if (hand == PlayerHand)
        {
            cardFF.GetComponent<CardInfoScript>().ShowCardInfo(card);
            PlayerHandCards.Add(cardFF.GetComponent<CardInfoScript>()); // trable
        }
        // else
                //cardFF.GetComponent<CardInfoScript>().HideCardInf(card);

        deck.RemoveAt(0);
    }

    IEnumerator TurnFunc() // НАДО СДЕЛАТЬ ВЫДАЧУ КАЖДОМУ ИГРОКУ ОТДЕЛЬНО (сейчас обоим одновременно)
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();

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
            GiveNewCards();

        StartCoroutine(TurnFunc());
    }

    void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);

    }
}
