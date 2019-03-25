using MLAgents;
using UnityEngine;

public class PassAcademy : Academy
{   
    public float agentRunSpeed;

    public float agentJumpHeight;
    public float agentJumpVelocity = 777;
	public float agentJumpVelocityMaxChange = 10;
    //public EnvironmentController EnvController;
    //public Transform Target;
    //public PassAgent_Horizontal_RandomSpawn PassAgent;

    /* public override void AcademyStep()
    {
        if(Target.position.z <= -5.0f)
        {
            Done();
            
        } 
        //print("agent done from academy: " + PassAgent.IsDone());
    }*/

    /* public override void AcademyReset()
    {  
        
        //PassAgent.AgentReset();
        EnvController.resetEnv_RandomSpawn();
        
    }*/
}