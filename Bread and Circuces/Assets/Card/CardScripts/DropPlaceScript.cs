using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlaceScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardController card = eventData.pointerDrag.GetComponent<CardController>();

        if (card && GameManagerScript.Instance.IsPlayerTurn && GameManagerScript.Instance.PlayerMana >= card.Card.Manacost &&
            !card.Card.IsPlaced)
        {
            card.Movement.DefaultParent = transform;
            card.OnCast();
        }
    }
}
