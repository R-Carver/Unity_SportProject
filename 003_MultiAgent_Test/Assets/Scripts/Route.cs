using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route
{   
    public int numWaypoints;
    public Vector3[] wayPoints;

    public Route(int numWaypoints)
    {
        this.numWaypoints = numWaypoints;
        wayPoints = new Vector3[numWaypoints];
    }

    public void addWayPoint(int position, Vector3 wayPoint)
    {
        wayPoints[position] = wayPoint;
    }
}
