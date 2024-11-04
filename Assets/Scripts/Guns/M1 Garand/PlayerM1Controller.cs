using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerM1Controller : MonoBehaviour
{
    [Header("controller Input")]
    public InputActionProperty leftTrigger;
    public InputActionProperty rightTrigger;

    [Header("Controllers")]
    [SerializeField] GameObject rightController;
    [SerializeField] GameObject leftController;
    ActionBasedController leftXRController;
    ActionBasedController rightXRController;

    [Header("M1GController")]
    [SerializeField] GrabableController grabableController;
    public Transform attachPoint;

    [Header("Hands")]
    [SerializeField] GameObject leftHandDefaultModel;
    [SerializeField] GameObject rightHandDefaultModel;

    PlayerController playerController;
    public float currentHandTriggerPressed;
    public ActionBasedController currentXRController;

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

        leftHandDefaultModel.SetActive(true);
        rightHandDefaultModel.SetActive(true);

        playerController = GetComponent<PlayerController>();

        currentHandTriggerPressed = 0f;
    }

    void Update()
    {
        float rightTriggerPressed = rightTrigger.action.ReadValue<float>();//float from 0 to 1, check for fully pressed with 1.
        float leftTriggerPressed = leftTrigger.action.ReadValue<float>(); //float from 0 to 1, check for fully pressed with 1.

        if (grabableController != null)
        {
            if (grabableController.isGrabbed)
            {
                
                
                if (grabableController.hand == "left")
                {
                    leftHandDefaultModel.SetActive(false);
                    currentHandTriggerPressed = leftTriggerPressed;
                    currentXRController = leftXRController;
                }
                else if (grabableController.hand == "right")
                {
                    rightHandDefaultModel.SetActive(false);
                    currentHandTriggerPressed = rightTriggerPressed;
                    currentXRController = rightXRController;
                }
                
            }
            else if (!playerController.holdingSomething)
            {
                leftHandDefaultModel.SetActive(true);
                rightHandDefaultModel.SetActive(true);
                currentHandTriggerPressed = 0f;
            }
            
        }
    }
}
