using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Agava.YandexGames;
using System;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public GameObject Popup;
    public GameObject CharPanel;
    public TextMeshProUGUI InTeam;
    public Button ChooseButton, PlayButton;

    public Sprite rage, defence;

    [Header("checkbox")]
    [SerializeField] private Sprite checkbox_ch = null;
    [SerializeField] private Sprite checkbox_unch = null;

    public AudioManager audioManager;

    public void LoadScene(string sceneName)
    {
        if (sceneName == "MainMenu")
            AudioManager.Instance.ChangeMusicOnMain();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevel()
    {
        AudioManager.Instance.ChangeMusicOnBattle();
        if (RunInfo.Instance.isTutorial)
            LoadScene("TutorialScene");
        else LoadScene("FightScene");
    }

    public static List<string> team = new List<string>(); 
    public static GameObject chosen;

    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void AddToTeam()
    {      
        if (!team.Contains(chosen.tag) && team.Count < 2)
        {
            team.Add(chosen.tag);
            chosen.transform.GetChild(3).GetComponent<Image>().sprite = checkbox_ch;
            InTeam.text = team.Count.ToString();
            PlayButton.interactable = false;
        }
        if (team.Count == 2)
        {
            PlayButton.interactable = true;
            RunInfo.Instance.Player.units.SelectUnits(team[0], team[1]);
        }

        ChangeChoiceButton();
    }

    public void DeleteFromTeam()
    {
        team.Remove(chosen.tag);
        chosen.transform.GetChild(3).GetComponent<Image>().sprite = checkbox_unch;
        InTeam.text = team.Count.ToString();

        ChangeChoiceButton();

        if (team.Count <= 2)
        {
            PlayButton.interactable = false;
        }

        if (team.Count == 2)
        {
            PlayButton.interactable = true;
            RunInfo.Instance.Player.units.SelectUnits(team[0], team[1]);
        }
    }

    public void ChangeChoiceButton()
    {
        if (team.Contains(chosen.tag))
        {
            ChooseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Œ“Ã≈Õ»“‹ ¬€¡Œ–";
            ChooseButton.onClick.AddListener(DeleteFromTeam);
        }
        else
        {
            ChooseButton.GetComponentInChildren<TextMeshProUGUI>().text = "¬€¡–¿“‹";
            ChooseButton.onClick.AddListener(AddToTeam);
        }
    }
    
    public void ResetTeam()
    {
        team.Clear();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NewMatch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("choiceMenu");
    }

    public void CheckWinCondition()
    {
        Debug.Log("Check");
        Time.timeScale = 0f;
        var playerUnitsNum = FindObjectsOfType<UnitInfo>().Where(x => x.teamSide == Team.Player).Count();
        var enemyUnitsNum = FindObjectsOfType<UnitInfo>().Where(x => x.teamSide == Team.Enemy).Count();
        Debug.Log("PLayer units: " + playerUnitsNum + ". Enemy units: " + enemyUnitsNum);
        if (playerUnitsNum == 0)
            UiController.Instance.failPopup.SetActive(true);//Lose
        if (enemyUnitsNum == 0)
            UiController.Instance.winPopup.SetActive(true);//Win
        else Time.timeScale = 1f;
    }

    public void ShowVideoAd()
    {
        VideoAd.Show();
    }

    public void ControlAudio()
    {
        if (audioManager.muted)
        {
            audioManager.Unmute();
        }
        else
        {
            audioManager.Mute();
        }
    }

    public void PlayButtonSound()
    {
        audioManager.Play("Choice");
    }
}
