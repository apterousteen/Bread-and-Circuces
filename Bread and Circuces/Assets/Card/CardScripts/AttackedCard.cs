using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCard : MonoBehaviour
{
    public void OnDrop(PointerEventData eventData)
    {
        if (!GameManagerScript.Instance.IsPlayerTurn)
            return;

        CardController attacker = eventData.pointerDrag.GetComponent<CardController>(),
                       defender = GetComponent<CardController>();

        if (attacker &&
            attacker.Card.IsPlaced)
        {
            GameManagerScript.Instance.CardFight(attacker, defender);
        }
    }
}
