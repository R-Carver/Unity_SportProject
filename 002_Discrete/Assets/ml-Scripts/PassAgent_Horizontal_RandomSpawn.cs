using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PassAgent_Horizontal_RandomSpawn : Agent
{
    
    public Transform Target;
    public Transform ThrowDestination;
    public Ball_Controller_RL ballController;
    public PassAcademy academy;
    public EnvironmentController EnvController;

    // Start is called before the first frame update
    void Start()
    {
        //academy.AgentResetIfDone += Test;
    }

    /* void Test()
    {
        print("Testfunc");
    }*/

    void Update()
    {
        //print("agent done from agent: " + IsDone());
    }

    public override void AgentReset()
    {   
        EnvController.resetEnv_RandomSpawn();
        TargetHit = false;
        TargetMissed = false;
        ballThrown = false;
        resetCounter = 0;
        //print("Agent reset");
        //print("IdDone  " + IsDone());
    }

    public override void AgentOnDone()
    {   
        /* TargetHit = false;
        TargetMissed = false;
        print("viech");*/
    }

    public override void CollectObservations()
    {
        // For now we try to only use the position of the receiver on his route
        // as observation for the passer
        // and the calculated target destination of the ball
        AddVectorObs(Target.position.z);
        AddVectorObs(ThrowDestination.position.z);
        //print("observing...");
    }

    // this is set from the Receiver Object when it is hit 
    public bool TargetHit = false;
    // this is set by the invisible wall behind the player
    public bool TargetMissed = false;
    private bool ballThrown = false;

    // use this to prevent the agent from throwing before the target was repositioned
    private int resetCounter = 0;
    public override void AgentAction(float[] vectorAction, string textAction)
    {   
        // Get the action for movement
        int moveTarget = Mathf.FloorToInt(vectorAction[0]);
        //print("moveTarget  " + moveTarget);
        
        // Get the action for throwing
        int doThrow = Mathf.FloorToInt(vectorAction[1]);

        // increse the counter so you can control whether to throw in the beginning
        resetCounter++;
        

        // dont move target
        if(moveTarget == 0) {ballController.MoveTarget(0);}
        // move target right
        if(moveTarget == 1) {ballController.MoveTarget(1);}
        // move target left
        if(moveTarget == 2) {ballController.MoveTarget(-1);}
        

        // check if you should shoot
        
        // third condition now tries to make sure that the receivers position was updated
        // after the reset 
        if(doThrow == 1 && !ballThrown && resetCounter > 5)
        {
            ballController.Launch();
            ballController.launched = true;
            ballThrown = true;
        }

        if(TargetHit == true)
        {   
            print("mops");
            SetReward(1.0f);
            TargetHit = false;
            Done();
            //print("Done after done()  " + IsDone());

        }else if(TargetMissed == true)
        {
            Done();
            //print("Done after done()  " + IsDone());
        }


        // Probably not needed any more, should only happen if there
        // is a bug
        if(ballController.ballOfPlatform == true)
        {   
            print("ball of platform");
            SetReward(-10);
            ballController.ballOfPlatform = false;
        }

    
    }

    
}
