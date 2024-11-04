using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject startHolding;

    private void Update()
    {
        if (startHolding != null)
        {
            GrabableController grab = startHolding.GetComponent<GrabableController>();
            if (grab != null)
            {
                startHolding.transform.position = transform.position;
                if (grab.isGrabbed) startHolding = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            GameObject currObject = other.gameObject;
            GrabableController grabableController = currObject.GetComponent<GrabableController>();

            if (grabableController != null && currObject != null)
            {
                if (!grabableController.inInventory && !grabableController.isGrabbed)
                {
                    grabableController.inInventory = true;
                }
                if (grabableController.inInventory)
                {
                    currObject.transform.position = transform.position;
                    Vector3 currentEulerAngles = transform.rotation.eulerAngles;

                    // Create a new Vector3 with the desired adjustments
                    Vector3 angle = new Vector3(currentEulerAngles.x + 45, currentEulerAngles.y - 90, currentEulerAngles.z);

                    // Set the rotation of currObject using the modified angles
                    currObject.transform.rotation = Quaternion.Euler(angle);
                }
                if (grabableController.isGrabbed)
                {
                    grabableController.inInventory = false;
                }
            }
        }
    }

}
/*GameObject holding;
    GrabableController holdingGrabable;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (holding != null && holdingGrabable != null)
        {
            holdingGrabable.inInventory = true;
            Rigidbody rb = holding.GetComponent<Rigidbody>();
            if (!holdingGrabable.isGrabbed)
            {
                holding.transform.position = transform.position;
                holding.transform.rotation = transform.rotation;
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            else
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                holdingGrabable.inInventory = false;
            }
        }

        if (holdingGrabable != null)
        {
            if (!holdingGrabable.inInventory)
            {
                Rigidbody thisRB = holding.GetComponentInChildren<Rigidbody>();
                thisRB.isKinematic = false;
                thisRB.useGravity = true;
                holdingGrabable.inInventory = false;
                holding = null;
                holdingGrabable = null;
            }
        }
        Debug.Log(holding + "\t" + holdingGrabable);

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            GrabableController grabableController = collision.gameObject.GetComponent<GrabableController>();

            if (grabableController != null && holding == null)
            {
                holding = collision.gameObject;
                holdingGrabable = holding.GetComponent<GrabableController>();
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Weapon") && collision.gameObject.Equals(holding) && holdingGrabable.isGrabbed && holding != null)
        {
            Rigidbody thisRB = collision.gameObject.GetComponentInChildren<Rigidbody>();
            thisRB.isKinematic = false;
            thisRB.useGravity = true;
            holdingGrabable.inInventory = false;
            holding = null;
            holdingGrabable = null;
            Debug.Log("exit");
        }
    }*/