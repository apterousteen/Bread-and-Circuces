using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTutorialManager : MonoBehaviour
{
    public static ChoiceTutorialManager Instance;

    public GameObject charPanel;
    public GameObject cardInfoPanel;
    private bool showedCardsPanel;

    private void Awake()
    {

        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        showedCardsPanel = false;
    }

    public void MakeCardInfoPanelActiveOnce()
    {
        if(!showedCardsPanel)
        {
            cardInfoPanel.SetActive(true);
            showedCardsPanel = true;
        }
    }

    public void MakeCharactersClickable()
    {
        for (var i = 0; i < charPanel.transform.childCount; i++)
        {
            charPanel.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
        }
    } 
}
