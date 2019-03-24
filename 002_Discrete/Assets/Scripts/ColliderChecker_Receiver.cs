using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker_Receiver : MonoBehaviour
{   
    public PassAgent_Hor_Defender Agent;

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
    }
}
