using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScript : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name, Attack, Defense, Manacost;

    public void ShowCardInfo(Card card)
    {
        SelfCard = card;
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;

        RefreshData();
    }

    public void RefreshData()
    {
        Attack.text = SelfCard.Attack.ToString();
        Defense.text = SelfCard.Defense.ToString();
        Manacost.text = SelfCard.Manacost.ToString();
    }

    public void CheckForAvailability(int currentMana)
    {
        GetComponent<CanvasGroup>().alpha = currentMana >= SelfCard.Manacost ? 1 : .5f;

    }
}
