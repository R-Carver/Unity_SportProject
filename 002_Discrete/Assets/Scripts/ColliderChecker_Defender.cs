using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker_Defender : MonoBehaviour
{   
    public DefAgent_Hor Agent;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        
        if(other.name == "Ball")
        {
            Agent.SetReward(1f);
            Agent.Done(); 
        }
        
    }
}
