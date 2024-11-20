using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedCrystalController : MonoBehaviour
{
    public float timer;

    private void Update()
    {
        timer -= 1 *Time.deltaTime;
        if (timer < 0 ) Destroy(gameObject);
    }
}
