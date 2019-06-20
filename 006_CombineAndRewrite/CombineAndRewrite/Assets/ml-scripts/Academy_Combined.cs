using MLAgents;
using UnityEngine;

public class Academy_Combined : Academy
{   
    public float agentRunSpeed;

    public EnvController_Combined envController;

    public override void InitializeAcademy()
    {
        //Monitor.SetActive(true);
    }

    public override void AcademyStep()
    {

    }

    public override void AcademyReset()
    {  
        envController.resetEnv();
    }
}