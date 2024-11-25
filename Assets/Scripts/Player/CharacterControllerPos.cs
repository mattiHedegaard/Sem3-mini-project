using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerPos : MonoBehaviour
{
    CharacterController cc;

    [Header("Colliders to ingore")]
    [SerializeField] Collider m1g;
    [SerializeField] Collider thompson;
    [SerializeField] Collider slingshot;
    [SerializeField] Collider dice;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        Physics.IgnoreCollision(cc.GetComponent<Collider>(), m1g);
        Physics.IgnoreCollision(cc.GetComponent<Collider>(), thompson);
        Physics.IgnoreCollision(cc.GetComponent<Collider>(), slingshot);
        Physics.IgnoreCollision(cc.GetComponent<Collider>(), dice);
    }
}
