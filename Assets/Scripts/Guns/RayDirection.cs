using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDirection : MonoBehaviour
{
    [SerializeField] Transform lookAt;

    private void Update()
    {
        transform.LookAt(lookAt);
    }
}
