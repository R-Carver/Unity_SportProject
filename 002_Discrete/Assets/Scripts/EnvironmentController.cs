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

    //public Ball_Catcher catcher;

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
        ball.rotation = ballRotation;
        
        Ball_Controller_RL ballController = ball.gameObject.GetComponent<Ball_Controller_RL>();
        ballController.ResetLauncher();
    }

    public void resetEnv_RandomSpawn(){
        
        // the only different thing here to the normal resetEnv is
        // the position of the receiver which is now random on the route

        // add a random value to the z position of the receiver
        //Vector3 randZpos = new Vector3(receiverPos.x , receiverPos.y, (receiverPos.z - Random.Range(0, 5)));

        receiver.position = receiverPos;
        receiver.rotation = receiverRotation;

        player.position = playerPos;
        player.rotation = playerRotation;

        // reset catcher
        //catcher.catchBall = false;

        // reset all ball stuff
        ball.position = ballPosition;
        ball.rotation = ballRotation;
        
        Ball_Controller_RL ballController = ball.gameObject.GetComponent<Ball_Controller_RL>();
        ballController.ResetLauncher();
    }
}
