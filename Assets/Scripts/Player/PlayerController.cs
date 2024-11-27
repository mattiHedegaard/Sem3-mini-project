using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool holdingSomething;

    [Header("Holdables")]
    [SerializeField] GameObject slingshot;
    [SerializeField] GameObject m1g;
    [SerializeField] GameObject thompson;

    List<GrabableController> grabables;

    private void Start()
    {
        grabables = new List<GrabableController>()
        {
            slingshot.GetComponent<GrabableController>(),
            m1g.GetComponent<GrabableController>(),
            thompson.GetComponent<GrabableController>()
        };

    }

    //updates the items isGrabbed, so that the scale and pos fit for the inventory
    private void Update()
    {
        holdingSomething = false;
        for (int i = 0; i < grabables.Count; i++)
        {
            if (grabables[i] != null)
            {
                if (grabables[i].isGrabbed)
                {
                    holdingSomething = true;
                }
            }
        }
    }
}
