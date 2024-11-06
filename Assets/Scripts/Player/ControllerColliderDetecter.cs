using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerColliderDetecter : MonoBehaviour
{
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
