using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_HAND,
    ENEMY_FIELD
}
public class DropPlaceScript : MonoBehaviour, IDropHandler
{
    public FieldType Type;
    public void OnDrop(PointerEventData eventData)
    {
        CardController card = eventData.pointerDrag.GetComponent<CardController>();

        if (card && GameManagerScript.Instance.IsPlayerTurn && GameManagerScript.Instance.CurrentGame.Player.Mana >= card.Card.Manacost &&
            !card.Card.IsPlaced)
        {
            if (!card.Card.IsSpell)
                card.Movement.DefaultParent = transform;

            card.OnCast();
        }
    }
}
