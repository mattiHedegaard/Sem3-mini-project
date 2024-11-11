using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.XR.Interaction.Toolkit;

public class M1GGunShoot : MonoBehaviour
{
    [Header("Ray")]
    [SerializeField] GameObject rayPoint;

    [Header("controller")]
    [SerializeField] PlayerM1Controller playerM1GController;

    [Header("Shooting")]
    bool justFired;
    [SerializeField] float hapticDurr;
    [SerializeField] float hapticAmp;
    public GameObject prefab;
    public int ammo = 0;

    private void Update()
    {
        GameObject objectHit = null;

        if (playerM1GController != null)
        {
            if (playerM1GController.currentHandTriggerPressed == 1f && !justFired && ammo > 0)
            {
                ammo -= 1;

                playerM1GController.currentXRController.SendHapticImpulse(hapticAmp, hapticDurr);
                
                

                justFired = true;

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
                    }
                }
                if (objectHit == null) Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f); else Debug.DrawRay(ray.origin, ray.direction * 10f, Color.white, 1f);

            }
            else if (playerM1GController.currentHandTriggerPressed != 1f) justFired = false;
        }
    }
}
