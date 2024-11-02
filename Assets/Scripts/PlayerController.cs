using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class playerController : MonoBehaviour
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
    [SerializeField] SlingshotController slingshotController;
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
    }

    void Update()
    {
        float rightTriggerPressed = rightTrigger.action.ReadValue<float>();//float from 0 to 1, check for fully pressed with 1.
        float leftTriggerPressed = leftTrigger.action.ReadValue<float>(); //float from 0 to 1, check for fully pressed with 1.
        float currentHandTriggerPressed = 0f;
        GameObject currentRockHand = null;

        if (slingshotController != null)
        {
            if (slingshotController.isGrabbed)
            {
                if (slingshotController.hand == "left")
                {
                    rightHandDefaultModel.SetActive(false);
                    leftHandDefaultModel.SetActive(false);
                    if (!isRockLoaded) rightHandRockModel.SetActive(true); else rightHandRockModel.SetActive(false);
                    currentHandTriggerPressed = rightTriggerPressed;
                    currentRockHand = rightController;
                }
                else if (slingshotController.hand == "right")
                {
                    leftHandDefaultModel.SetActive(false);
                    rightHandDefaultModel.SetActive(false);
                    if (!isRockLoaded) leftHandRockModel.SetActive(true); else leftHandRockModel.SetActive(false);
                    currentHandTriggerPressed = leftTriggerPressed;
                    currentRockHand = leftController;
                }
            }
            else
            {
                leftHandDefaultModel.SetActive(true);
                rightHandDefaultModel.SetActive(true);
                leftHandRockModel.SetActive(false);
                rightHandRockModel.SetActive(false);
                currentHandTriggerPressed = 0f;
                currentRockHand = null;
            }

            
            if (!isRockLoaded && isNearLeatherChild(currentRockHand) && currentHandTriggerPressed != 0f)
            {
                createRock();
            }
            else if (isRockLoaded)
            {
                pullRockBack(currentHandTriggerPressed,currentRockHand);
            }
        }
    }

    private bool isNearLeatherChild(GameObject controller)
    {
        if (controller != null)
        {
            float distance = Vector3.Distance(controller.transform.position, leather.transform.position);
            float distanceThreshold = 0.1f;

            return distance < distanceThreshold;
        }
        return false;
    }

    void createRock()
    {
        loadedRock = Instantiate(rockPrefab, start.transform.position, Quaternion.Euler(0,0,0));
        isRockLoaded = true;
    }

    private void pullRockBack(float trigger, GameObject controller)
    {
        loadedRock.transform.position = controller.transform.position;
        leather.transform.position = new Vector3(controller.transform.position.x - 0.008f, controller.transform.position.y, controller.transform.position.z);

        Vector3 directionToStart = (start.transform.position - loadedRock.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToStart);
        loadedRock.transform.rotation = lookRotation;

        if (trigger == 0f)
        {
            releaseRock(controller);
            leather.transform.position = start.transform.position;
        }
    }

    private void releaseRock(GameObject controller)
    {
        float force = Vector3.Distance(controller.transform.position, start.transform.position);
        Vector3 forceDir = (start.transform.position -  loadedRock.transform.position).normalized;
        //forceDir.y = 0;

        Debug.Log("Dir: " + forceDir);
        Debug.Log("force: " + force*forceMultiplier);

        loadedRock.transform.position = start.transform.position;

        float totalForce = force * forceMultiplier;
        if (totalForce > 2.5f) totalForce = 2.5f;

        Rigidbody rockRb = loadedRock.GetComponent<Rigidbody>();
        rockRb.AddForce(forceDir * totalForce, ForceMode.Impulse);

        isRockLoaded = false;
        loadedRock = null;
    }

}
