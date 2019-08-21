using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
    TapNote,
    HoldNote,
    SlideNote,
}
public class NoteData
{
    public NoteType NoteType    = NoteType.TapNote;
    public int      NoteID      = 0;
    public int      TrackIndex  = 0;
    public float    NoteTime    = 0.0f;
    public bool     IsActive    = true;

    //HoldNote
    public float    HoldEndTime       = 0.0f;
    public int      HoldEndTrackIndex = 0;
    public bool     IsNeedTap         = true;
    public bool     IsNeedRelease     = true;
    public bool     IsNeedEndSlide    = false;
}
