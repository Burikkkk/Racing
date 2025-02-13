using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] protected float vertical = 0.0f;
    [SerializeField] protected float horizontal = 0.0f;
    [SerializeField] protected bool handbrake = false;

    public float Vertical
    {
        get { return vertical; }
    }

    public float Horizontal
    {
        get { return horizontal; }
    }

    public bool Handbrake
    {
        get { return handbrake; }
    }
}
