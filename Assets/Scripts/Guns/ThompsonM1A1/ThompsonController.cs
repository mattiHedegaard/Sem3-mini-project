using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ThompsonController : XRGrabInteractable
{
    [Header("myScript")]
    public bool isGrabbed = false;
    public string hand = "none";
    bool inInventory;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Check which interactor is interacting
        XRBaseInteractor interactor = args.interactor;

        // Check if the interactor has a tag for left or right controller
        if (interactor.CompareTag("LeftController"))
        {
            isGrabbed = true;
            hand = "left";
        }
        else if (interactor.CompareTag("RightController"))
        {
            isGrabbed = true;
            hand = "right";
        }
        else
        {
            isGrabbed = false;
            hand = "none";
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isGrabbed = false;
    }

    public (bool, string) checkSlingshotGrabbedAndHand()
    {
        return (isGrabbed, hand);
    }
}
