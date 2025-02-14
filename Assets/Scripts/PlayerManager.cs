using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    public float finishCountdown = 0.5f;
    private VehicleController vehicleController;
    private Timer finishTimer;

    private void Start()
    {
        finishTimer = gameObject.AddComponent<Timer>(); 
        vehicleController = GetComponent<VehicleController>();
    }

    void Update()
    {
        levelManager.lapsLeft = levelManager.laps - vehicleController.LapsFinished;
        if (vehicleController.LapsFinished == levelManager.laps && finishTimer!=null && !finishTimer.Active)
        {
            finishTimer.Activate();
            levelManager.finishText.SetActive(true);
        }

        if(finishTimer != null && finishTimer.Value > finishCountdown)
        {
            finishTimer.Value = 0;
            finishTimer.Stop();
            finishTimer = null;
            levelManager.finishText.SetActive(false);
            levelManager.FinishGame();

        }

    }
}
