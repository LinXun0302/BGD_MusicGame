using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchTypes
{
    TouchDown,
    TouchHold,
    TouchRelease,
    Slide,
    None
}
public class InputData
{
    public TouchTypes TouchType;
    public int TouchID;
    public int TouchTrackIndex;
}
