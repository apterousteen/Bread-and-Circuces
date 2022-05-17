using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public CharInfoPanel CIP;

    public GameObject Popup;
    public GameObject CharPanel;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnMouseDown()
    {
        Debug.Log(this.gameObject.tag + " Was Clicked");

        var heroes = new List<CharInfo>
        {
            CIP.charInfoRet,
            CIP.charInfoMurm,
            CIP.charInfoSkis,
            CIP.charInfoHoplo
        }; 

        foreach (var charInfo in heroes)
        {
            if (gameObject.tag != charInfo.charTag)
            {
                charInfo.cards.SetActive(false);
                continue;
            }
            
            CIP.charName.text = charInfo.charName;
            CIP.health.text = charInfo.health.ToString();
            CIP.moveDistance.text = charInfo.moveDistance.ToString();
            CIP.attackReachDistance.text = charInfo.attackReachDistance.ToString();
            CIP.info.text = charInfo.info;
            charInfo.cards.SetActive(true);
            CIP.cardPanel = charInfo.cards;
        }
    }

    public void OpenPopup()
    {
        Popup.SetActive(true);
        CharPanel.SetActive(false);
    }

    public void ClosePopup()
    {
        CharPanel.SetActive(true);
        Popup.SetActive(false);
    }
}
