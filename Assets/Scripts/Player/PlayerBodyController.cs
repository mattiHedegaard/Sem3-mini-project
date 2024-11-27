using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour
{
    [SerializeField] GameObject follow;
    [SerializeField] float yOffset = 0.5f;

    void LateUpdate()
    {
        //makes the playerbody follow the player
        transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y - yOffset, follow.transform.position.z);
        transform.rotation = Quaternion.Euler(0, follow.transform.eulerAngles.y, 0);
    }
}
