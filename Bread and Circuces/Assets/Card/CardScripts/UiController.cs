using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController Instance;

    public TextMeshProUGUI PlayerMana, EnemyMana;

    public TextMeshProUGUI TurnTime;
    public Button EndTurnBtn;
    private bool isTurnEndButton;

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

    public void StartGame()
    {
        EndTurnBtn.interactable = true;
        isTurnEndButton = true;
        UpdateMana();
    }

    public void UpdateMana()
    {
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
