using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public float timer;
    DiceController diceController;

    private void Start()
    {
        diceController = FindObjectOfType<DiceController>();

        // Check if it's found
        if (diceController != null)
        {
            Debug.Log("DiceController found!");
        }
        else
        {
            Debug.LogError("DiceController not found in the scene!");
        }
    }

    private void Update()
    {
        if (timer > 0) timer -= 1*Time.deltaTime; else if (timer <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("crystal"))
        {
            Destroy(gameObject);
            CrystalController crystalController = collision.gameObject.GetComponent<CrystalController>();
            crystalController.destroyCrystal();
            diceController.dicePoints += Random.Range(25, 35);
        }
    }
}
