using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PassAgent_Horizontal_RandomSpawn : Agent
{
    public EnvironmentController EnvController;
    public Transform Target;
    public Transform ThrowDestination;
    public Ball_Controller_RL ballController;

    // Start is called before the first frame update
    void Start()
    {

        //this kicks off the decision making loop
        //this.RequestDecision();
    }

    void Update()
    {
        /* if(Target.position.z <= -5.0f)
        {
            Done();
        }*/
    }
    public override void AgentReset()
    {
        EnvController.resetEnv_RandomSpawn();
        TargetHit = false;
        TargetMissed = false;
        ballThrown = false;
        resetCounter = 0;

        //this.RequestDecision();
    }

    public override void CollectObservations()
    {
        // For now we try to only use the position of the receiver on his route
        // as observation for the passer
        // and the calculated target destination of the ball
        AddVectorObs(Target.position.z);
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

        // increse the counter so you can control whether to throw in the beginning
        resetCounter++;

        // move the target
        float changeZOfTarget = vectorAction[0];
        ballController.MoveTarget(changeZOfTarget);

        // check if you should shoot
        // as we only have float values between -1 and 1 we shoot when the value is above 0
        
        // third condition now tries to make sure that the receivers position was updated
        // after the reset 
        if(vectorAction[1] > 0 && !ballThrown && resetCounter > 2)
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
        }

        if(Target.position.z <= -5.0f)
        {
            Done();
        } 

        
        
    }

    
}
