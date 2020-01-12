using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Note : MonoBehaviour
{

//-----------------------------------------------
//Get;Set;
//-----------------------------------------------
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

//-----------------------------------------------
//Abstract
//-----------------------------------------------

    abstract public void Initialize(NoteData iNoteData);
    abstract public void ChangeStateByJudge(NoteJudgment.Judgment judgment);
    abstract public void UpdateNote(float iTime);

//-----------------------------------------------
//Public
//-----------------------------------------------
    public void TrackSetUp(float iTrackSpeed, float iTrackLengthTime, TrackBackGround iTrackBackGround)
    {
        m_TrackSpeed = iTrackSpeed;
        m_TrackLengthTime = iTrackLengthTime;
        m_TrackBackGround = iTrackBackGround;
    }

    public void RecycleSelf()
    {
        NoteManager.Instance.RecycleNote(this);
    }
//-----------------------------------------------
//Private
//-----------------------------------------------
    protected Vector3 CaculatePositionAtTrackIndexByTime(float iNoteTime, float iNowTime, int aTrackIndex)
    {
        Vector3 aPosition = new Vector3();
        float aNotePositionZ = (iNoteTime - iNowTime) * m_TrackSpeed;
        aNotePositionZ = aNotePositionZ + m_TrackBackGround.GetJudgeLinePosition().z;
        float aNotePositionX = m_TrackBackGround.GetTrackPointPosition(aTrackIndex).x;
        aPosition = new Vector3(aNotePositionX, 0.01f, aNotePositionZ);
        return aPosition;
    }

//-----------------------------------------------
//Variable
//-----------------------------------------------
    protected NoteType m_NoteType;
    protected int      m_NoteID;
    protected int      m_TrackIndex;
    protected float    m_NoteTime;
    protected float    m_TrackSpeed;
    protected float    m_TrackLengthTime;
    protected bool     m_IsActive;
    protected TrackBackGround m_TrackBackGround;
}
