using System.Collections;
using System.Collections.Generic;
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

    public int rolledPoints;

    private void Start()
    {
        diceController = GetComponent<DiceController>();
        if (diceController == null) Debug.Log("DiceCotnroller missing");
    }

    private void Update()
    {
        if (inShop)
        {
            m1gSIObject.SetActive(true);
            thompsonSIObject.SetActive(true);
            cancenSIObject.SetActive(true);
        }
        else
        {
            m1gSIObject.SetActive(false);
            thompsonSIObject.SetActive(false);
            cancenSIObject.SetActive(false);
        }
    }
}
