using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController_Combined : MonoBehaviour
{   
    public Transform receiver;

    private Vector3 receiverPos;
    private Quaternion receiverRotation;
    private RunMulti runMulti;
    private Transform ball;
    private Vector3 ballPos;
    private Quaternion ballRot;

    private TargetController targetController;

    private DefAgent_ManCov defAgent;

    // Start is called before the first frame update
    void Start()
    {
        receiverPos = receiver.position;
        receiverRotation = receiver.rotation;
        runMulti = receiver.GetComponent<RunMulti>();

        ball = GameObject.Find("Ball").transform;
        ballPos = ball.position;
        ballRot = ball.rotation;

        GameObject target = GameObject.Find("Target");
        targetController = target.GetComponent<TargetController>();

        GameObject defender = GameObject.Find("Defender");
        defAgent = defender.GetComponent<DefAgent_ManCov>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetEnv(){

        receiver.position = receiverPos;
        receiver.rotation = receiverRotation;

        //reset the route system
        RouteController.Instance.currentRouteFinished = false;
        runMulti.resetRoutes();

        //reset ball stuff
        ball.position = ballPos;
        ball.rotation = ballRot;

        Ball_Controller ballController = ball.gameObject.GetComponent<Ball_Controller>();
        ballController.ResetLauncher();

        targetController.ResetTarget();

        //since the PassAgent is controlling the resetting of the env
        //we have to reset the DefAgent manually here
        //defAgent.AgentReset();
        //defAgent.episodeDone = true;
        
    }

    
}
