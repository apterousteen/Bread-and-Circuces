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

    public GameObject Popup;
    public GameObject CharPanel;
    public TextMeshProUGUI InTeam;
    public Button ChooseButton;

    [Header("checkbox")]
    [SerializeField] private Sprite checkbox_ch = null;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public List<string> team = new List<string>(); 
    public static GameObject chosen; 

    public void AddToTeam()
    {
        if (team.Count < 2)
        {
            if (!team.Contains(chosen.tag))
            {
                team.Add(chosen.tag);
                chosen.transform.GetChild(1).GetComponent<Image>().sprite = checkbox_ch;
                InTeam.text = team.Count.ToString();
            } 
        }
        else 
            ChooseButton.interactable = false;
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
