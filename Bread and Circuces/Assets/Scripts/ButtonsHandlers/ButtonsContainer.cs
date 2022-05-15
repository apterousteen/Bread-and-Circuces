using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsContainer : MonoBehaviour
{
    public MoveButtonHandler moveButton;
    public AttackButtonHandler attackButton;

    void Start()
    { 
        moveButton = FindObjectOfType<MoveButtonHandler>();
        attackButton = FindObjectOfType<AttackButtonHandler>();
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

        return -1;
    }

    public void EndAction()
    {
        if(moveButton.State)
        {
            moveButton.State = !moveButton.State;
        }
        
        if(attackButton.State)
        {
            attackButton.State = !attackButton.State;
        }
    }    

    public void ActivateUnitButtons()
    {
        moveButton.ActivateDeactivateButton(true);
        attackButton.ActivateDeactivateButton(true);
    }
    
    public void DeactivateUnitButtons()
    {
        moveButton.resetButton();
        attackButton.resetButton();
    }
}
