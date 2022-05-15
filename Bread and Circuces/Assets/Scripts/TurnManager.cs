using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Team currTeam;
    private Team teamWithInitiative;
    private GameManagerScript gameManager;

    public GameObject activeUnit = null;

    public int Turn = 0, activationNum = 0;
    int TurnTime = 60;

    void Start()
    {
        gameManager = FindObjectOfType<GameManagerScript>();
        teamWithInitiative = (Team)Random.Range(0, 1);
        currTeam = teamWithInitiative;

        StartCoroutine(TurnFunc());
    }

    public Team GetCurrTeam()
    {
        return currTeam;
    }

    public void SetCurrTeam(Team team)
    {
        currTeam = team;
    }

    public IEnumerator TurnFunc()
    {
        TurnTime = 60;
        UiController.Instance.UpdateTurnTime(TurnTime);

        gameManager.CheckCardsForManaAvaliability();

        if (gameManager.IsPlayerTurn)
        {
            while (TurnTime-- > 0)
            {
                UiController.Instance.UpdateTurnTime(TurnTime);
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            while (TurnTime-- > 0)
            {
                UiController.Instance.UpdateTurnTime(TurnTime);
                yield return new WaitForSeconds(1);
            }
        }
        EndPlayerActivation();
    }

    bool CheckIfPlayerCanActivate(Player player)
    {
        return player.activatedUnits < 2 && player.Mana != 0;
    }

    public void EndPlayerActivation()
    {
        activationNum++;
        StopAllCoroutines();

        UiController.Instance.DisableTurnBtn();

        if (currTeam == Team.Player)
        {
            gameManager.CurrentGame.Player.activatedUnits++;
            Debug.Log("Player units activated:" + gameManager.CurrentGame.Player.activatedUnits);
        }
        else if (currTeam == Team.Enemy)
        {
            gameManager.CurrentGame.Enemy.activatedUnits++;
            Debug.Log("Enemy units activated:" + gameManager.CurrentGame.Enemy.activatedUnits);
        }

        var playerCanActivate = CheckIfPlayerCanActivate(gameManager.CurrentGame.Player);
        var enemyCanActivate = CheckIfPlayerCanActivate(gameManager.CurrentGame.Enemy);

        activeUnit.GetComponent<UnitControl>().DeactivateFigure();

        if (enemyCanActivate && (currTeam == Team.Player || !playerCanActivate))
        {
            currTeam = Team.Enemy;
        }
        else if (playerCanActivate && (currTeam == Team.Enemy || !enemyCanActivate))
        {
            currTeam = Team.Player;
        }

        if (playerCanActivate || enemyCanActivate)
        {
            StartCoroutine(TurnFunc());
        }
        else ChangeTurn();
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;
        activationNum = 0;
        currTeam = teamWithInitiative;

        UiController.Instance.DisableTurnBtn();

        gameManager.GiveNewCards();
        gameManager.CurrentGame.Player.RestoreRoundMana();
        gameManager.CurrentGame.Player.activatedUnits = 0;
        gameManager.CurrentGame.Enemy.RestoreRoundMana();
        gameManager.CurrentGame.Enemy.activatedUnits = 0;

        UiController.Instance.UpdateMana();

        StartCoroutine(TurnFunc());
    }

    public bool ActiveUnitExist()
    {
        return activeUnit != null;
    }

    public void ClearActiveUnit()
    {
        activeUnit = null;
    }

    public void SetActiveUnit(GameObject unit)
    {
        activeUnit = unit;
    }

    public GameObject GetActiveUnit()
    {
        return activeUnit;
    }
}
