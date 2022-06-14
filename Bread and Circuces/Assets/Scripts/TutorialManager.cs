using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public GameObject actionsPanel;
    public GameObject turnOrderPanel;
    public GameObject defensePanel;
    public GameObject enemyOutlinePanel;
    public GameObject playerOutlinePanel;

    private bool actionsExplained = false;
    private bool turnOrderExplained = false;
    private bool defenseExplained = false;
    private bool enemyPanelExplained = false;

    private TurnManager turnManager;

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
    }

    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }

    private void Update()
    {
        if (!actionsExplained && turnManager.ActiveUnitExist())
        {
            actionsPanel.SetActive(true);
            Time.timeScale = 0f;
            UiController.Instance.GameIsPaused = true;
            actionsExplained = true;
            playerOutlinePanel.SetActive(false);
        }

        if(!turnOrderExplained && turnManager.activatedUnits.Count > 0 && !turnManager.ActiveUnitExist())
        {
            turnOrderPanel.SetActive(true);
            Time.timeScale = 0f;
            UiController.Instance.GameIsPaused = true;
            turnOrderExplained = true;
        }

        if(!defenseExplained && turnManager.isReactionTime && turnManager.currTeam == Team.Enemy)
        {
            defensePanel.SetActive(true);
            Time.timeScale = 0f;
            UiController.Instance.GameIsPaused = true;
            defenseExplained = true;
        }
    }

    public void ControlOutline(GameObject unit)
    {
        if (!enemyPanelExplained && unit.GetComponent<UnitInfo>().teamSide.ToString() == "Enemy")
        {
            enemyOutlinePanel.SetActive(false);
            enemyPanelExplained = true;
            playerOutlinePanel.SetActive(true);
        }
    }
}
