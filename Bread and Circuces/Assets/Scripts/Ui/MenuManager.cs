using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Meta;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        public GameObject Popup;
        public GameObject CharPanel;
        public TextMeshProUGUI InTeam;
        public Button ChooseButton, PlayButton;

        public Sprite rage, defence;

        [Header("mini_panel_bg")]
        [SerializeField] private Sprite panel_chosen = null;
        [SerializeField] private Sprite panel_not_chosen = null;

        public string activePosition = "left";

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
            //if (RunInfo.Instance.isTutorial)
            if (SceneManager.GetActiveScene().name == "choiceMenuTutorial")
                LoadScene("TutorialScene");
            else LoadScene("FightScene");
        }

        public static List<string> team = new List<string>();

        public static Vector3 left;
        public static Vector3 right;
        public static CharInfo charInfo;
        public static GameObject chosen;
        public static GameObject chosenButton;

        private void Awake()
        {

            if (Instance == null)
                Instance = this;
            //audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        public void AddToTeam()
        {
            Debug.Log("AddToTeam Method Invoked");
            if (!team.Contains(chosen.tag) && team.Count < 2)
            {

                team.Add(chosen.tag);

                if (activePosition == "left")
                {
                    Debug.Log("Rendered on the left");
                    chosen.transform.GetChild(1).transform.localPosition = left;
                    charInfo.slotPosition = "left";
                    activePosition = "right";
                }
                else
                {
                    Debug.Log("Rendered on the right");
                    chosen.transform.GetChild(1).transform.localPosition = right;
                    charInfo.slotPosition = "right";
                    activePosition = "left";
                }

                chosenButton.transform.GetComponent<Image>().sprite = panel_chosen;
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
            Debug.Log("DeleteFromTeam Method Invoked");
            // added logic: left and right side
            Debug.Log("charinfo now " + charInfo.charName);
            Debug.Log("chosen now " + chosen);
            activePosition = charInfo.slotPosition;
            charInfo.slotPosition = "";

            team.Remove(chosen.tag);
            chosenButton.transform.GetComponent<Image>().sprite = panel_not_chosen;
            InTeam.text = team.Count.ToString();

            ChangeChoiceButton();
            PlayButton.interactable = false;

            //if (team.Count <= 2)
            //{
            //    PlayButton.interactable = false;
            //}

            //if (team.Count == 2)
            //{
            //    PlayButton.interactable = true;
            //    RunInfo.Instance.Player.units.SelectUnits(team[0], team[1]);
            //}
        }

        public void ChangeChoiceButton()
        {
            if (team.Contains(chosen.tag))
            {
                ChooseButton.interactable = true;
                ChooseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Отменить выбор";
                //ChooseButton.onClick.RemoveListener(AddToTeam);
                ChooseButton.onClick.RemoveAllListeners();
                ChooseButton.onClick.AddListener(DeleteFromTeam);
            }
            else
            {
                ChooseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Выбрать";
                //ChooseButton.onClick.RemoveListener(DeleteFromTeam);
                ChooseButton.onClick.RemoveAllListeners();
                ChooseButton.onClick.AddListener(AddToTeam);
                if (team.Count == 2)
                    ChooseButton.interactable = false;
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
            ResetTeam();
            if (SceneManager.GetActiveScene().name == "choiceMenu" || SceneManager.GetActiveScene().name == "choiceMenuTutorial")
            {
                ChooseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Выбрать";
                ChooseButton.onClick.AddListener(AddToTeam);
            }
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
            //VideoAd.Show();
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
}
