using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheckerGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.name == "Ball"){
            
            GameController.GetInstance().passAgent.SetReward(-0.1f);

            GameController.GetInstance().academy.Done();

            /* if(agent.TargetHit == false)
            {   
                //defAgent.episodeDone = true;
                //agent.TargetMissed = true;
                GameController.GetInstance().academy.Done();
            }*/
        }
        
        
    }
}
