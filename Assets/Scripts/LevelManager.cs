using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public int trackNumber;
    [SerializeField] private float startCountdown;
    [SerializeField] private Timer gameTimer;
    [SerializeField] private VehicleController[] vehicles;

    [SerializeField] private TMP_Text gameTimerText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text bestTimeText;
    [SerializeField] private TMP_Text currentTimeText;


    private Timer countdownTimer;
    private bool gameStarted = false;

    private void Start()
    {
        countdownTimer = gameObject.AddComponent<Timer>();

        countdownTimer.Activate();
    }

    private void Update()
    {
        gameTimerText.text = Timer.ToText(gameTimer.Value);
        if (countdownTimer != null && countdownTimer.Value < startCountdown)
            return;

        if (gameStarted)
            return;

        StartGame();
    }

    private void StartGame()
    {
        Destroy(countdownTimer);
        gameTimer.Activate();

        foreach (var vehicle in vehicles)
        {
            vehicle.enabled = true;
        }
        gameStarted = true;
    }

    public void FinishGame()
    {
        gameTimer.Stop();
        endPanel.SetActive(true);
        float bestTime, currentTime = gameTimer.Value;
        string keyName = "level" + trackNumber;
        if (!PlayerPrefs.HasKey(keyName))
        {
            PlayerPrefs.SetFloat(keyName, currentTime);
            bestTime = currentTime;
        }
        else
        {
            bestTime = PlayerPrefs.GetFloat(keyName);
            if(currentTime < bestTime)
            {
                bestTime = currentTime;
            }
        }
        bestTimeText.text = Timer.ToText(bestTime);
        currentTimeText.text = Timer.ToText(currentTime);
        Time.timeScale = 0.0f;
        //show menu
    }
}
