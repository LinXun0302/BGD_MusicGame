using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track
{
    private List<TapNote> m_NoteList;
    public List<TapNote> GetTrackNoteList()
    {
        return m_NoteList;
    }
    public void AddTapNote(TapNote iTapNote)
    {
        m_NoteList.Add(iTapNote);
    }
    public TapNote GetClosestNote(float iTime)
    {
        TapNote aTapNote = null;
        float aTime = 0.5f;
        for (int index = 0; index < m_NoteList.Count;index++)
        {
            if (m_NoteList[index].GetIsActive())
            {
                float aTimeRange = Mathf.Abs(iTime - m_NoteList[index].GetNoteTime());
                if (aTimeRange <= aTime)
                {
                    aTapNote = m_NoteList[index];
                    aTime = aTimeRange;
                }
            }
        }
        return aTapNote;
    }
    public HoldNote GetHoldNoteByTouchID(int iID)
    {
        HoldNote aHoldNote = null;
        for (int index = 0; index < m_NoteList.Count; index++)
        {
            if (m_NoteList[index].GetIsActive() && m_NoteList[index].GetNoteType() == NoteType.HoldNote)
            {
                HoldNote aNote = m_NoteList[index] as HoldNote;
                if (aNote.HoldFingerID == iID)
                {
                    aHoldNote = aNote;
                }
            }
        }
        return aHoldNote;
    } 
    public Track()
    {
        m_NoteList = new List<TapNote>();
    }
}
