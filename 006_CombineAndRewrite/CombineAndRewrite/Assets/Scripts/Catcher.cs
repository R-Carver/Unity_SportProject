using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{   
    public bool caught = false;

    GameObject ball;

    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.name == "Ball")
        {   
            ball = other.gameObject;

            Rigidbody ballRb = other.gameObject.GetComponent<Rigidbody>();
            ballRb.useGravity = false;

            caught = true;

            if(GameController.GetInstance().defAgent != null)
            {
                GameController.GetInstance().defAgent.SetReward(1.0f);
            }

            if(GameController.GetInstance().passAgent != null)
            {
                GameController.GetInstance().passAgent.SetReward(-1.0f);
            }
            
            GameController.GetInstance().academy.Done();
        }
    }

    void Update() {

        if(caught == true)
        {
            ball.transform.position = this.transform.position;
        }
    }
}
