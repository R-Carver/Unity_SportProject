using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteRenderer_ManCoverage : MonoBehaviour
{   
    public Material lineMaterial;

    // show a route to this position
    Transform destination; 
    
    Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {   
        destination = this.transform.Find("Destination");

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.widthMultiplier = 0.06f;
        lineRenderer.material = lineMaterial;

        startPoint = this.transform.parent.position;

        DrawLine();
    }

    void DrawLine()
    {
        // if you dont want the line to change use this
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, startPoint);

        Vector3 drawPoint = destination.position;
        lineRenderer.SetPosition(1,drawPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
