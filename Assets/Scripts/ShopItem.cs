using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftController") || other.CompareTag("RightController"))
        {
            if (diceShop.rolledPoints >= price)
            {
                diceShop.inShop = false;

                switch (whichWeapon)
                {
                    case weaponType.THOMPSON:
                        ThompsonGunShoot thompson = weapon.GetComponent<ThompsonGunShoot>();

                        thompson.ammo += ammoAmount;
                        Debug.Log("t");
                        break;

                    case weaponType.M1G:
                        M1GGunShoot m1g = weapon.GetComponent<M1GGunShoot>();

                        m1g.ammo += ammoAmount;
                        Debug.Log("m1g");
                        break;

                    case weaponType.CANCEL:
                        Debug.Log("cancel");
                        break;
                }
            }
        }
    }
}
