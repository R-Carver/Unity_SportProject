using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker_Alt : MonoBehaviour
{
    public Agent_Walking_Alternative Agent; 
    
    private void OnTriggerEnter(Collider other) {
           
       
        Agent.SetReward(1f);
        Agent.Done(); 
        
    }
}
