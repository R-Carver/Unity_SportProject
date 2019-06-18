using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{   
    public PassAgent agent;
    public DefAgent_ManCov defAgent;
    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.name == "Ball")
        {   
            defAgent.episodeDone = true;
            agent.TargetHit = true;
            
            

        }
    }

    void Update(){
        //print("episode done" + defAgent.episodeDone);
    }
}
