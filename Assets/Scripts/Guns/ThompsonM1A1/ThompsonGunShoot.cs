using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThompsonGunShoot : MonoBehaviour
{
    [Header("Ray")]
    [SerializeField] GameObject rayPoint;

    [Header("controller")]
    [SerializeField] PlayerThompsonController playerThompsonController;

    [Header("Shooting")]
    float shootCD = 0f;
    [SerializeField] float shootCDMax;
    [SerializeField] float hapticDurr;
    [SerializeField] float hapticAmp;
    [SerializeField] GameObject prefab;
    public int ammo = 0;
    public DiceController diceController;

    private void Update()
    {
        if (shootCD >= 0)
        {
            shootCD -= 1*Time.deltaTime;
        }
        GameObject objectHit = null;

        if (playerThompsonController != null)
        {
            if (playerThompsonController.currentHandTriggerPressed == 1f && shootCD <= 0f && ammo > 0)
            {
                ammo -= 1;
                shootCD = shootCDMax;
                playerThompsonController.currentXRController.SendHapticImpulse(hapticAmp, hapticDurr);

                RaycastHit hit;

                Ray ray = new Ray(rayPoint.transform.position, rayPoint.transform.forward);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
                {
                    objectHit = hit.collider.gameObject;

                    if (objectHit != null)
                    {
                        Vector3 hitLocation = hit.point;
                        GameObject bulletHole = Instantiate(prefab, hitLocation, Quaternion.identity);
                        bulletHole.transform.SetParent(objectHit.transform);

                        if (objectHit.CompareTag("enemy"))
                        {
                            EnemyController enemy = objectHit.GetComponent<EnemyController>();
                            enemy.hp--;
                        }
                        if (objectHit.CompareTag("crystal"))
                        {
                            CrystalController crystal = objectHit.GetComponent<CrystalController>();
                            crystal.destroyCrystal();
                            diceController.dicePoints += Random.Range(25, 35);
                        }
                    }

                    if (objectHit == null) Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f); else Debug.DrawRay(ray.origin, ray.direction * 10f, Color.white, 1f);
                    
                }
                
            }
        }
    }
}
