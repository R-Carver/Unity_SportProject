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

    private bool ballThrown = false;

    // Start is called before the first frame update
    void Start()
    {
        //academy.AgentResetIfDone += Test;
    }

    void Update()
    {
        //print("agent done from agent: " + IsDone());
    }

    public bool BallWasThrown()
    {
        return this.ballThrown;
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
        //negRewardTest = 0;
    }

    public override void AgentOnDone()
    {   
    }

    public override void CollectObservations()
    {
        // For now we try to only use the position of the receiver on his route
        // as observation for the passer
        // and the calculated target destination of the ball
        AddVectorObs(Target.localPosition.z);
        AddVectorObs(ThrowDestination.localPosition.z);

        // Added later for test
        AddVectorObs(ballThrown);
        if(ballThrown == true)
        {   
            // prevent any action when ball is thrown
            SetActionMask(1, 1);
            //SetActionMask(0, new int[2]{1, 2});
        }
        //print("observing...");
    }

    // this is set from the Receiver Object when it is hit 
    public bool TargetHit = false;
    // this is set by the invisible wall behind the player
    public bool TargetMissed = false;
    

    //double negRewardTest = 0.0f;

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
            SetReward(1.0f);
            TargetHit = false;
            Done();

        }else if(TargetMissed == true)
        {   
            SetReward(-0.2f);
            Done();
        }

        // set small negative reward when aiming behind the receiver befor the ball is thrown
        //if(ThrowDestination.localPosition.z > Target.localPosition.z && ballThrown == false)
        if(ThrowDestination.localPosition.z > Target.localPosition.z)
        {
            SetReward(-0.01f);
        }

        if(Target.localPosition.z <= -5.0f)
        {
            SetReward(-1.0f);
            Done();
        }
    
    }

    
}
