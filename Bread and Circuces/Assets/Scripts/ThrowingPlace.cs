using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ThrowingPlace : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if (card != null)
            card.DefaultParent = transform;
    }
}
