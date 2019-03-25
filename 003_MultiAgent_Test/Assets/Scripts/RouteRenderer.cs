using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteRenderer : MonoBehaviour
{   
    public Material lineMaterial;
    
    Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.widthMultiplier = 0.06f;
        lineRenderer.material = lineMaterial;

        startPoint = this.transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, startPoint);

        Vector3 drawPoint = new Vector3(startPoint.x, startPoint.y, startPoint.z - 7f);
        lineRenderer.SetPosition(1,drawPoint);
    }
}
