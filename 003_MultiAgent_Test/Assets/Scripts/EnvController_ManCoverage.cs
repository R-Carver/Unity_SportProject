using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController_ManCoverage : MonoBehaviour
{   
    public Transform receiver;

    private Vector3 receiverPos;
    private Quaternion receiverRotation;
    private RunMulti runMulti;

    // Start is called before the first frame update
    void Start()
    {
        receiverPos = receiver.position;
        receiverRotation = receiver.rotation;
        runMulti = receiver.GetComponent<RunMulti>();
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

        //reset the route system
        RouteController.Instance.currentRouteFinished = false;
        runMulti.resetRoutes();
        
    }

    
}
