using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] protected float vertical;
    [SerializeField] protected float horizontal;
    [SerializeField] protected int handbrake;

    public float Vertical
    {
        get { return vertical; }
    }

    public float Horizontal
    {
        get { return horizontal; }
    }

    public int Handbrake
    {
        get { return handbrake; }
    }
}
