using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonHandler : MonoBehaviour
{
    public bool State = false;
    public bool ActivationState = false;
    public GameObject Button;

    void Start()
    {
        ActivateDeactivateButton(ActivationState);
    }

    public void ActivateDeactivateButton(bool activationStateIn)
    {
        ActivationState = activationStateIn;
        Button.SetActive(ActivationState);
    }

    public void resetButton()
    {
        State = false;
        ActivateDeactivateButton(false);
    }

    public void HandleClick()
    {
        State = !State;
    }
    
}
