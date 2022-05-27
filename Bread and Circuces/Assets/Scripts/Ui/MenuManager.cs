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

    public static List<string> team = new List<string>(); 
    public static GameObject chosen; 

    public void AddToTeam()
    {
        if (!team.Contains(chosen.tag))
        {
            team.Add(chosen.tag);
            chosen.transform.GetChild(3).GetComponent<Image>().sprite = checkbox_ch;
            InTeam.text = team.Count.ToString();
        }
        if (team.Count == 2)
        {
            ChooseButton.interactable = false;
            RunInfo.Instance.Player.units.SelectUnits(team[0], team[1]);
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

    /// game screen
    public void Pause()
    {
        UiController.Instance.pausePopup.SetActive(true);
        Time.timeScale = 0f;
        UiController.Instance.GameIsPaused = true;
    }

    public void Resume()
    {
        UiController.Instance.pausePopup.SetActive(false);
        Time.timeScale = 1f;
        UiController.Instance.GameIsPaused = false;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("choiceMenu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
