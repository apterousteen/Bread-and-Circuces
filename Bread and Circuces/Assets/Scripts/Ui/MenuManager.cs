using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public CharInfoPanel CIP;

    public GameObject Popup;
    public GameObject CharPanel;
    public TextMeshProUGUI InTeam;
    public Button ChooseButton;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public List<string> team = new List<string>(); 
    public string chosen; 

    public void AddToTeam()
    {
        if (team.Count == 1)
        {
            ChooseButton.interactable = false;
        }

        if (!team.Contains(chosen))
        {
            team.Add(chosen);
            InTeam.text = team.Count.ToString();
        }
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
                charInfo.charObj.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                charInfo.charObj.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                continue;
            }

            chosen = charInfo.charTag;
            Debug.Log(chosen + " is chosen");

            charInfo.charObj.GetComponent<Image>().color = new Color(0, 1, 1, 1);
            charInfo.charObj.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);

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
