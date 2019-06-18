using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteRenderer : MonoBehaviour
{   
    LineRenderer lineRenderer;
    public Material lineMaterial;

    Route currentRoute;
    
    Vector3 startPoint;
    Vector3 nextPoint;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        
        lineRenderer.widthMultiplier = 0.08f;
        lineRenderer.material = lineMaterial;

        startPoint = this.transform.parent.position;
        nextPoint = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        //lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, startPoint);

        nextPoint = startPoint;

        for(int i=1 ; i <= currentRoute.numWaypoints ; i++)
        {
            nextPoint = nextPoint + currentRoute.wayPoints[i-1];
            lineRenderer.SetPosition(i,nextPoint);
            //print(i);
        }
        /* nextPoint = startPoint + currentRoute.wayPoints[0];
        lineRenderer.SetPosition(1,nextPoint);

        nextPoint = nextPoint + currentRoute.wayPoints[1];
        lineRenderer.SetPosition(2,nextPoint);*/
    }

    public void InitRouteRenderer(Route route)
    {
        currentRoute = route;

        lineRenderer.positionCount = route.numWaypoints + 1;
    }
}
