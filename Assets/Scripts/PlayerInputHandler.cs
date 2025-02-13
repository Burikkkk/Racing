using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : InputHandler
{

    // Update is called once per frame
    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        handbrake = Input.GetKey(KeyCode.Space) == true? 1 : 0;
    }
}
