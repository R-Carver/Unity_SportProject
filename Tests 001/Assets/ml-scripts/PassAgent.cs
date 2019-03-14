using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PassAgent : Agent
{
    public EnvironmentController EnvController;
    public Transform Target;
    public Transform ThrowDestination;

    private Ball_Controller_RL ballController;

    // Start is called before the first frame update
    void Start()
    {
        ballController = transform.GetComponent<Ball_Controller_RL>();
    }
    public override void AgentReset()
    {
        EnvController.resetEnv();
        TargetHit = false;
        ballThrown = false;
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
    private bool ballThrown = false;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // move the target
        float changeZOfTarget = vectorAction[0];
        ballController.MoveTarget(changeZOfTarget);

        // check if you should shoot
        // as we only have float values between -1 and 1 we shoot when the value is above 0
        // third condition is added because there was some bug in which the ball was thrown before
        // the scene was set up properply, so this just delays it a bit
        if(vectorAction[1] > 0 && !ballThrown && Target.position.z < 3)
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
