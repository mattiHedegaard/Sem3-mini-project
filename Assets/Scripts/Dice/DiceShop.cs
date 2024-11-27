using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceShop : MonoBehaviour
{
    DiceController diceController;
    [SerializeField] ShopItem shopItem;

    [Header("Weapons")]
    [SerializeField] GameObject thompson;
    [SerializeField] GameObject m1g;
    [SerializeField] GameObject slingshot;

    [Header("shop")]
    public bool inShop = false;
    [SerializeField] GameObject m1gSIObject;
    [SerializeField] GameObject thompsonSIObject;
    [SerializeField] GameObject cancenSIObject;
    [SerializeField] GameObject points;
    [SerializeField] TextMeshPro text;

    public int rolledPoints;

    private void Start()
    {
        diceController = GetComponent<DiceController>();
        if (diceController == null) Debug.Log("DiceCotnroller missing");
    }

    private void Update()
    {
        //sets the objects active/inactive when the shop is avtive/inactive
        if (inShop)
        {
            m1gSIObject.SetActive(true);
            thompsonSIObject.SetActive(true);
            cancenSIObject.SetActive(true);
            points.SetActive(true);
            text.text = $"You have {rolledPoints} points!";
        }
        else
        {
            m1gSIObject.SetActive(false);
            thompsonSIObject.SetActive(false);
            cancenSIObject.SetActive(false);
            points.SetActive(false);
        }
    }
}
