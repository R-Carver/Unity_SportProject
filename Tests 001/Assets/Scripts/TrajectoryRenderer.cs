using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 31;
        lineRenderer.widthMultiplier = 0.06f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTrajectory(Vector3[] points){
        
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPositions(points);
    }
}
