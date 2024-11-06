using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public enum weaponType
    {
        M1G,
        THOMPSON,
        CANCEL
    }

    [Header("Controllers")]
    [SerializeField] DiceShop diceShop;

    [Header("shopitem")]
    public int price;
    public int ammoAmount;
    public GameObject weapon;
    public weaponType whichWeapon;
    public TextMeshPro text;
    public string name;

    Vector3 scale;
    float shrinkAmount = 0.40f;

    private void Start()
    {
        scale = transform.localScale;
        if (whichWeapon != weaponType.CANCEL) text.text = $"{name}\nx{ammoAmount}\nPrice: {price}"; else text.text = "Back";
    }

    private void Update()
    {
        if (price > diceShop.rolledPoints)
        {
            transform.localScale = scale * shrinkAmount;
            text.color = Color.red;
        }
        else
        {
            transform.localScale = scale;
            text.color = Color.white;
        }
    }

    public void buyItem()
    {
        if (diceShop.rolledPoints >= price)
        {
            switch (whichWeapon)
            {
                case weaponType.THOMPSON:
                    ThompsonGunShoot thompson = weapon.GetComponent<ThompsonGunShoot>();

                    thompson.ammo += ammoAmount;
                    Debug.Log("thompson");
                    break;

                case weaponType.M1G:
                    M1GGunShoot m1g = weapon.GetComponent<M1GGunShoot>();

                    m1g.ammo += ammoAmount;
                    Debug.Log("m1g");
                    break;

                case weaponType.CANCEL:
                    Debug.Log("cancel");
                    diceShop.rolledPoints = 0;
                    diceShop.inShop = false;
                    break;
            }
            diceShop.rolledPoints -= price;
        }
    }
}
