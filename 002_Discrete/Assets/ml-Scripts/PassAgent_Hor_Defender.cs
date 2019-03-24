using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PassAgent_Hor_Defender : Agent
{
    
    public Transform Receiver;
    public Transform ThrowDestination;
    public Ball_Controller_RL ballController;
    public PassAcademy academy;
    public EnvironmentController EnvController;

    private int episodeCounter = 0;
    private int hitCounter = 0;

    public override void AgentReset()
    {   
        EnvController.resetEnv();
        TargetHit = false;
        TargetMissed = false;
        ballThrown = false;
        resetCounter = 0;

        episodeCounter++;
    }

    public override void CollectObservations()
    {
        // For now we try to only use the position of the receiver on his route
        // as observation for the passer
        // and the calculated target destination of the ball
        AddVectorObs(Receiver.position.z);
        AddVectorObs(ThrowDestination.position.z);
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
            hitCounter++;
            print("hit " + hitCounter + " of " + episodeCounter);
            SetReward(1.0f);
            TargetHit = false;
            Done();
            //print("Done after done()  " + IsDone());

        }else if(TargetMissed == true)
        {
            Done();
            //print("Done after done()  " + IsDone());
        }

        // set small negative reward if the calculated destination is behind receiver 
        if(ThrowDestination.position.z > Receiver.position.z)
        {
            SetReward(-0.01f);
        }
    
    }

    
}
