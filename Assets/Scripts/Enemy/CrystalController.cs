using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    public GameObject destroyedVersion;
    public EnemyController enemyController;

    public void destroyCrystal()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        gameObject.SetActive(false);
        enemyController.activeCrystals--;
    }
}
