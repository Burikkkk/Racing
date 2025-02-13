using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackWaypoints : MonoBehaviour
{
    [SerializeField] private bool looped;
    [SerializeField] private Transform[] waypoints;
    
    public Transform[] Waypoints 
    { 
        get { return waypoints; } 
    }

    public int GetNextWaypoint(int currentWaypoint)
    {
        if(currentWaypoint < waypoints.Length - 1)
        {
            return currentWaypoint + 1;
        }
        else if(looped)
        {
            return 0;
        }
        else
        {
            //end of route
            return waypoints.Length - 1;
        }
    }

    private void Start()
    {
        waypoints = GetComponentsInChildren<Transform>();
    }

    private void OnDrawGizmos()
    {
        waypoints = GetComponentsInChildren<Transform>();
        for (int i = 1; i < waypoints.Length; i++)
        {
            Vector3 currentWaypoint = waypoints[i].position;
            Vector3 previousWaypoint = waypoints[i - 1].position;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(previousWaypoint, currentWaypoint);
            Gizmos.DrawSphere(currentWaypoint, 1);
        }
        if(looped)
        {
            Gizmos.DrawLine(waypoints[0].position, waypoints[waypoints.Length - 1].position);
        }
    }
}
