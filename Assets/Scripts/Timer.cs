using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float value = 0;
    [SerializeField] private bool active = false;

    private void Update()
    {
        if (active)
        {
            value += Time.deltaTime;
        }
    }

    public static string ToText(float value)
    {
        int minuts = (int)value / 60;
        int seconds = (int)value % 60;
        return "0" + minuts + ":" + (seconds < 10? "0" + seconds : seconds);
    }

    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public void Activate()
    {
        active = true;
    }

    public void Stop()
    {
        active = false;
    }

}
