using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunVertical_ManCoverage : MonoBehaviour
{
    public int speed = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //for now we need to move him in the negative z axis
        Vector3 moveDir = new Vector3(0, 0, -1);
        transform.Translate(moveDir * speed * Time.deltaTime);
    }
}
