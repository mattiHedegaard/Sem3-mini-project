using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DiceIsGrabbed : XRGrabInteractable
{
    public bool isGrabbed;
    public bool diceRolled;
    public DiceController controller;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        //check which interactor(controler) is interacting
        XRBaseInteractor interactor = args.interactor;

        //check if the interactor(controller) has a tag for left or right controller
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
        //retracts the point and makes the dice ready to be rolled again
        base.OnSelectExited(args);
        isGrabbed = false;
        if (controller.enoughPoints)
        {
            diceRolled = true;
            controller.dicePoints -= 100;
            controller.enoughPoints = false;
        }
    }
}
