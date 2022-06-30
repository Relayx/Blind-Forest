using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public string Axis;
    public float BufferedTime;

    [HideInInspector]
    public bool Pressed { get; private set; }

    [HideInInspector]
    public bool Released { get; private set; }

    private float pressedTimer;
    private float releasedTimer;

    private void Update()
    {
        if (Input.GetButtonDown(Axis)) 
        {
            Pressed = true;
            pressedTimer = BufferedTime;
        }
        if (Input.GetButtonUp(Axis)) 
        {
            Released = true;
            releasedTimer = BufferedTime;
        }

        if (Pressed && pressedTimer <= 0f)
        {
            Pressed = false;
        }
        if (Released && releasedTimer <= 0f)
        {
            Released = false;
        }

        pressedTimer -= Time.deltaTime;
        releasedTimer -= Time.deltaTime;
    }
}
