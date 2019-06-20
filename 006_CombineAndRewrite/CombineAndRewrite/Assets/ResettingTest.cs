using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class ResettingTest : MonoBehaviour
{   

    Academy_Combined academy;
    // Start is called before the first frame update
    void Start()
    {
        GameObject academyGo = GameObject.Find("Academy");
        academy = academyGo.GetComponent<Academy_Combined>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {   
            print("should reset the academy");
            academy.Done();
        }
        
    }
}
