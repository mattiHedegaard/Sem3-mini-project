using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public float timer;
    DiceController diceController;

    private void Start()
    {
        diceController = FindObjectOfType<DiceController>(); // finds the dice so that it can add to its points
    }

    private void Update()
    {
        if (timer > 0) timer -= 1*Time.deltaTime; else if (timer <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //calls the crystals destroyfunction if hit by the rock
        if (collision.gameObject.CompareTag("crystal"))
        {
            Destroy(gameObject);
            CrystalController crystalController = collision.gameObject.GetComponent<CrystalController>();
            crystalController.destroyCrystal();
            diceController.dicePoints += Random.Range(25, 35); // ads to the dies points
        }
    }
}
