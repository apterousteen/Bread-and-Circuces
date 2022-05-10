using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButtonHandler : ButtonHandler
{
    private ButtonsContainer buttonsContainer;
    
    void Start()
    {
		buttonsContainer = FindObjectOfType<ButtonsContainer>();
    }

	public new void HandleClick()
    {
        State = !State;
        print("pressed");
    }

    public new void resetButton()
    {
        State = false;
    }
}
