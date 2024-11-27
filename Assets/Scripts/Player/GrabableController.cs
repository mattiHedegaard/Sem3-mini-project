using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabableController : XRGrabInteractable
{
    [Header("myScript")]
    public bool isGrabbed = false;
    public string hand = "none";
    public bool inInventory = false;
    public Vector3 scale;
    public float shrinkAmount = 0.25f;
    Rigidbody rb;
    bool touchingInInventory = false;

    private void Start()
    {
        scale = transform.localScale;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //adjusting the rigidbodys to be in the inventory 
        if (!inInventory)
        {
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            transform.localScale = scale;
        }
        else if (inInventory && !touchingInInventory)
        {
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            transform.localScale = scale * shrinkAmount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //scales it up from the inventory if hand hovers
        if (other.CompareTag("RightController") || other.CompareTag("LeftController"))
        {
            touchingInInventory = true;
            transform.localScale = scale;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightController") || other.CompareTag("LeftController"))
        {
            //Resets it to inventory size
            touchingInInventory = false;
            transform.localScale = scale * shrinkAmount;
        }
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        isGrabbed = true;

        // Check which interactor is interacting
        XRBaseInteractor interactor = args.interactor;

        // Check if the interactor has a tag for left or right controller
        if (interactor.CompareTag("LeftController"))
        {
            hand = "left";
        }
        else if (interactor.CompareTag("RightController"))
        {
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
