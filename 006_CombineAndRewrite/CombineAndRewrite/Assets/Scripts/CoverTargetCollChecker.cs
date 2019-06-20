using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverTargetCollChecker : MonoBehaviour
{
    private void OnTriggerStay(Collider col)
    {
        if(col.name == "Defender")
        {
            GameController.GetInstance().defAgent.AddReward(0.005f);
        }
    }
}
