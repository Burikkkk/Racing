using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private InputHandler inputHandler;
    private Rigidbody rb;


    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
    }

  
    void FixedUpdate()
    {
        MoveVehicle();
        SteerVehicle();
        AddDownforce();
        SpinWheels();

    }

    private void MoveVehicle()
    {
        foreach (var wheel in rearWheelsColliders)
        {
            wheel.motorTorque = torque * inputHandler.Vertical;
            wheel.brakeTorque = breakForce * inputHandler.Handbrake;
        }
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

    // add ackerman
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
