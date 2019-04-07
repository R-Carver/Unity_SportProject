using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    public DefAgent_ManCoverage01 Agent; 
    
    private void OnTriggerStay(Collider other) {
           
        if(other.name == "Defender"){
            Agent.AddReward(0.005f);
            //print("covering");
        }
        //Agent.SetReward(1f);
        //Agent.Done(); 
        
    }
}