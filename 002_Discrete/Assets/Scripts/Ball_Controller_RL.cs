using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller_RL : MonoBehaviour
{   
    public Rigidbody ball;
    public Transform target;

    public float h = 2;
    public float gravity = -18;

    public bool launched = false;

    // this keeps calculating the launchData for the ball
    public LaunchData currentLaunchData;

    void Start(){
        ball.useGravity = false;

        //This might be a problem later if the target is not set up yet
        currentLaunchData = CalculateLaunchData();
        
    }

    void Update(){
        
        currentLaunchData = CalculateLaunchData();

    }

    public void Launch(){
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = currentLaunchData.initialVelocity;
        ball.angularVelocity = Vector3.zero;
    }

    LaunchData CalculateLaunchData(){

        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

        float time = (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity));

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY, time);
    }

    public int TargetSpeed = 3;
    public void MoveTarget(float zValue)
    {
        // in the current version we only move the target along the route of the receiver
        // which is along the z axis
        Vector3 movement = new Vector3(0, 0, zValue);
        if(target.position.z < 3 && zValue >= 0)
        {
            target.Translate(movement * TargetSpeed *Time.deltaTime);
        }

        if(target.position.z > -4 && zValue < 0)
        {
            target.Translate(movement * TargetSpeed *Time.deltaTime);
        }
    }

    public void ResetLauncher()
    {
        ball.useGravity = false;
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        launched = false;

        target.position = new Vector3(3, 0, 0);
    }

    public struct LaunchData{
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
