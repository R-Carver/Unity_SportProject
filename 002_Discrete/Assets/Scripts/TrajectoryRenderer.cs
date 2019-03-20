using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{   
    public Transform ball;
    Ball_Controller_RL ball_controller;

    List<Vector3> trajectory = new List<Vector3>();
    Vector3[] trajectoryForLineRend;

    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        ball_controller = ball.GetComponent<Ball_Controller_RL>();
        lineRenderer.positionCount = 31;
        lineRenderer.widthMultiplier = 0.06f;

    }

    // Update is called once per frame
    void Update()
    {   
        if(ball_controller.launched == false)
        {
            RenderTrajectory();
        }
        
    }

    void RenderTrajectory(){

        //Render trajectory in the Game View
            trajectory = GetPath();
            trajectoryForLineRend = trajectory.ToArray();
            this.UpdateTrajectory(trajectoryForLineRend);
    }

    public void UpdateTrajectory(Vector3[] points){
        
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPositions(points);
    }

    List<Vector3> GetPath(){
        /*
          Here we want a way to draw the path without updating it while the ball is on it's way
          So we just want to save the points and then when the ball is launched, we don't
          want to update this any longer
        */
        Ball_Controller_RL.LaunchData launchData = ball_controller.CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;
        List<Vector3> pathPoints = new List<Vector3>();
        pathPoints.Add(previousDrawPoint);

        int resolution = 30;
        for(int i = 1; i <= resolution; i++){
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * ball_controller.gravity * simulationTime * simulationTime/2f;
            Vector3 drawPoint = ball.position + displacement;
            pathPoints.Add(drawPoint);
        }
        return pathPoints;
    }
}
