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
    private bool isTurnEndButton;

    /// from determined
    [Header("Popups")]
    [SerializeField] private GameObject winPopup = null;
    [SerializeField] private GameObject failPopup = null;
    [SerializeField] private GameObject pausePopup = null;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    /// from determined
    public static bool GameIsPaused = false;
    public void Pause()
    {
        pausePopup.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pausePopup.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("choiceMenu");
    }
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
        EndTurnBtn.interactable = true;
        isTurnEndButton = true;
        UpdateMana();
    }

    public void UpdateMana()
    {
        Debug.Log(GameManagerScript.Instance.CurrentGame.Player.Mana.ToString());
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
        var buttonText = EndTurnBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log("Text Changed");
        if (isTurnEndButton)
            buttonText = "END TURN";
        else buttonText = "PASS";
    }
}
