using MLAgents;
using UnityEngine;

public class Academy_ManCoverage : Academy
{   
    public float agentRunSpeed;

    public override void InitializeAcademy()
    {
        Monitor.SetActive(true);
    }

    public override void AcademyStep()
    {

    }

    /* public override void AcademyReset()
    {  
        
    }*/
}