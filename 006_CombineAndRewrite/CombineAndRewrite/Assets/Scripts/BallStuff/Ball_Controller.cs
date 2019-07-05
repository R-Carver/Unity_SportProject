using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{   
    public Rigidbody ball;
    public Transform target;

    public float h = 1;
    public float gravity = -18;

    public bool launched = false;

    void Start(){
        ball.useGravity = false;
    }

    void Update(){

    }

    public void Launch(){
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        
        ball.velocity = CalculateLaunchData().initialVelocity;
        ball.angularVelocity = Vector3.zero;
    }

    public LaunchData CalculateLaunchData(){

        float displacementY = target.position.y - ball.position.y;
        
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        

        float time = (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity));
        
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        
        Vector3 velocityXZ = displacementXZ / time;
        
        return new LaunchData(velocityXZ + velocityY, time);
    }


    public void ResetLauncher()
    {
        ball.useGravity = false;
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        launched = false;
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
