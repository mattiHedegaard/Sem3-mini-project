using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DiceIsGrabbed : XRGrabInteractable
{
    public bool isGrabbed;
    public bool diceRolled;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Check which interactor is interacting
        XRBaseInteractor interactor = args.interactor;

        // Check if the interactor has a tag for left or right controller
        if (interactor.CompareTag("LeftController"))
        {
            isGrabbed = true;
        }
        else if (interactor.CompareTag("RightController"))
        {
            isGrabbed = true;
        }
        else
        {
            isGrabbed = false;
        }
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isGrabbed = false;
        diceRolled = true;
    }
}
