using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.OpenXR.Features.Interactions.HTCViveControllerProfile;

public class DiceContrller : MonoBehaviour
{
    [Header("controller Input")]
    public InputActionProperty leftMenuBtn;
    public InputActionProperty rightMenuBtn;
    public InputActionProperty leftGrip;
    public InputActionProperty rightGrip;

    [Header("controller")]
    [SerializeField] GameObject rightController;
    [SerializeField] GameObject leftController;

    [Header("Body")]
    [SerializeField] GameObject playerBody;
    [SerializeField] float xOffset = 0.5f;
    [SerializeField] float yOffset = 0.5f;

    [Header("Dice")]
    [SerializeField] DiceIsGrabbed dig;
    Rigidbody rb;
    bool diceCalled = false;
    bool diceRolled = false;
    bool diceThrowed = false;
    bool diceReady = true;
    bool isgrabbed;
    int rolled;

    private List<GameObject> numberList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        leftMenuBtn.action.Enable();
        rightMenuBtn.action.Enable();
        leftGrip.action.Enable();
        rightGrip.action.Enable();

        foreach (Transform child in transform)
        {
            numberList.Add(child.gameObject); //this line says this: NullReferenceException: Object reference not set to an instance of an object DiceContrller.Start()(at Assets / Scripts / DiceController.cs:34)
        }

        numberList.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        //Menu
        float rightMenuPressed = rightMenuBtn.action.ReadValue<float>();
        float leftMenuPressed = leftMenuBtn.action.ReadValue<float>();
        //Grip
        float rightGripPressed = rightGrip.action.ReadValue<float>();
        float leftGripPressed = leftGrip.action.ReadValue<float>();

        if (rightMenuPressed == 1f || leftMenuPressed == 1f) diceCalled = true;

        if (diceCalled)
        {
            if (rightGripPressed == 1f || leftGripPressed == 1f)
            {
                diceCalled = false;
            }

            Vector3 forwardDirection = playerBody.transform.forward;
            Vector3 newPosition = playerBody.transform.position + forwardDirection * xOffset;
            newPosition.y = playerBody.transform.position.y+yOffset;

            transform.position = newPosition;
        }

        if (dig.isGrabbed)
        {
            diceReady = true;
        }
        if (diceReady && dig.diceRolled)
        {
            diceRolled = true;
        }

        if (diceRolled)
        {
            if (rb.IsSleeping())
            {
                numberList.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
                rolled = int.Parse(numberList[0].name);
                Debug.Log(rolled);
                
                diceRolled = false;
                dig.diceRolled = false;
            }
        }


    }
}