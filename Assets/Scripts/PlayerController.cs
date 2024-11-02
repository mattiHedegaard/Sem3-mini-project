using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool holdingSomething;

    [Header("Holdables")]
    [SerializeField] SlingshotController slingshotController;
    [SerializeField] M1GController m1gController;
    [SerializeField] ThompsonController thompsonController;

    private void Update()
    {
        if (m1gController != null && slingshotController != null && thompsonController != null)
        {
            if (m1gController.isGrabbed || slingshotController.isGrabbed || thompsonController.isGrabbed)
            {
                holdingSomething = true;
            }
            else holdingSomething = false;
        }
    }
}
