using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class playerController : MonoBehaviour
{
    public InputActionProperty leftTrigger;
    public InputActionProperty rightTrigger;

    [SerializeField] GameObject rightController;
    [SerializeField] GameObject leftController;
    ActionBasedController leftXRController;
    ActionBasedController rightXRController;

    [SerializeField] SlingshotController slingshotController;

    private void Start()
    {
        leftXRController = leftController.GetComponent<ActionBasedController>();
        rightXRController = rightController.GetComponent<ActionBasedController>();
        if (rightXRController == null)
        {
            Debug.Log("right XR missing");
        }
        if (leftXRController == null)
        {
            Debug.Log("left XR missing");
        }
    }

    void Update()
    {
        float rightTriggerPressed = rightTrigger.action.ReadValue<float>();//float from 0 to 1, check for fully pressed with 1.
        float leftTriggerPressed = leftTrigger.action.ReadValue<float>(); //float from 0 to 1, check for fully pressed with 1.

        if (slingshotController != null)
        {
            if (slingshotController.isGrabbed)
            {
                   //next up make the other hand a rock, and make the slingshot work
            }
        }
    }


    
}
