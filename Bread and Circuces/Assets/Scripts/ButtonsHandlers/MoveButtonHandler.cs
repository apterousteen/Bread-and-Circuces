using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveButtonHandler : ButtonHandler
{
	private ButtonsContainer buttonsContainer;

    void Start()
    {
		buttonsContainer = FindObjectOfType<ButtonsContainer>();
    }

	public new void HandleClick()
    {
        //if(!buttonsContainer.attackButton.State)
        //State = !State;
        var turnManager = FindObjectOfType<TurnManager>();
        FindObjectOfType<GameManagerScript>().ReduceMana(true, 1);
        turnManager.AddAction(new Action(ActionType.DiscardActivePlayer, Team.Player, 1));
        turnManager.AddAction(new Action(ActionType.Move, Team.Player, turnManager.activeUnit.GetComponent<UnitInfo>().moveDistance));
        turnManager.AddAction(new Action(ActionType.Draw, Team.Player, 1));
        turnManager.EndAction();
    }
}
