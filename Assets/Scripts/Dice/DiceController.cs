using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.OpenXR.Features.Interactions.HTCViveControllerProfile;

public class DiceController : MonoBehaviour
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
    [SerializeField] float yOffset = 0.8f;

    [Header("Dice")]
    [SerializeField] DiceIsGrabbed dig;
    Rigidbody rb;
    bool diceCalled = false;
    bool diceRolled = false;
    bool diceThrowed = false;
    bool diceReady = false;
    bool isgrabbed;
    int rolled;
    DiceShop diceShop;
    public int dicePoints = 0;
    public bool enoughPoints = false;

    private List<GameObject> numberList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        leftMenuBtn.action.Enable();
        rightMenuBtn.action.Enable();
        leftGrip.action.Enable();
        rightGrip.action.Enable();

        //creates a list of all the number child objects placed on each side of the die
        foreach (Transform child in transform)
        {
            numberList.Add(child.gameObject);
        }

        numberList.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));

        rb = GetComponent<Rigidbody>();
        diceShop = GetComponent<DiceShop>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(dicePoints + "\t" + enoughPoints);
        //Menu
        float rightMenuPressed = rightMenuBtn.action.ReadValue<float>(); //pause enemys when this is pressed
        float leftMenuPressed = leftMenuBtn.action.ReadValue<float>();
        //Grip
        float rightGripPressed = rightGrip.action.ReadValue<float>();
        float leftGripPressed = leftGrip.action.ReadValue<float>();

        if (dicePoints >= 100)
        {
            enoughPoints = true;
        }
        else enoughPoints = false;

        if ((rightMenuPressed == 1f || leftMenuPressed == 1f) && enoughPoints) diceCalled = true;

        //the dice follows the player
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

        //when it is grabbed it sets diceReady to true so that it knows that it can be rolled(so that it os not enough it is being pushed on the ground
        if (dig.isGrabbed)
        {
            diceReady = true;
        }
        if (diceReady && dig.diceRolled)
        {
            diceRolled = true;
        }

        if (diceRolled && dig.diceRolled)
        {
            //checks if the dice is laying still
            if (rb.IsSleeping())
            {
                //sorts the objects depending on their y value, the highest is what is rolled.
                numberList.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
                rolled = int.Parse(numberList[0].name);
                diceShop.rolledPoints += rolled;
                rolled = 0;

                //resets so it is ready to be rolled again
                diceRolled = false;
                dig.diceRolled = false;
                diceShop.inShop = true;
            }
        }


    }
}
