using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScript : MonoBehaviour
{
    public CardController CC;

    public Image Logo;
    public TextMeshProUGUI Name, Attack, Defense, Manacost;

    public void ShowCardInfo()
    {
        Logo.sprite = CC.Card.Logo;
        Logo.preserveAspect = true;
        Name.text = CC.Card.Name;

        if (CC.Card.IsSpell)
        {
            Attack.gameObject.SetActive(false);
            Defense.gameObject.SetActive(false);
        }

        RefreshData();
    }

    public void RefreshData()
    {
        Attack.text = CC.Card.Attack.ToString();
        Defense.text = CC.Card.Defense.ToString();
        Manacost.text = CC.Card.Manacost.ToString();
    }

    public void HiglightManaAvaliability(int currentMana)
    {
        GetComponent<CanvasGroup>().alpha = currentMana >= CC.Card.Manacost ? 1 : .5f;

    }

    public void HighlightAsSpellTarget(bool higlight)
    {
        GetComponent<Image>().color = higlight ?
                                      Color.red :
                                      Color.green;
    }
}
