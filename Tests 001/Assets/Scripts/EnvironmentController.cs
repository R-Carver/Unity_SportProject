using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{   
    private Vector3 receiverPos;
    private Quaternion receiverRotation;

    private Vector3 playerPos;
    private Quaternion playerRotation;

    private Vector3 ballPosition;
    private Quaternion ballRotation;
    
    public Transform receiver;
    public Transform player;

    public Transform ball;

    // Start is called before the first frame update
    void Start()
    {
        receiverPos = receiver.position;
        receiverRotation = receiver.rotation;

        playerPos = player.position;
        playerRotation = player.rotation;

        ballPosition = ball.position;
        ballRotation = ball.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){

            resetEnv();
        }
    }

    public void resetEnv(){

        receiver.position = receiverPos;
        receiver.rotation = receiverRotation;

        player.position = playerPos;
        player.rotation = playerRotation;

        // reset all ball stuff
        ball.position = ballPosition;
        ball.rotation = ballRotation;/* 
        Rigidbody ballRB = ball.gameObject.GetComponent<Rigidbody>();
        ballRB.useGravity = false;
        ballRB.velocity = Vector3.zero;
        ballRB.angularVelocity = Vector3.zero;*/
        Ball_Controller_RL ballController = ball.gameObject.GetComponent<Ball_Controller_RL>();
        ballController.ResetLauncher();
    }
}
