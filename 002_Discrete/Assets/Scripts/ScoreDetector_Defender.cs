using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetector_Defender : MonoBehaviour
{   
    public PassAgent_Hor_Defender Agent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        Agent.TargetMissed = true; 
    }
}
