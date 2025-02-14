using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private float torque;
    [SerializeField] private float steeringMaxAngle;
    [SerializeField] private float downforce;
    [SerializeField] private float breakForce;
    [SerializeField] private WheelCollider[] frontWheelsColliders;
    [SerializeField] private WheelCollider[] rearWheelsColliders;
    [SerializeField] private GameObject[] frontWheelsMeshes;
    [SerializeField] private GameObject[] rearWheelsMeshes;
    [SerializeField] private TrackWaypoints trackWaypoints;
    [SerializeField] private float nextPointMargin;
    public bool isPlayer = false;

    private InputHandler inputHandler;
    private Rigidbody rb;
    private int lapsFinished;
    private int currentWaypoint = 0;
    private Transform nextWaypoint;


    private void Awake()
    {
        nextWaypoint = trackWaypoints.Waypoints[currentWaypoint];
    }

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
    }

  
    void FixedUpdate()
    {
        MoveVehicle();
        CheckWaypoints();
        //Debug.Log();
        SteerVehicle();
        AddDownforce();
        SpinWheels();

    }

    private void MoveVehicle()
    {
        foreach (var wheel in rearWheelsColliders)
        {

            wheel.motorTorque = torque * inputHandler.Vertical;

            if(inputHandler.Handbrake)
            {
                wheel.brakeTorque = breakForce;
            }
            else
            {
                wheel.brakeTorque = 0.0f;
            }

            //Vector3 direction = transform.InverseTransformDirection(rb.velocity).normalized;

            //if (inputHandler.Vertical > 0 && direction.z > 0) //moving forward
            //{
            //    wheel.motorTorque = torque * inputHandler.Vertical;
            //    wheel.brakeTorque = 0.0f;
            //}
            //else if(inputHandler.Vertical < 0 && direction.z < 0) //moving backwards
            //{
            //    wheel.motorTorque = torque * 0.7f * inputHandler.Vertical;
            //    wheel.brakeTorque = 0.0f;
            //}
            //else if(inputHandler.Vertical < 0 && direction.z > 0 ||
            //    inputHandler.Vertical > 0 && direction.z < 0)   // changing direction
            //{
            //    wheel.brakeTorque = breakForce;
            //}
        }
    }

    private void CheckWaypoints()
    {
        if (DistanceToNextWaypoint() < nextPointMargin)
        {
            currentWaypoint = trackWaypoints.GetNextWaypoint(currentWaypoint, this);
            nextWaypoint = trackWaypoints.Waypoints[currentWaypoint];
        }
    }

    private float DistanceToNextWaypoint()
    {
        Vector3 distance = transform.position - nextWaypoint.position;
        return distance.magnitude;
    }

    public Transform NextWaypoint
    {
        get { return nextWaypoint; }
    }

    public void AddLap()
    {
        Debug.Log("Here");
        lapsFinished++;
    }

    public int LapsFinished
    {
        get { return lapsFinished; }
    }

    private void SteerVehicle()
    {
        foreach (var wheel in frontWheelsColliders)
        {
            wheel.steerAngle = steeringMaxAngle * inputHandler.Horizontal;
        }
    }

    private void AddDownforce()
    {
        rb.AddForce(-transform.up * downforce * rb.velocity.magnitude);
    }

    private void SpinWheels()
    {
        Vector3 wheelPosition;
        Quaternion wheelRotation;

        for(int i = 0; i < frontWheelsMeshes.Length; i++)
        {
            var wheel = frontWheelsMeshes[i];
            frontWheelsColliders[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheel.transform.rotation = wheelRotation;
            wheel.transform.position= wheelPosition;
        }

        for (int i = 0; i < rearWheelsMeshes.Length; i++)
        {
            var wheel = rearWheelsMeshes[i];
            rearWheelsColliders[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheel.transform.rotation = wheelRotation;
            wheel.transform.position = wheelPosition;
        }
    }

    
}
