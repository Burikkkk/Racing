using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private float torque; // крутящий момент
    [SerializeField] private float steeringMaxAngle; // поворот колес
    [SerializeField] private float downforce; // прижимает машину к земле, увеличивается со скоростью
    [SerializeField] private float breakForce; //торможение
    [SerializeField] private WheelCollider[] frontWheelsColliders;
    [SerializeField] private WheelCollider[] rearWheelsColliders;
    [SerializeField] private GameObject[] frontWheelsMeshes;
    [SerializeField] private GameObject[] rearWheelsMeshes;

    private InputHandler inputHandler;
    private Rigidbody rb;


    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>(); //силы, физика
    }

  
    void FixedUpdate()
    {
        MoveVehicle();
        //Debug.Log();
        SteerVehicle();
        AddDownforce();
        SpinWheels();

    }

    private void MoveVehicle()
    {
        foreach (var wheel in rearWheelsColliders) //задний привод
        {
            Vector3 direction = transform.InverseTransformDirection(rb.velocity).normalized; //нормализация вектора скорорсти [-1;1] по z

            if (inputHandler.Vertical > 0 && direction.z > 0) //движение вперед
            {
                wheel.motorTorque = torque * inputHandler.Vertical; 
                wheel.brakeTorque = 0.0f;
            }
            else if(inputHandler.Vertical < 0 && direction.z < 0) //движение назад
            {
                wheel.motorTorque = torque * 0.7f * inputHandler.Vertical;
                wheel.brakeTorque = 0.0f;
            }
            else if(inputHandler.Vertical < 0 && direction.z > 0 ||
                inputHandler.Vertical > 0 && direction.z < 0)   // изменение направления
            {
                wheel.brakeTorque = breakForce; //торможение
            }

            if(inputHandler.Handbrake)
            {
                wheel.brakeTorque = breakForce;
            }
            else
            {
                wheel.brakeTorque = 0.0f;
            }
        }
    }

    private void SteerVehicle()
    {
        foreach (var wheel in frontWheelsColliders)
        {
            wheel.steerAngle = steeringMaxAngle * inputHandler.Horizontal; //поворот колес
        }
    }

    private void AddDownforce()
    {
        rb.AddForce(-transform.up * downforce * rb.velocity.magnitude);//хз.
    }

    // add ackerman
    private void SpinWheels() //вращение колес
    {
        Vector3 wheelPosition;
        Quaternion wheelRotation;

        for(int i = 0; i < frontWheelsMeshes.Length; i++)
        {
            var wheel = frontWheelsMeshes[i];
            frontWheelsColliders[i].GetWorldPose(out wheelPosition, out wheelRotation); //движение модели за колайдером
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
