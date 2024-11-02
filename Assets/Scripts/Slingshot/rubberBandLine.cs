using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubberBandLine : MonoBehaviour
{
    [SerializeField] Material material;
    public LineRenderer lineRenderer; // Reference to the Line Renderer
    public Transform anchor; // The start point of the line
    public Transform leather; // The end point of the line
    [SerializeField] float sWidth; //start
    [SerializeField] float eWidth; //end

    void Start()
    {
        lineRenderer.startWidth = sWidth; // Set the starting width
        lineRenderer.endWidth = eWidth; // Set the ending width

        // Ensure the line renderer has at least 2 positions
        lineRenderer.positionCount = 2;

        lineRenderer.material = material; // Uncomment if needed
    }

    void Update()
    {
        // Update the positions of the line renderer
        lineRenderer.SetPosition(0, anchor.position); // Start point
        lineRenderer.SetPosition(1, leather.position); // End point
    }
}
