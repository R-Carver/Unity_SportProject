using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class Agent_Simple_Walking : Agent
{
    public Transform TargetPoint;

    Rigidbody agentRB;
    Academy_WalkingSimple academy;
    

    Quaternion startRotation;
    Vector3 startPosition;

    public override void InitializeAgent()
    {   
        academy = FindObjectOfType<Academy_WalkingSimple>();
        agentRB = GetComponent<Rigidbody>();
        startRotation = transform.rotation;
        startPosition = transform.localPosition;
    }

    public override void AgentReset()
    {   
        transform.localPosition = startPosition;
        transform.rotation = startRotation;
    }

    public override void CollectObservations()
    {   
        AddVectorObs(TargetPoint.localPosition);
        AddVectorObs(transform.localPosition);
        //print("Walker " + transform.localPosition);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {   
        MoveAgent(vectorAction);
         /*  Uncomment this when you want to activate the proportional lerning
        double rewardBig = (TargetPoint.position - transform.position).sqrMagnitude;
        double reward = -rewardBig/10; 
        
        // force value to not be smaller than -1
        reward = reward / 6.0f;
        if(reward < -1){
            reward = -1;
        }
        

        //print((float)Math.Round(reward,1));
        SetReward((float)Math.Round(reward,1));
        */
        
        if(Mathf.Abs(transform.localPosition.z) > 5 || Mathf.Abs(transform.localPosition.x) > 5)
        {   
            SetReward(-1.0f);
            Done();
        }
        //print(Receiver.localPosition);
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
