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

    bool justFired;

    public float durr;
    public float amp;
    public GameObject prefab;
    
    private void Update()
    {
        GameObject objectHit = null;

        if (playerM1GController != null)
        {
            if (playerM1GController.currentHandTriggerPressed == 1f && !justFired)
            {
                playerM1GController.currentXRController.SendHapticImpulse(amp, durr);
                
                

                justFired = true;

                RaycastHit hit;

                Ray ray = new Ray(rayPoint.transform.position, rayPoint.transform.forward);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
                {
                    objectHit = hit.collider.gameObject;

                    if (objectHit != null)
                    {
                        Vector3 hitLocation = hit.point;
                        Instantiate(prefab, hitLocation, Quaternion.identity);
                    }
                }
                if (objectHit == null) Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f); else Debug.DrawRay(ray.origin, ray.direction * 10f, Color.white, 1f);

            }
            else if (playerM1GController.currentHandTriggerPressed != 1f) justFired = false;
        }
    }
}
