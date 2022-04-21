using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlaceScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if (card)
        {
            card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScript>());
            card.GameManager.PlayerFieldCards.Add(card.GetComponent<CardInfoScript>());
            card.DefaultParent = transform;
        }
    }
}
