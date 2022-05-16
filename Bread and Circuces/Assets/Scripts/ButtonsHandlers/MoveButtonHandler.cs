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
        	State = !State;
    }
}
