using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class handAnimationController : MonoBehaviour
{
    [Header("controller Input")]
    public InputActionProperty trigger;
    public InputActionProperty grab;

    [Header("Controllers")]
    [SerializeField] GameObject controller;
    ActionBasedController XRController;

    [Header("AnimationContoler")]
    [SerializeField] Animator ac;

    // Start is called before the first frame update
    void Start()
    {
        XRController = controller.GetComponent<ActionBasedController>();
        if (XRController == null)
        {
            Debug.Log("right XR missing");
        }
        if (XRController == null)
        {
            Debug.Log("left XR missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float triggerPressed = trigger.action.ReadValue<float>();//float from 0 to 1, check for fully pressed with 1.
        float grabPressed = grab.action.ReadValue<float>();
        float grabAValue = 0f;
        float triggerAValue = 0f;
        if (triggerPressed != 0f) triggerAValue = triggerPressed - 0.001f; else triggerAValue = 0f;
        if (grabPressed != 0f) grabAValue = grabPressed - 0.001f; else grabAValue = 0f;

        ac.Play("triggerHanAnimation", 1, triggerAValue);
        ac.Play("grabHandAnimation", 2, grabAValue);

        ac.speed = 0;
    }
}
