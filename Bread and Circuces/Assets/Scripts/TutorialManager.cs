using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public GameObject movementPanel;
    public GameObject discardPanel;
    public GameObject attackPanel;
    public GameObject apPanel;
    public GameObject endPanel;
    public GameObject defencePanel;
    public GameObject congratsPanel;

    public bool unitPicked = false;
    public bool discardedOnce = false;
    public bool movedToEnemy = false;
    public bool attackFinished = false;
    public bool inAdvanceFinished = false;
    public bool attacked = false;
    public bool turnEnded = false;
    public bool apShowed = false;

    private TurnManager turnManager;
    private GameManagerScript gameManager;
    private UiController uiController;

    private Transform startPosition;

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
        uiController = FindObjectOfType<UiController>();
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    private void Update()
    {
        if (!unitPicked && turnManager.ActiveUnitExist())
        {
            movementPanel.SetActive(true);
            //Time.timeScale = 0f;
            //UiController.Instance.GameIsPaused = true;
            unitPicked = true;
            startPosition = turnManager.activeUnit.transform.parent;
            //playerOutlinePanel.SetActive(false);
        }

        if(!discardedOnce && uiController.discardWindow.active)
        {
            discardPanel.SetActive(true);
            discardedOnce = true;
            startPosition = turnManager.activeUnit.transform.parent;
        }

        if(!movedToEnemy && startPosition != turnManager.activeUnit.transform.parent)
        {
            attackPanel.SetActive(true);
            movedToEnemy = true;
        }


        if(!inAdvanceFinished && apShowed && (turnManager.activeUnit.GetComponent<UnitInfo>().currentStance == Stance.Advance || gameManager.CurrentGame.Player.Mana == 0))
        {
            endPanel.SetActive(true);
            inAdvanceFinished = true;
        }

        if(!attacked && turnManager.isReactionTime)
        {
            defencePanel.SetActive(true);
            attacked = true;
        }

        if(!turnEnded && turnManager.Turn > 0)
        {
            congratsPanel.SetActive(true);
            turnEnded = true;
        }    
    }

    public void Showed()
    {
        apShowed = true;
    }

    //public void ControlOutline(GameObject unit)
    //{
    //    if (!enemyPanelExplained && unit.GetComponent<UnitInfo>().teamSide.ToString() == "Enemy")
    //    {
    //        enemyOutlinePanel.SetActive(false);
    //        enemyPanelExplained = true;
    //        playerOutlinePanel.SetActive(true);
    //    }
    //}
}
