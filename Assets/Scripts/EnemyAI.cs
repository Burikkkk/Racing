using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    private float torqueMultiplier = 1.0f;
    private EnemyInputHandler inputHandler;
    private VehicleController vehicleController;
    private Transform target;

    private void Start()
    {
        inputHandler = GetComponent<EnemyInputHandler>();
        vehicleController = GetComponent<VehicleController>();
        target = vehicleController.NextWaypoint;
    }

    private void FixedUpdate()
    {
        target = vehicleController.NextWaypoint;
        inputHandler.Horizontal = AngleToTarget();
        inputHandler.Vertical = torqueMultiplier;
    }

    private float AngleToTarget()
    {
        Vector3 relative = transform.InverseTransformPoint(target.position);
        relative = relative.normalized;

        return relative.x;
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
