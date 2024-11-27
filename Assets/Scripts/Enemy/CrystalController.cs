using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalController : MonoBehaviour
{
    public GameObject destroyedVersion;
    public EnemyController enemyController;
    public GameObject parent;
    public DiceController diceController;

    public void destroyCrystal()
    {
        //creates a crystal that falls apart and sets the crystal to inactive
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        enemyController.activeCrystals--;
        parent.SetActive(false);
    }
}
