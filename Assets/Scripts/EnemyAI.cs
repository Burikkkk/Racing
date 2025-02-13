using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float nextPointMargin;
    [SerializeField] private TrackWaypoints trackWaypoints;


    private float torqueMultiplier = 1.0f;
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
        inputHandler.Vertical = torqueMultiplier;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyTrigger"))
        {
            
            var trigger = other.gameObject.GetComponent<EnemyTrigger>();
            torqueMultiplier = trigger.TorqueMultiplier;
            inputHandler.Handbrake = trigger.Braking;
        }
    }
}
