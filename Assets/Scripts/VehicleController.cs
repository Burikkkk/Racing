using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private float torque;
    [SerializeField] private float steeringMaxAngle;
    [SerializeField] private WheelCollider[] frontWheelsColliders;
    [SerializeField] private WheelCollider[] rearWheelsColliders;
    [SerializeField] private GameObject[] frontWheelsMeshes;
    [SerializeField] private GameObject[] rearWheelsMeshes;


    void Start()
    {
        
    }

  
    void FixedUpdate()
    {
        // вперед/назад - в отдельную функцию
        // тормозить быстрее, чем разгоняться
        foreach (var wheel in rearWheelsColliders)
        {
            // инпут в отдельные классы
            wheel.motorTorque = torque * Input.GetAxis("Vertical");
        }

        foreach (var wheel in frontWheelsColliders)
        {
            wheel.steerAngle = steeringMaxAngle * Input.GetAxis("Horizontal");
        }

        RotateWheels();

    }

    private void RotateWheels()
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
