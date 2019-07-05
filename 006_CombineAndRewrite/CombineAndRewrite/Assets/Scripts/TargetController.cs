using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{   

    Route currentRoute;

    Vector3 targetForward;
    Vector3 targetBack;
    public float speed = 2.0f;
    // Start is called before the first frame update

    int waypointIndex = 0;
    void Start()
    {
        /* currentRoute = RouteController.Instance.currentRoute;
        this.transform.position = RouteController.Instance.receiverPos;

        ResetTarget();*/
    }

    public void ResetTarget()
    {   
        currentRoute = RouteController.Instance.currentRoute;
        this.transform.position = RouteController.Instance.receiverPos;

        //init the target at the last waypoint
        int numWaypoints = currentRoute.numWaypoints;

        //we need two targets, one for going forward and one for going back

        //get the global version of the last waypoint
        waypointIndex = numWaypoints - 1;
        Vector3 lastWaypoint = RouteController.Instance.GetGlobalWaypoint(currentRoute, numWaypoints-1);

        targetForward = lastWaypoint;
        if(numWaypoints > 1){
            targetBack = RouteController.Instance.receiverPos + currentRoute.wayPoints[numWaypoints-2];
        }else{
            targetBack = RouteController.Instance.receiverPos;
        }

        this.transform.position = lastWaypoint;
    }

    // Update is called once per frame
    void Update()
    {   
        currentRoute = RouteController.Instance.currentRoute;
        //float step = speed * Time.deltaTime;
        
        /* transform.position = Vector3.MoveTowards(transform.position, target, step);
        if((target - transform.position).magnitude < 0.1f && currentRoute.numWaypoints > waypointIndex){
            target = target + currentRoute.wayPoints[waypointIndex];
            waypointIndex++;
        }*/

    }

    public void MoveTarget(float direction){

        // we can only move the target along the route of the receiver
        // so we interpret a positive value of direction as moving to the end
        // of the route and a negative as moving to the beginning
        float step = speed * Time.deltaTime;

        if(direction > 0){
            
            // move to next waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetForward, step);
            //print(targetForward);
            //check if target was reached
            CheckTargetForward();

        }else if(direction < 0){

            // move to previsou waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetBack, step);
            //print(targetBack);
            //check if target was reached
            CheckTargetBack();

        }else{
            //do nothing
        }
        
    }

    private void CheckTargetForward(){
        if((targetForward - transform.position).magnitude < 0.1f && currentRoute.numWaypoints - 1 > waypointIndex){
            
            waypointIndex++;

            //we need to also change the targetBack to the prior targetForward
            targetBack = targetForward;

            targetForward = targetForward + currentRoute.wayPoints[waypointIndex];

        }
    }

    private void CheckTargetBack(){
        if((targetBack - transform.position).magnitude < 0.1f && waypointIndex > 0){
            
            waypointIndex--;

             //we need to also change the targetForward to the prior targetForward
            targetForward = targetBack;

            targetBack = targetBack - currentRoute.wayPoints[waypointIndex];
            
        }
    }
}
