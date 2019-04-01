using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class DefAgent_ManCoverage01 : Agent
{
    public Transform Receiver;
    public EnvController_ManCoverage envController;

    Rigidbody agentRB;
    Academy_ManCoverage academy;
    

    Quaternion startRotation;
    Vector3 startPosition;

    public override void InitializeAgent()
    {   
        academy = FindObjectOfType<Academy_ManCoverage>();
        agentRB = GetComponent<Rigidbody>();
        startRotation = transform.rotation;
        startPosition = transform.localPosition;
    }

    public override void AgentReset()
    {   
        // Reset Env
        envController.resetEnv();
        transform.localPosition = startPosition;
        transform.rotation = startRotation;
    }

    public override void CollectObservations()
    {   
        // observations for now are the position of the reveiver and the position of the ball
        AddVectorObs(Receiver.localPosition);
        AddVectorObs(transform.localPosition);
    }

    // for now try to move the agent just by moving foward/backward and changing the rotation
    // so in the first iteration there are 2 discrete arrays with 3 values each (like: dont move, move forward, move backward)
    public override void AgentAction(float[] vectorAction, string textAction)
    {   
        MoveAgent(vectorAction);

        if(Receiver.localPosition.z < -10)
        {
            Done();
        }

        // punish for leaving the area 
        if(Math.Abs(transform.localPosition.z) > 6 || Math.Abs(transform.localPosition.x) > 5)
        {
            AddReward(-0.005f);
        }
        //print(Receiver.localPosition);
        Monitor.Log("reward", this.GetReward());
        Monitor.Log("episode reward", this.GetCumulativeReward());


    }

    public void MoveAgent(float[] act)
    {   
        // punish for beeing idle
        AddReward(-0.001f);
        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        // get the move signals
        int dirForward = (int) act[0];
        int rotateDirAction = (int) act[1];

        if(dirForward == 1)
        // move forward
            dirToGo = transform.forward * 1f;
        else if(dirForward == 2)
        // move back
            dirToGo = transform.forward * -1f;

        if(rotateDirAction == 1)
        // left rotation
            rotateDir = transform.up * -1f;
        else if(rotateDirAction == 2)
        // right rotation
            rotateDir = transform.up * 1f;
        
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 300f);
        agentRB.AddForce(dirToGo * academy.agentRunSpeed , ForceMode.VelocityChange);
    }

    private void FixedUpdate() 
    {   
        // try to correct the rotation issue
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y, 0 );
    }

    
}
