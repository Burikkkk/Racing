using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float nextPointMargin;
    [SerializeField] private TrackWaypoints trackWaypoints;
    
    
    private int currentWaypoint = 0;
    private EnemyInputHandler inputHandler;
    private Transform target;

    private void Start()
    {
        inputHandler = GetComponent<EnemyInputHandler>();
        target = trackWaypoints.Waypoints[currentWaypoint];
    }

    // will break on finish
    private void FixedUpdate()
    {
        if(DistanceToTarget() < nextPointMargin)
        {
            currentWaypoint = trackWaypoints.GetNextWaypoint(currentWaypoint);
            //destroy on end
            target = trackWaypoints.Waypoints[currentWaypoint];
        }

        inputHandler.Horizontal = AngleToTarget();
        inputHandler.Vertical = 1f;
    }

    private float AngleToTarget()
    {
        Vector3 relative = transform.InverseTransformPoint(target.position);
        relative = relative.normalized;

        return relative.x;
    }

    private float DistanceToTarget()
    {
        // local global???
        Vector3 distance = transform.position - target.position;
        return distance.magnitude;
    }
}
