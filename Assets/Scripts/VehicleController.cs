using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private float torque;
    [SerializeField] private WheelCollider[] frontWheels;
    [SerializeField] private WheelCollider[] rearWheels;


    void Start()
    {
        
    }

  
    void FixedUpdate()
    {
        foreach (var wheel in rearWheels)
        {
            wheel.motorTorque = torque * Input.GetAxis("Vertical");
        }
    }


    
}
