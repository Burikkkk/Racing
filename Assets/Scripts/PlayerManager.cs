using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    
    private VehicleController vehicleController;

    private void Start()
    {
        vehicleController = GetComponent<VehicleController>();
    }

    void Update()
    {
        levelManager.lapsLeft = levelManager.laps - vehicleController.LapsFinished;
        if(vehicleController.LapsFinished == levelManager.laps)
            levelManager.FinishGame();
    }
}
