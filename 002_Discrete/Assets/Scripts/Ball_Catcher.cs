using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Catcher : MonoBehaviour
{   
    public GameObject ball;
    public bool catchBall = false;

    Rigidbody ballRB;
    // Start is called before the first frame update
    void Start()
    {
        ballRB = ball.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // add Ball catching here
        if(catchBall == true){
            
            // for now make the ball just take the position of the receiver
            ballRB.useGravity = false;
            ball.transform.position = this.transform.position + Vector3.up;
        }
    }
}
