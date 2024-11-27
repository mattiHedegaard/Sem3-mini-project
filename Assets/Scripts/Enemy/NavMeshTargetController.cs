using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshTargetController : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        //keeps the target at tge player
        transform.position = target.position;
    }
}
