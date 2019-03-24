using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class DefAgent_Hor : Agent
{

    public PassAgent_Hor_Defender passAgent;
    public Transform Ball;
    public Transform Receiver;

    Rigidbody agentRB;
    PassAcademy academy;
    EnvironmentController envController;

    public float fallingForce;
    public float jumpingTime;

    Vector3 jumpTargetPos;
    Vector3 jumpStartingPos;

    Quaternion startRotation;

    public override void InitializeAgent()
    {   
        academy = FindObjectOfType<PassAcademy>();
        envController = FindObjectOfType<EnvironmentController>();
        agentRB = GetComponent<Rigidbody>();
        startRotation = transform.rotation;
    }

    public override void AgentReset()
    {   
        passAgent.Done();
        transform.position = new Vector3(Random.Range(-2,2), transform.position.y, Random.Range(-3, 3));
        transform.rotation = startRotation;
    }

    public override void CollectObservations()
    {   
        // observations for now are the position of the reveiver and the position of the ball
        AddVectorObs(Receiver.position);
        AddVectorObs(Ball.position);
        AddVectorObs(transform.position);
    }

    // for now try to move the agent just by moving foward/backward and changing the rotation
    // so in the first iteration there are 2 discrete arrays with 3 values each (like: dont move, move forward, move backward)
    public override void AgentAction(float[] vectorAction, string textAction)
    {   
        MoveAgent(vectorAction);

        // punish the agent if he leaves his assigned area
        if(agentRB.position.z > 4 || agentRB.position.z < -4 || agentRB.position.x > 2.5f || agentRB.position.x < -2.5f)
        {   
            SetReward(-1f);
            Done();
        }
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
        int jumpAction = (int)act[2]; 

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

        if (jumpAction == 1)
            // if not jumping allready
            if ((jumpingTime <= 0f) && transform.position.y <= 1)
            {
                Jump();
            }

        // dont move if jumping
        if(transform.position.y <= 1)
        {
            transform.Rotate(rotateDir, Time.fixedDeltaTime * 300f);
            agentRB.AddForce(dirToGo * academy.agentRunSpeed , ForceMode.VelocityChange);
        }
        

        if (jumpingTime > 0f)
        {
            jumpTargetPos =
            new Vector3(agentRB.position.x,
                        jumpStartingPos.y + academy.agentJumpHeight,
                        agentRB.position.z) /* + dirToGo*/;
            MoveTowards(jumpTargetPos, agentRB, academy.agentJumpVelocity,
                        academy.agentJumpVelocityMaxChange);

        }

        // come down again
        if (!(jumpingTime > 0f))
        {
            agentRB.AddForce(
            Vector3.down * fallingForce, ForceMode.Acceleration);
        }
        jumpingTime -= Time.fixedDeltaTime;

    }

     // Begin the jump sequence
    public void Jump()
    {

        jumpingTime = 0.2f;
        jumpStartingPos = agentRB.position;
    }

    // used in jumping, copied from the WallJumpAgent
    void MoveTowards(Vector3 targetPos, Rigidbody rb, float targetVel, float maxVel)
    {
        Vector3 moveToPos = targetPos - rb.worldCenterOfMass;
        Vector3 velocityTarget = moveToPos * targetVel * Time.fixedDeltaTime;
        if (float.IsNaN(velocityTarget.x) == false)
        {
            rb.velocity = Vector3.MoveTowards(
                rb.velocity, velocityTarget, maxVel);
        }
    }

    private void FixedUpdate() 
    {   
        /* if(Input.GetKeyDown(KeyCode.Space))
            //agentRB.AddForce(transform.up * 200, ForceMode.Impulse);
            Jump();   */ 
    }

    
}
