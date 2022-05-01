using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlaceScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if (card && card.GameManager.IsPlayerTurn && card.GameManager.PlayerMana >= card.GetComponent<CardInfoScript>().SelfCard.Manacost &&
            !card.GetComponent<CardInfoScript>().SelfCard.IsPlaced)
        {
            card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScript>());
            card.GameManager.PlayerFieldCards.Add(card.GetComponent<CardInfoScript>());
            card.DefaultParent = transform;

            card.GetComponent<CardInfoScript>().SelfCard.IsPlaced = true;
            card.GameManager.ReduceMana(true, card.GetComponent<CardInfoScript>().SelfCard.Manacost);
            card.GameManager.CheckCardsForAvaliability(); 
        }
    }
}
