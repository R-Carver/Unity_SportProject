using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheckerGround : MonoBehaviour
{
    public PassAgent agent;

    public DefAgent_ManCov defAgent;
    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.name == "Ball"){

            if(agent.TargetHit == false)
            {   
                defAgent.episodeDone = true;
                agent.TargetMissed = true;
            }
        }
        
        
    }
}
