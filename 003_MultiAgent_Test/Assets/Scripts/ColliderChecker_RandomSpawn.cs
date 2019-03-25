using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker_RandomSpawn : MonoBehaviour
{   
    public PassAgent_Horizontal_RandomSpawn Agent;

    //Ball_Catcher catcher;
    // Start is called before the first frame update
    void Start()
    {
        //catcher = this.gameObject.GetComponent<Ball_Catcher>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {

        Agent.TargetHit = true; 
        //catcher.catchBall = true;   
    }
}
