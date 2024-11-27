using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubberBandLine : MonoBehaviour
{
    [SerializeField] Material material;
    public LineRenderer lineRenderer;
    public Transform anchor;
    public Transform leather;
    [SerializeField] float sWidth; //start
    [SerializeField] float eWidth; //end

    void Start()
    {
        lineRenderer.startWidth = sWidth; //set the starting width
        lineRenderer.endWidth = eWidth; //set the ending width

        //checks the line renderer has 2 positions
        lineRenderer.positionCount = 2;

        lineRenderer.material = material;
    }

    void Update()
    {
        //update the positions of the line renderer
        lineRenderer.SetPosition(0, anchor.position); // Start point
        lineRenderer.SetPosition(1, leather.position); // End point
    }
}
