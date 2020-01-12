using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapNote : Note
{
    public override void Initialize(NoteData iNoteData)
    {
        m_NoteType          = iNoteData.NoteType;
        m_NoteID            = iNoteData.NoteID;
        m_NoteTime          = iNoteData.NoteTime;
        m_TrackIndex        = iNoteData.TrackIndex;
        m_IsActive          = true;
    }

    public override void ChangeStateByJudge(NoteJudgment.Judgment judgment)
    {
        m_IsActive = false;
        RecycleSelf();
    }

    public override void UpdateNote(float iTime)
    {
        Vector3 aPosition = CaculatePositionAtTrackIndexByTime(m_NoteTime, iTime, m_TrackIndex);
        this.gameObject.transform.position = aPosition;
    }
}
