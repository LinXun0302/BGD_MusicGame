using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
    TapNote,
    HoldNote,
    NONE,
}

public class NoteData
{
    public NoteType NoteType    = NoteType.TapNote;
    public int      NoteID      = 0;
    public int      TrackIndex  = 0;
    public float    NoteTime    = 0.0f;

    //HoldNote
    public float    HoldEndTime       = 0.0f;
    public int      HoldEndTrackIndex = 0;
    public bool     IsNeedTap         = true;
    public bool     IsNeedRelease     = true;
}

public class HoldNoteData : NoteData
{
    //public float HoldEndTime = 0.0f;
    //public int   HoldEndTrackIndex = 0;
    //public bool  IsNeedTap = true;
    //public bool  IsNeedRelease = true;
    //public bool  IsNeedEndSlide = false;
}
