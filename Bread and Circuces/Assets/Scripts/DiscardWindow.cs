using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardWindow : MonoBehaviour
{
    private List<GameObject> cardsToDiscard;
    public int numforDiscard;

    public void SetNum(int num)
    {
        numforDiscard = num;
    }

    public void SelectCard(GameObject card)
    {
        if (cardsToDiscard.Count < numforDiscard)
        {
            cardsToDiscard.Add(card);
            card.GetComponent<CardInfoScript>().HiglightCard(true);
        }
        UiController.Instance.UpdateDiscardButton();
    }

    public void DeselectCard(GameObject card)
    {
        cardsToDiscard.Remove(card);
        card.GetComponent<CardInfoScript>().HiglightCard(false);
        UiController.Instance.UpdateDiscardButton();
    }

    public bool IsButtonActive()
    {
        return numforDiscard == cardsToDiscard.Count;
    }

    public void ConfirmChoice()
    {
        foreach(var card in cardsToDiscard)
        {
            card.GetComponent<CardController>().DiscardCard();
        }
        ResetCards();
        var turnManager =  FindObjectOfType<TurnManager>();
        turnManager.EndAction();
        turnManager.ContinueTurnCoroutine();
        UiController.Instance.MakeDiscardWindowActive(false);
    }

    public void SetCards()
    {
        cardsToDiscard = new List<GameObject>();
        var hand = FindObjectOfType<GameManagerScript>().PlayerHand;
        FindObjectOfType<GameManagerScript>().MakeAllCardsUnplayable();
        var cards = new List<Transform>();
        for(int i = 0; i < hand.childCount; i ++)
        {
            cards.Add(hand.GetChild(i));
        }
        foreach (var card in cards)
        {
            card.SetParent(transform);
            var cardScript = card.GetComponent<CardMovementScript>();
            cardScript.IsClickable = true;
            cardScript.IsDraggable = false;
            card.GetComponent<CardInfoScript>().HiglightCard(false);
        }
        FindObjectOfType<TurnManager>().StopAllCoroutines();
        UiController.Instance.UpdateDiscardButton();
    }

    private void ResetCards()
    {
        var hand = FindObjectOfType<GameManagerScript>().PlayerHand;
        var cards = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            cards.Add(transform.GetChild(i));
        }
        foreach (var card in cards)
        {
            card.SetParent(hand);
            var cardScript = card.GetComponent<CardMovementScript>();
            cardScript.IsClickable = false;
            cardScript.IsDraggable = true;
        }
    }
}
