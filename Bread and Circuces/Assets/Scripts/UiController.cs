using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class UiController : MonoBehaviour
{
    public static UiController Instance;

    public TextMeshProUGUI PlayerMana, EnemyMana;

    public TextMeshProUGUI TurnTime;
    public Button EndTurnBtn;
    public GameObject discardWindow;
    private bool isTurnEndButton;

    public TurnManager turnManager;
    public TextMeshProUGUI playerName, playerHealth, playerAttac, playerMove;
    public TextMeshProUGUI enemyName, enemyHealth, enemyAttac, enemyMove;
    public GameObject playerStance;
    public GameObject enemyStance;

    public GameObject player1;
    public GameObject player2;
    public GameObject enemy1;
    public GameObject enemy2;

    public TextMeshProUGUI turnText;
    public Image timerOutline;

    public string[] enemies;
    public string[] players;

    [Serializable]
    public struct stanceSprites
    {
        public string name;
        public Sprite image;
    }
    public stanceSprites[] stances;

    [Serializable]
    public struct iconSprites
    {
        public string name;
        public Sprite image;
    }
    public stanceSprites[] icons;

    /// from determined
    [Header("Popups")]
    public GameObject winPopup;
    public GameObject failPopup;
    public GameObject pausePopup;

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
 
        DontDestroyOnLoad(this);
        
    }

    public void FindUI()
    {

        PlayerMana = GameObject.Find("PlayerMana").GetComponent<TextMeshProUGUI>();
        EnemyMana = GameObject.Find("EnemyMana").GetComponent<TextMeshProUGUI>();
        TurnTime = GameObject.Find("TurnTimeTXT").GetComponent<TextMeshProUGUI>(); 
        EndTurnBtn = GameObject.Find("EndTurnBtn").GetComponent<Button>(); 
        discardWindow = GameObject.Find("Discard Panel");
        discardWindow.SetActive(false);
        winPopup = GameObject.Find("WinPanel");
        winPopup.SetActive(false);
        failPopup = GameObject.Find("FailPanel");
        failPopup.SetActive(false);
        pausePopup = GameObject.Find("PausePanel");
        pausePopup.SetActive(false);
        turnManager = FindObjectOfType<TurnManager>();

        playerName = GameObject.Find("playerName").GetComponent<TextMeshProUGUI>();
        playerHealth = GameObject.Find("playerHealth").GetComponent<TextMeshProUGUI>();
        playerAttac = GameObject.Find("playerAttac").GetComponent<TextMeshProUGUI>();
        playerMove = GameObject.Find("playerMove").GetComponent<TextMeshProUGUI>();
        enemyName = GameObject.Find("enemyName").GetComponent<TextMeshProUGUI>();
        enemyHealth = GameObject.Find("enemyHealth").GetComponent<TextMeshProUGUI>();
        enemyAttac = GameObject.Find("enemyAttac").GetComponent<TextMeshProUGUI>();
        enemyMove = GameObject.Find("enemyMove").GetComponent<TextMeshProUGUI>();
        playerStance = GameObject.Find("playerStance");
        enemyStance = GameObject.Find("enemyStance");
        player1 = GameObject.Find("player1");
        enemy1 = GameObject.Find("enemy1");
        player2 = GameObject.Find("player2");
        enemy2 = GameObject.Find("enemy2");

        turnText = GameObject.Find("turnText").GetComponent<TextMeshProUGUI>();
        timerOutline = GameObject.Find("timerOutline").GetComponent<Image>();
    }

    public bool GameIsPaused = false;

    public void StartGame()
    {
        FindUI();
        EndTurnBtn.interactable = true;
        isTurnEndButton = true;
        UpdateMana();

        enemies = GameManagerScript.Instance.CurrentGame.Enemy.units.units;
        players = GameManagerScript.Instance.CurrentGame.Player.units.units;
        UpdateIcons();
    }

    public void UpdateMana()
    {
        //Debug.Log(GameManagerScript.Instance.CurrentGame.Player.Mana.ToString());
        PlayerMana.text = GameManagerScript.Instance.CurrentGame.Player.Mana.ToString();
        EnemyMana.text = GameManagerScript.Instance.CurrentGame.Enemy.Mana.ToString();
    }

    public void UpdateTurnTime(int time)
    {
        TurnTime.text = time.ToString();
    }

    public void DisableTurnBtn()
    {
        EndTurnBtn.interactable = GameManagerScript.Instance.IsPlayerTurn;
    }

    public void ChangeEndButtonText()
    {
        isTurnEndButton = !isTurnEndButton;
        var buttonText = EndTurnBtn.GetComponentInChildren<TextMeshProUGUI>();
        if (isTurnEndButton)
            buttonText.text = "ХОД";
        else buttonText.text = "ПАС";
    }

    public void MakeDiscardWindowActive(bool active)
    {
        discardWindow.SetActive(active);
    }

    public void UpdateDiscardButton()
    {
        discardWindow.GetComponentInChildren<Button>().interactable = FindObjectOfType<DiscardWindow>().IsButtonActive();
    }

    public void UpdateSidePanel(GameObject unit)
    {
        if (unit.GetComponent<UnitInfo>().teamSide.ToString() == "Player")
        {
            playerName.text = unit.GetComponent<UnitInfo>().unitName;
            playerHealth.text = unit.GetComponent<UnitInfo>().health.ToString();

            foreach (var stance in stances)
            {
                if (stance.name == unit.GetComponent<UnitInfo>().currentStance.ToString())
                    playerStance.GetComponent<Image>().sprite = stance.image;
            }

            playerAttac.text = unit.GetComponent<UnitInfo>().attackReachDistance.ToString();
            playerMove.text = unit.GetComponent<UnitInfo>().moveDistance.ToString();
        }
        else if (unit.GetComponent<UnitInfo>().teamSide.ToString() == "Enemy")
        {
            enemyName.text = unit.GetComponent<UnitInfo>().unitName;
            enemyHealth.text = unit.GetComponent<UnitInfo>().health.ToString();

            foreach (var stance in stances)
            {
                if (stance.name == unit.GetComponent<UnitInfo>().currentStance.ToString())
                    enemyStance.GetComponent<Image>().sprite = stance.image;
            }

            enemyAttac.text = unit.GetComponent<UnitInfo>().attackReachDistance.ToString();
            enemyMove.text = unit.GetComponent<UnitInfo>().moveDistance.ToString();
        }
    }

    public void UpdateTurn()
    {
        if (turnManager.GetCurrTeam() == Team.Player)
        {
            turnText.text = "ваш ход";
            timerOutline.enabled = true;
        }
        else if (turnManager.GetCurrTeam() == Team.Enemy) 
        {
            turnText.text = "ход врага";
            timerOutline.enabled = false;
        }
    }

    public void UpdateIcons()
    {
        foreach (var icon in icons)
        {
            if (icon.name == players[0])
                player1.GetComponent<Image>().sprite = icon.image;

            if (icon.name == players[1])
                player2.GetComponent<Image>().sprite = icon.image;

            if (icon.name == enemies[0])
                enemy1.GetComponent<Image>().sprite = icon.image;

            if (icon.name == enemies[1])
                enemy2.GetComponent<Image>().sprite = icon.image;
        }
    }
}
