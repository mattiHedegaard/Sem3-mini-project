using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    public GameObject destroyedVersion;
    public EnemyController enemyController;
    public GameObject parent;
    public DiceController diceController;

    public void destroyCrystal()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        enemyController.activeCrystals--;
        parent.SetActive(false);
    }
}
