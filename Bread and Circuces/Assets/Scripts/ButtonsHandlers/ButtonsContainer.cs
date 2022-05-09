using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsContainer : MonoBehaviour
{
    public MoveButtonHandler moveButton;
    public AttackButtonHandler attackButton;
    public EndTurnButtonHandler endTurnButton;

    void Start()
    { 
        moveButton = FindObjectOfType<MoveButtonHandler>();
        attackButton = FindObjectOfType<AttackButtonHandler>();
        endTurnButton = FindObjectOfType<EndTurnButtonHandler>();
        DeactivateUnitButtons();
    }

    public int GetAction()
    {
        if(moveButton.State)
        {
            return 1;
        }

        if(attackButton.State)
        {
            return 2;
        }

        if (endTurnButton.State)
        {
            return 3;
        }

        return -1;
    }

    public void ActivateUnitButtons()
    {
        moveButton.ActivateDeactivateButton(true);
        endTurnButton.ActivateDeactivateButton(true);
        attackButton.ActivateDeactivateButton(true);
    }
    
    public void DeactivateUnitButtons()
    {
        moveButton.resetButton();
        endTurnButton.resetButton();
        attackButton.resetButton();
    }
}
