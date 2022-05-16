using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SelfHand,
    Field
}
public class DropPlaceScript : MonoBehaviour, IDropHandler
{
    public FieldType Type;
    public void OnDrop(PointerEventData eventData)
    {
        CardController card = eventData.pointerDrag.GetComponent<CardController>();

        if (card && GameManagerScript.Instance.IsPlayerTurn &&
            GameManagerScript.Instance.CurrentGame.Player.Mana >= card.Card.Manacost && !card.Card.IsPlaced)
            card.OnCast();
    }
}
