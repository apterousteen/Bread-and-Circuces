using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (!GameManagerScript.Instance.IsPlayerTurn)
            return;

        CardController spell = eventData.pointerDrag.GetComponent<CardController>(),
                       target = GetComponent<CardController>();

        if (spell && spell.IsPlayerCard && spell.Card.IsPlaced &&
            GameManagerScript.Instance.CurrentGame.Player.Mana >= spell.Card.Manacost)
        {
            var spellCard = spell.Card;

            if ((spellCard.SpellTarget == Card.TargetType.This && target.IsPlayerCard) ||
                (spellCard.SpellTarget == Card.TargetType.Enemy && !target.IsPlayerCard))
            {
                GameManagerScript.Instance.ReduceMana(true, spell.Card.Manacost);
                spell.UseSpell(target);
                //GameManagerScript.Instance.ShowAvailableAttackCards();
            }
        }
    }
}
