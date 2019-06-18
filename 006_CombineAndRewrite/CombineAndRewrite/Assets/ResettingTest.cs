using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettingTest : MonoBehaviour
{   
    public DefAgent_ManCov defAgent;
    public bool testBool = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(testBool == true)
        {
            defAgent.episodeDone = true;   
        }
        
    }
}
