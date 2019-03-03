using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller_with_Target : MonoBehaviour
{   
    public Rigidbody ball;
    public Transform target;

    public float h = 2;
    public float gravity = -18;

    public bool debugPath = true;
    public bool launched = false;

    List<Vector3> trajectory = new List<Vector3>();
    Vector3[] trajectoryForLineRend;
    Transform lineRenderer;

    void Start(){
        ball.useGravity = false;
        lineRenderer = this.gameObject.transform.GetChild(0);
        
    }

    void Update(){

        if(Input.GetKeyDown(KeyCode.Space)){

            // get trajectory
            trajectory = GetPath();
            
            Launch();
            launched = true;
        }

        if(debugPath && !launched){
            DrawPath();
            RenderTrajectory();
            
        }else if(launched){
            DrawPathFromList(trajectory);
            
        }
    }

    void RenderTrajectory(){

        //Render trajectory in the Game View
            trajectory = GetPath();
            trajectoryForLineRend = trajectory.ToArray();
            TrajectoryRenderer renderer = lineRenderer.GetComponent<TrajectoryRenderer>();
            renderer.UpdateTrajectory(trajectoryForLineRend);
    }

    void Launch(){
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData(){

        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

        float time = (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity));

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY, time);
    }

    void DrawPath(){
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;

        LineRenderer line = GetComponent<LineRenderer>();

        int resolution = 30;
        for(int i = 1; i <= resolution; i++){
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime/2f;
            Vector3 drawPoint = ball.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    List<Vector3> GetPath(){
        /*
          Here we want a way to draw the path without updating it while the ball is on it's way
          So we just want to save the points and then when the ball is launched, we don't
          want to update this any longer
        */
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;
        List<Vector3> pathPoints = new List<Vector3>();
        pathPoints.Add(previousDrawPoint);

        int resolution = 30;
        for(int i = 1; i <= resolution; i++){
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime/2f;
            Vector3 drawPoint = ball.position + displacement;
            pathPoints.Add(drawPoint);
        }
        return pathPoints;
    }

    void DrawPathFromList(List<Vector3> pathPoints)
    {   
        //get the start point
        Vector3 previousDrawPoint = pathPoints[0];
        for(int i=1; i < pathPoints.Count; i++)
        {
            Vector3 drawpoint = pathPoints[i];
            Debug.DrawLine(previousDrawPoint, drawpoint, Color.green);
            previousDrawPoint = drawpoint;
        }
    }

    struct LaunchData{
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
