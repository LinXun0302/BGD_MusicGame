using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : TapNote
{
    public enum HoldNoteState {
        Tap,
        Hold,
        Relase,
        None
    }

    private LineRender m_LineRender;
    private HoldNoteState m_HoldNoteState;
    private int m_HoldFingerID;
    //HoldNoteData
    private float m_HoldEndTime;
    private int   m_HoldEndTrackIndex;
    private bool  m_IsNeedTap;
    private bool  m_IsNeedRelease;
    private bool  m_IsNeedEndSlide;

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
    public bool GetIsNeedEndSlide()
    {
        return m_IsNeedEndSlide;
    }
    public float GetNoteEndTime()
    {
        return m_HoldEndTime;
    }
    public int GetNoteEndTrack()
    {
        return m_HoldEndTrackIndex;
    }
    public int HoldFingerID
    {
        get
        {
            return m_HoldFingerID;
        }
        set
        {
            m_HoldFingerID = value;
        }
    }

    public override void Initialize(NoteData iNoteData)
    {
        base.Initialize(iNoteData);

        m_HoldEndTime       = iNoteData.HoldEndTime;
        m_HoldEndTrackIndex = iNoteData.HoldEndTrackIndex;
        m_IsNeedTap         = iNoteData.IsNeedTap;
        m_IsNeedRelease     = iNoteData.IsNeedRelease;
        m_IsNeedEndSlide    = iNoteData.IsNeedEndSlide;

        m_HoldNoteState = HoldNoteState.Tap;

        m_LineRender = this.gameObject.AddComponent<LineRender>();
    }

    public override void UpdateNote(float iTime)
    {
        base.UpdateNote(iTime);
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
            gameObject.SetActive(false);
        }
    }
    public void ChangeState(HoldNoteState iHoldNoteState)
    {
        m_HoldNoteState = iHoldNoteState;
    }
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
                float XpositionPercent =  (iTime - m_NoteTime) /(m_HoldEndTime - m_NoteTime);
                iStartPoint.x = (m_TrackBackGround.GetTrackPointPosition(m_HoldEndTrackIndex).x - m_TrackBackGround.GetTrackPointPosition(m_TrackIndex).x) * XpositionPercent;
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
}
