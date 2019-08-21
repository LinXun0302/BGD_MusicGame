using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapNote : MonoBehaviour
{
    protected NoteType m_NoteType;
    protected int      m_NoteID;
    protected int      m_TrackIndex;
    protected float    m_NoteTime;
    protected float    m_TrackSpeed;
    protected float    m_TrackLengthTime;
    protected bool     m_IsActive;

    protected TrackBackGround m_TrackBackGround;

    public NoteType GetNoteType()
    {
        return m_NoteType;
    }
    public int GetNoteID()
    {
        return m_NoteID;
    }
    public int GetTrackIndex()
    {
        return m_TrackIndex;
    }
    public float GetNoteTime()
    {
        return m_NoteTime;
    }
    public bool GetIsActive()
    {
        return m_IsActive;
    }

    public virtual void Initialize(NoteData iNoteData)
    {
        m_NoteType          = iNoteData.NoteType;
        m_NoteID            = iNoteData.NoteID;
        m_NoteTime          = iNoteData.NoteTime;
        m_TrackIndex        = iNoteData.TrackIndex;
        m_IsActive          = true;
    }

    public virtual void ChangeStateByJudge(NoteJudgment.Judgment judgment)
    {
        m_IsActive = false;
        gameObject.SetActive(false);
    }

    public void TrackSetUp(float iTrackSpeed, float iTrackLengthTime, TrackBackGround iTrackBackGround)
    {
        m_TrackSpeed = iTrackSpeed;
        m_TrackLengthTime = iTrackLengthTime;
        m_TrackBackGround = iTrackBackGround;
    }

    public virtual void UpdateNote(float iTime)
    {
        Vector3 aPosition = CaculatePositionAtTrackIndexByTime(m_NoteTime, iTime, m_TrackIndex);
        this.gameObject.transform.position = aPosition;
    }

    protected Vector3 CaculatePositionAtTrackIndexByTime(float iNoteTime, float iNowTime,int aTrackIndex)
    {
        Vector3 aPosition = new Vector3();
        float aNotePositionZ = (iNoteTime - iNowTime) * m_TrackSpeed;
        aNotePositionZ = aNotePositionZ + m_TrackBackGround.GetJudgeLinePosition().z;
        float aNotePositionX = m_TrackBackGround.GetTrackPointPosition(aTrackIndex).x;
        aPosition = new Vector3(aNotePositionX,0.01f,aNotePositionZ);
        return aPosition;
    }
}
