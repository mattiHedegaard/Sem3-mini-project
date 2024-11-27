using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class playerSlingshotController : MonoBehaviour
{
    [Header("controller Input")]
    public InputActionProperty leftTrigger;
    public InputActionProperty rightTrigger;

    [Header("Controllers")]
    [SerializeField] GameObject rightController;
    [SerializeField] GameObject leftController;
    ActionBasedController leftXRController;
    ActionBasedController rightXRController;

    [Header("SlingshotController")]
    [SerializeField] GrabableController grabableController;
    private GameObject loadedRock;
    public bool isRockLoaded = false;
    [SerializeField] float forceMultiplier = 50f;

    [Header("Hands")]
    [SerializeField] GameObject leftHandDefaultModel;
    [SerializeField] GameObject rightHandDefaultModel;
    [SerializeField] GameObject leftHandRockModel;
    [SerializeField] GameObject rightHandRockModel;

    [Header("slingshot objects")]
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;
    [SerializeField] GameObject leather;
    [SerializeField] GameObject rockPrefab;

    [Header("Haptic")]
    public float holdingHapticAmp;
    public float holdingHapticDurr;
    public float releaseHapticAmp;
    public float releaseHapticDurr;

    [SerializeField] PlayerController playerController;

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
        leftHandRockModel.SetActive(false);
        rightHandRockModel.SetActive(false);

        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        float rightTriggerPressed = rightTrigger.action.ReadValue<float>();//float from 0 to 1, check for fully pressed with 1.
        float leftTriggerPressed = leftTrigger.action.ReadValue<float>(); //float from 0 to 1, check for fully pressed with 1.
        float currentHandTriggerPressed = 0f;
        GameObject currentRockHand = null;
        ActionBasedController currentXRController = null;
        ActionBasedController currentRockXRController = null;


        if (grabableController != null)
        {
            if (grabableController.isGrabbed)
            {
                //sets the variable for which hand is used and creates a ronck in the other hand
                if (grabableController.hand == "left")
                {
                    rightHandDefaultModel.SetActive(false);
                    leftHandDefaultModel.SetActive(false);
                    if (!isRockLoaded) rightHandRockModel.SetActive(true); else rightHandRockModel.SetActive(false);
                    currentHandTriggerPressed = rightTriggerPressed;
                    currentRockHand = rightController;
                    currentXRController = leftXRController;
                    currentRockXRController = rightXRController;
                }
                else if (grabableController.hand == "right")
                {
                    leftHandDefaultModel.SetActive(false);
                    rightHandDefaultModel.SetActive(false);
                    if (!isRockLoaded) leftHandRockModel.SetActive(true); else leftHandRockModel.SetActive(false);
                    currentHandTriggerPressed = leftTriggerPressed;
                    currentRockHand = leftController;
                    currentXRController = rightXRController;
                    currentRockXRController = leftXRController;
                }

            }
            else if (!playerController.holdingSomething)
            {
                leftHandDefaultModel.SetActive(true);
                rightHandDefaultModel.SetActive(true);
                leftHandRockModel.SetActive(false);
                rightHandRockModel.SetActive(false);
                currentHandTriggerPressed = 0f;
                currentRockHand = null;
                currentXRController = null;
                currentRockXRController = null;
            }



            if (!isRockLoaded && isNearLeatherChild(currentRockHand) && currentHandTriggerPressed != 0f)
            {
                createRock(); //creates rock if close enough to the slingshot
            }
            else if (isRockLoaded)
            {
                //makes the ronck follow the player hand
                pullRockBack(currentHandTriggerPressed, currentRockHand, currentXRController);
                currentRockXRController.SendHapticImpulse(holdingHapticAmp, holdingHapticDurr);
            }
        }
    }

    private bool isNearLeatherChild(GameObject controller)
    {
        if (controller != null)
        {
            //checks the distance between the hand and the leather of the slingshot
            float distance = Vector3.Distance(controller.transform.position, leather.transform.position);
            float distanceThreshold = 0.1f;

            return distance < distanceThreshold;
        }
        return false;
    }

    void createRock()
    {
        loadedRock = Instantiate(rockPrefab, start.transform.position, Quaternion.Euler(0,0,0));
        loadedRock.transform.GetChild(0).gameObject.SetActive(false);
        isRockLoaded = true;
    }

    private void pullRockBack(float trigger, GameObject controller, ActionBasedController XRController)
    {
        loadedRock.transform.position = controller.transform.position;
        leather.transform.position = new Vector3(controller.transform.position.x - 0.008f, controller.transform.position.y, controller.transform.position.z);

        Vector3 directionToStart = (start.transform.position - loadedRock.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToStart); // makes the rock look fowrad to ensure the direction when applying force
        loadedRock.transform.rotation = lookRotation;

        if (trigger == 0f)
        {
            releaseRock(controller);
            leather.transform.position = start.transform.position;
            XRController.SendHapticImpulse(releaseHapticAmp, releaseHapticDurr);
        }
    }

    private void releaseRock(GameObject controller)
    {
        loadedRock.transform.GetChild(0).gameObject.SetActive(true);
        float force = Vector3.Distance(controller.transform.position, start.transform.position); //calculates tge force
        Vector3 forceDir = (start.transform.position -  loadedRock.transform.position).normalized;

        loadedRock.transform.position = start.transform.position; //places the rock at the center of the slingshot so it is shot from there

        float totalForce = force * forceMultiplier;
        if (totalForce > 2.5f) totalForce = 2.5f; // sets the force to the max force

        Rigidbody rockRb = loadedRock.GetComponent<Rigidbody>();
        rockRb.AddForce(forceDir * totalForce, ForceMode.Impulse); //adds the force

        //resets to the next rock
        isRockLoaded = false;
        loadedRock = null;
    }

}
