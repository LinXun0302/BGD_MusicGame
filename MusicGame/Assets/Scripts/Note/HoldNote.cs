using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : Note
{
//-----------------------------------------------
//Enum
//-----------------------------------------------
    public enum HoldNoteState {
        Tap,
        Hold,
        Relase,
        None
    }
//-----------------------------------------------
//Get;Set;
//-----------------------------------------------
#region
    public HoldNoteState GetHoldNoteState()
    {
        return m_HoldNoteState;
    }
    public bool GetIsNeedTap()
    {
        return m_IsNeedTap;
    }
    public bool GetIsNeedRelease()
    {
        return m_IsNeedRelease;
    }
    public float GetNoteEndTime()
    {
        return m_HoldEndTime;
    }
    public int GetNoteEndTrack()
    {
        return m_HoldEndTrackIndex;
    }
    public int HoldFingerID { get; set; }
    #endregion

//-----------------------------------------------
//Public
//-----------------------------------------------
    public override void Initialize(NoteData iNoteData)
    {
        m_NoteType = iNoteData.NoteType;
        m_NoteID = iNoteData.NoteID;
        m_NoteTime = iNoteData.NoteTime;
        m_TrackIndex = iNoteData.TrackIndex;
        m_IsActive = true;

        m_HoldEndTime       = iNoteData.HoldEndTime;
        m_HoldEndTrackIndex = iNoteData.HoldEndTrackIndex;
        m_IsNeedTap         = iNoteData.IsNeedTap;
        m_IsNeedRelease     = iNoteData.IsNeedRelease;

        m_HoldNoteState = HoldNoteState.Tap;

        if (m_LineRender == null)
        {
            m_LineRender = this.gameObject.AddComponent<LineRender>();
        }
    }

    public override void UpdateNote(float iTime)
    {
        Vector3 aPosition = CaculatePositionAtTrackIndexByTime(m_NoteTime, iTime, m_TrackIndex);
        this.gameObject.transform.position = aPosition;

        if (iTime >= m_HoldEndTime && m_HoldNoteState == HoldNoteState.Hold && !m_IsNeedRelease)
        {
            m_IsActive = false;
            RecycleSelf();
        }
        UpdateLineRender(iTime);
    }

    public override void ChangeStateByJudge(NoteJudgment.Judgment judgment)
    {
        if (m_HoldNoteState == HoldNoteState.Tap)
        { 
            if (judgment != NoteJudgment.Judgment.Miss)
            {
                m_HoldNoteState = HoldNoteState.Hold;
            }
            else
            {
                m_IsActive = false;
                gameObject.SetActive(false);
            }
        }
        else if (m_HoldNoteState == HoldNoteState.Hold)
        {
            m_IsActive = false;
            RecycleSelf();
        }
    }

    public void ChangeState(HoldNoteState iHoldNoteState)
    {
        m_HoldNoteState = iHoldNoteState;
    }

//-----------------------------------------------
//Private
//-----------------------------------------------
    private void UpdateLineRender(float iTime)
    {
        Vector3 iStartPoint = this.transform.position;
        Vector3 iEndPoint;
        switch (m_HoldNoteState)
        {
            case HoldNoteState.Tap:
                iStartPoint = this.transform.position;
                break;
            case HoldNoteState.Hold:
                iStartPoint.z = m_TrackBackGround.GetJudgeLinePosition().z;
                float XpositionPercent = (iTime - m_NoteTime) /(m_HoldEndTime - m_NoteTime);
                iStartPoint.x = (m_TrackBackGround.GetTrackPointPosition(m_HoldEndTrackIndex).x - m_TrackBackGround.GetTrackPointPosition(m_TrackIndex).x) * XpositionPercent + m_TrackBackGround.GetTrackPointPosition(m_TrackIndex).x;
                break;
        }
        if (m_HoldEndTime - iTime > m_TrackLengthTime)
        {
            iEndPoint = CaculatePositionAtTrackIndexByTime(iTime + m_TrackLengthTime, iTime, m_HoldEndTrackIndex);
        }
        else
        {
            iEndPoint = CaculatePositionAtTrackIndexByTime(m_HoldEndTime, iTime, m_HoldEndTrackIndex);
        }
        m_LineRender.ChangeLinePoint(iStartPoint, iEndPoint);
    }

//-----------------------------------------------
//Variables
//-----------------------------------------------
    private LineRender m_LineRender;
    private HoldNoteState m_HoldNoteState;
    private float m_HoldEndTime;
    private int m_HoldEndTrackIndex;
    private bool m_IsNeedTap;
    private bool m_IsNeedRelease;
}
