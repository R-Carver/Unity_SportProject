using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PassAgent : Agent
{
    
    public Transform Receiver;

    public Transform Defender;
    public Transform ThrowDestination;
    public Ball_Controller ballController;
    TargetController targetController;
    //public PassAcademy academy;
    public EnvController_Combined envController;

    private bool ballThrown = false;

    // Start is called before the first frame update
    void Start()
    {
        targetController = ThrowDestination.GetComponent<TargetController>();
    }

    void Update()
    {
        
    }

    public bool BallWasThrown()
    {
        return this.ballThrown;
    }

    public override void AgentReset()
    {   
        ballThrown = false;
        resetCounter = 0;
    }

    public override void AgentOnDone()
    {   
    }

    public override void CollectObservations()
    {   
        //Num. of Observations: 5

        //In this version the receiver and the target can be
        //on a 2D Interval 
        //An alternative would be to give the route as observation
        AddVectorObs(Defender.localPosition.x);
        AddVectorObs(Defender.localPosition.z);
        AddVectorObs(Receiver.localPosition.x);
        AddVectorObs(Receiver.localPosition.z);
        AddVectorObs(ThrowDestination.localPosition.x);
        AddVectorObs(ThrowDestination.localPosition.z);

        // Added later for test
        AddVectorObs(ballThrown);
        
        if(ballThrown == true)
        {   
            // prevent any action when ball is thrown
            SetActionMask(1, 1);
            //SetActionMask(0, new int[2]{1, 2});
        }
    }

    // use this to prevent the agent from throwing before the target was repositioned
    private int resetCounter = 0;
    public override void AgentAction(float[] vectorAction, string textAction)
    {   
        // Get the action for movement
        int moveTarget = Mathf.FloorToInt(vectorAction[0]);
        
        // Get the action for throwing
        int doThrow = Mathf.FloorToInt(vectorAction[1]);

        // increse the counter so you can control whether to throw in the beginning
        resetCounter++;
        

        // dont move target
        if(moveTarget == 0) {targetController.MoveTarget(0);}
        // move target right
        if(moveTarget == 1) {targetController.MoveTarget(1);}
        // move target left
        if(moveTarget == 2) {targetController.MoveTarget(-1);}
        

        // check if you should shoot
        
        // third condition now tries to make sure that the receivers position was updated
        // after the reset 
        if(doThrow == 1 && !ballThrown && resetCounter > 5)
        {
            ballController.Launch();
            ballController.launched = true;
            ballThrown = true;
        }

        if(RouteController.Instance.currentRouteFinished == true)
        {
            SetReward(-0.5f);
            Done();
        }

        //simulate pressure - punish for not throwing
        AddReward(-0.002f);
    }
}
