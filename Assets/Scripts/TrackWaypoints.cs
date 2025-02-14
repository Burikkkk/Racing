using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackWaypoints : MonoBehaviour
{
    [SerializeField] private bool looped;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private GameObject checkpointPrefab;

    private GameObject[] checkpoints;

    public Transform[] Waypoints 
    { 
        get { return waypoints; } 
    }

    public int GetNextWaypoint(int currentWaypoint, VehicleController vehicle)
    {
        if(currentWaypoint < waypoints.Length - 1)
        {
            if(vehicle.isPlayer)
            {
                SetCheckpoint(currentWaypoint, false);
                SetCheckpoint(currentWaypoint + 1, true);
            }
            return currentWaypoint + 1;
        }
        else if(looped)
        {
            if (vehicle.isPlayer)
            {
                SetCheckpoint(currentWaypoint, false);
                SetCheckpoint(0, true);
            }
            vehicle.AddLap();
            return 0;
        }
        else
        {
            vehicle.AddLap();
            return waypoints.Length - 1;
        }
    }

    private void Start()
    {
        waypoints = GetComponentsInChildren<Transform>();
        GenerateCheckpoints();
    }

    private void GenerateCheckpoints()
    {
        checkpoints = new GameObject[waypoints.Length];
        for(int i = 0; i < checkpoints.Length; i++)
        {
            var position = waypoints[i].position;
            position.y += 5.0f;
           
            checkpoints[i] = Instantiate(checkpointPrefab, position, Quaternion.identity, transform);
            checkpoints[i].SetActive(false);
        }
    }

    public void SetCheckpoint(int index, bool value)
    {
        checkpoints[index].SetActive(value);
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
