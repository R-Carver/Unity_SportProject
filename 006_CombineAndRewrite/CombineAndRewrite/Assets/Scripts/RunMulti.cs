using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMulti : MonoBehaviour{

    Vector3 target;
    RouteController routeController;
    Route randomRoute;
    //keep track of the current waypoint in the route
    int waypointIndex;

    RouteRenderer routeRenderer;

    void Start()
    {   
        routeRenderer = GetComponentInChildren<RouteRenderer>();

        routeController = RouteController.Instance;

        //for restarting the route system we have to set the route to not finished on every start
        
        //get a random route
        //randomRoute = routeController.GetRandomRoute();

        //set the route on the renderer
        routeRenderer.InitRouteRenderer(randomRoute);

        waypointIndex = 0;
        Vector3 targetDir = randomRoute.wayPoints[waypointIndex];

        target = CalcTarget(targetDir);
    }

    void Update()
    {
        float step = 1.0f * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if((target - transform.position).magnitude < 0.1f)
        {   
            if(randomRoute.numWaypoints > (waypointIndex + 1))
            {
                //there is stil a new waypoint left
                //print("Change");
                waypointIndex ++;
                Vector3 nextWaypointDir = randomRoute.wayPoints[waypointIndex];
                transform.position = target;
                
                target = CalcTarget(nextWaypointDir);
            }else
            {
                //this is the last waypoint

                //set the route to finished to be able to reset the env
                print("route finished");
                routeController.currentRouteFinished = true;

                //finished routes means resetting the episode
                GameController.GetInstance().academy.Done();
            }
        }
    }

    public void resetRoutes()
    {
        randomRoute = routeController.GetRandomRoute();

        //set the route on the renderer
        routeRenderer.InitRouteRenderer(randomRoute);

        waypointIndex = 0;
        Vector3 targetDir = randomRoute.wayPoints[waypointIndex];

        target = CalcTarget(targetDir);
    }

    private Vector3 CalcTarget(Vector3 targetDir)
    {
        return this.transform.position + targetDir;
    }
}