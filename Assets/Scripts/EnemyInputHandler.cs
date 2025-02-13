using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputHandler : InputHandler
{
    public float Vertical
    {
        get { return vertical; }
        set { vertical = value; }
    }

    public float Horizontal
    {
        get { return horizontal; }
        set { horizontal = value; }
    }

    public int Handbrake
    {
        get { return handbrake; }
        set { handbrake = value; }
    }
}
