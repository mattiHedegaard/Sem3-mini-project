using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerColliderDetecter : MonoBehaviour
{
    //checks if the hand touches a shopitem and calls the buytiem function
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopItem"))
        {
            ShopItem boughtItem = other.GetComponent<ShopItem>();
            if (boughtItem != null)
            {
                boughtItem.buyItem();
            }
        }
    }
}
