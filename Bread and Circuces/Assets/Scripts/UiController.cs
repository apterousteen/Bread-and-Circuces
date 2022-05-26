using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class UiController : MonoBehaviour
{
    public static UiController Instance;

    public TextMeshProUGUI PlayerMana, EnemyMana;

    public TextMeshProUGUI TurnTime;
    public Button EndTurnBtn;
    public GameObject discardWindow;
    private bool isTurnEndButton;

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

    public void HandleUI()
    {
        //PlayerMana = FindObjectsOfType<TextMeshProUGUI>().Where(x => x.gameObject.CompareTag("PlayerMana")).First();
        //EnemyMana = FindObjectsOfType<TextMeshProUGUI>().Where(x => x.gameObject.CompareTag("EnemyMana")).First();
        //TurnTime = FindObjectsOfType<TextMeshProUGUI>().Where(x => x.gameObject.CompareTag("TurnTimeTXT")).First();
        //EndTurnBtn = FindObjectsOfType<Button>().Where(x => x.gameObject.CompareTag("EndTurnBtn")).First();
        //discardWindow = GameObject.FindGameObjectWithTag("DiscardPanel");
        //discardWindow.SetActive(false);
        //winPopup = GameObject.FindGameObjectWithTag("WinPanel");
        //winPopup.SetActive(false);
        //failPopup = GameObject.FindGameObjectWithTag("FailPanel");
        //failPopup.SetActive(false);
        //pausePopup = GameObject.FindGameObjectWithTag("PausePanel");
        //pausePopup.SetActive(false);

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
    }

    /// from determined
    public bool GameIsPaused = false;

    /*
    public void OpenWinPopup()
    {

        if (resultBoard.levelWasWon)
        {
            Debug.Log("Level Was Won");
            FindObjectsOfType<Button>().Where(x => x.gameObject.tag == "Result Button").First().enabled = false;
            StartCoroutine(WaitAndShow(winPopup, 2.0f));
            resultBoard.levelWasWon = false;
        }//4 secs
    }

    IEnumerator WaitAndShow(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(true);
    }

        [Header("Menus")]
    [SerializeField] private GameObject failMenu = null;

    bool gameHasEnded;

    void Update()
    {
        if (healthValue == 0 && gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("fail screen");
            failMenu.SetActive(true);
        }
    */
    /// end of code from determined

    public void StartGame()
    {
        HandleUI();
        EndTurnBtn.interactable = true;
        isTurnEndButton = true;
        UpdateMana();
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
            buttonText.text = "END TURN";
        else buttonText.text = "PASS";
    }

    public void MakeDiscardWindowActive(bool active)
    {
        discardWindow.SetActive(active);
    }

    public void UpdateDiscardButton()
    {
        discardWindow.GetComponentInChildren<Button>().interactable = FindObjectOfType<DiscardWindow>().IsButtonActive();
    }
}
