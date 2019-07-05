using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Collider on Receiver.
Checks whether the ball was caught by the receiver
 */
public class ColliderChecker : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.name == "Ball")
        {   
            //Ball was caught, so give reward for a good pass
            //and a punishment for the defender
            GameController.GetInstance().passAgent.SetReward(1.0f);

            if(GameController.GetInstance().defAgent != null)
            {
                GameController.GetInstance().defAgent.SetReward(-1.0f);
            }
            
            GameController.GetInstance().academy.Done();
        }
    }

    void Update(){
        //print("episode done" + defAgent.episodeDone);
    }
}
