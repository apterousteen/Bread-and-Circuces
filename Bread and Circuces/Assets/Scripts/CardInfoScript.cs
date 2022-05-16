using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScript : MonoBehaviour
{
    public CardController CC;

    public Image Logo;
    public TextMeshProUGUI Name, Manacost;

    public void ShowCardInfo()
    {
        Logo.sprite = CC.Card.Logo;
        Logo.preserveAspect = true;
        Name.text = CC.Card.Name;

        ManacostRefresh();
    }

    public void ManacostRefresh()
    {
        Manacost.text = CC.Card.Manacost.ToString();
    }

    public void HiglightManaAvaliability(int currentMana)
    {
        GetComponent<CanvasGroup>().alpha = currentMana >= CC.Card.Manacost ? 1 : .5f;
    }

    public void HiglightCardAvaliability(bool canBePlayed)
    {
        GetComponent<CanvasGroup>().alpha = canBePlayed ? 1 : .5f;
    }

    /* подсвет карты(пока не нужно)
    public void HighlightAsSpellTarget(bool higlight)
    {
        GetComponent<Image>().color = higlight ?
                                      Color.red :
                                      Color.green;
    }
    */
}
