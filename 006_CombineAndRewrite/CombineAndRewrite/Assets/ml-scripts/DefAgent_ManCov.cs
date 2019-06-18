using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class DefAgent_ManCov : Agent
{
    public Transform Receiver;
    public EnvController_Combined envController;

    //we need the RouteController to determine whether the routes are done
    RouteController routeController;

    Rigidbody agentRB;
    Academy_Combined academy;
    

    Quaternion startRotation;
    Vector3 startPosition;

    public bool episodeDone = false;

    public override void InitializeAgent()
    {   
        academy = FindObjectOfType<Academy_Combined>();
        agentRB = GetComponent<Rigidbody>();
        startRotation = transform.rotation;
        startPosition = transform.localPosition;
    }

    public override void AgentReset()
    {   
        // for now the PassAgent is resetting the env
        //this should be centralised later
        //envController.resetEnv();
        transform.localPosition = startPosition;
        transform.rotation = startRotation;
        episodeDone = false;
        //print("defAgent reset");
    }

    public override void CollectObservations()
    {   
        AddVectorObs(Receiver.localPosition);
        AddVectorObs(transform.localPosition);
    }

    // for now try to move the agent just by moving foward/backward and changing the rotation
    // so in the first iteration there are 2 discrete arrays with 3 values each (like: dont move, move forward, move backward)
    public override void AgentAction(float[] vectorAction, string textAction)
    {   
        routeController = RouteController.Instance;
        MoveAgent(vectorAction);

        //version with many routes
        if(routeController.currentRouteFinished == true)
        {   
            print("reaction to finished route");
            Done();
        }

        if(episodeDone == true)
        {
            Done();
        }

        // punish for leaving the area 
        if(Math.Abs(transform.localPosition.z) > 6 || Math.Abs(transform.localPosition.x) > 5)
        {
            AddReward(-0.005f);
        }
    }

    public void MoveAgent(float[] act)
    {   
        // Alternative version for the movement:
        // The idea here is that we don't have to figure out the rotation, but we only move
        // Globally right-left and up-down and adjust the rotation after that

        // punish for beeing idle
        AddReward(-0.001f);
        Vector3 moveVertical = Vector3.zero;
        Vector3 moveHorizontal = Vector3.zero;

        // get the move signals
        int verticalSignal = (int) act[0];
        int horizontalSignal = (int) act[1];

        if(verticalSignal == 1)
            // move forward
            moveVertical = Vector3.forward * 1f;
        else if(verticalSignal == 2)
            // move back
            moveVertical = Vector3.forward * -1f;

        if(horizontalSignal == 1)
            // move left
            moveHorizontal = Vector3.right * -1f;
        else if(horizontalSignal == 2)
            // move right
            moveHorizontal = Vector3.right * 1f;

        Vector3 moveVector = moveHorizontal + moveVertical;

        // only change the rotation when you have a movement in some direction
        if(!(verticalSignal == 0 && horizontalSignal == 0))
        {
            Quaternion walkRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            transform.rotation = walkRotation;
        }

        Vector3 target = transform.position + moveVector;
        float speed = academy.agentRunSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        
    }

    private void FixedUpdate() 
    {   
        // try to correct the rotation issue
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y, 0 );
    }
}
