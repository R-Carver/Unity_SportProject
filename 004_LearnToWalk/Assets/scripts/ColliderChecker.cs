using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    public Agent_Simple_Walking Agent; 
    
    private void OnTriggerEnter(Collider other) {
           
       
        Agent.SetReward(1f);
        Agent.Done(); 
        
    }
}
