using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller_Simple_Force : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(Input.GetKeyDown("space")){
            rb.AddForce(transform.forward * thrust, ForceMode.Impulse);
        }
    }
}
