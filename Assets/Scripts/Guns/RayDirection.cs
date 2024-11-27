using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDirection : MonoBehaviour
{
    [SerializeField] Transform lookAt;

    private void Update()
    {
        //makes the raycast dir match the iron sights
        transform.LookAt(lookAt);
    }
}
