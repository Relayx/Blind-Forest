using System;
using UnityEngine;

[Serializable]
public class Button2
{
    public string ButtonName;



    [HideInInspector]
    public bool Holded => Input.GetButton(ButtonName);

    [HideInInspector]
    public bool Pressed => KeyChange && Input.GetButton(ButtonName);

    [HideInInspector]
    public bool Released => KeyChange && !Input.GetButton(ButtonName);



    private int lastStart = -2;
    private int lastEnd = -1;

    private bool Last => lastStart > lastEnd;

    private bool KeyChange => HasChange(Input.GetButton(ButtonName));



    protected bool HasChange(bool current)
    {
        if(current == Last)
            return lastStart == Time.frameCount
                || lastEnd == Time.frameCount;

        if(current)
            lastStart = Time.frameCount;
        else
            lastEnd = Time.frameCount;

        return true;
    }
}
