using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private float torqueMultiplier; //ускорение
    [SerializeField] private bool braking; //остановка

    public float TorqueMultiplier
    {
        get { return torqueMultiplier; }
    }

    public bool Braking
    {
        get { return braking; }
    }
}
