using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : Singleton<TrackController>
{
//-----------------------------------------------
//Public
//-----------------------------------------------
    public override void Awake()
    {
        base.Awake();
        m_NoteManager = NoteManager.Instance;
        m_NoteJudgment = this.gameObject.AddComponent<NoteJudgment>();
        m_TrackList = new Dictionary<int, Track>();
        m_NoteMap = new NoteMap();
    }
//-----------------------------------------------
//private
//-----------------------------------------------
    private void Start()
    {
        m_NowTime = 0;
        m_TrackSpeed = 60;
        m_TrackLengthTime = m_TrackBackGround.GetTrackLength() / m_TrackSpeed;
        m_TrackMissTime   = m_TrackBackGround.GetJudgeLinePosition().z / m_TrackSpeed;
        m_NoteDataList    = m_NoteMap.GetNoteDataList();
    }

    private void Update()
    {
        UpdateTrack();
        m_NoteJudgment.JudgmentUpdate(m_NowTime, m_TrackList);
        m_NowTime += Time.deltaTime;
    }

    private void UpdateTrack()
    {
        int aListLength = m_NoteDataList.Count;
        for (int index = 0; index < aListLength; index++)
        {
            if (m_NoteDataList[index].NoteType == NoteType.TapNote)
            {
                if (m_NowTime - m_TrackMissTime <= m_NoteDataList[index].NoteTime && m_NowTime + m_TrackLengthTime >= m_NoteDataList[index].NoteTime)
                {
                    UpdateNote(m_NoteDataList[index]);
                }
            }
            else if (m_NoteDataList[index].NoteType == NoteType.HoldNote)
            {
                if (m_NowTime - m_TrackMissTime <= m_NoteDataList[index].HoldEndTime && m_NowTime + m_TrackLengthTime >= m_NoteDataList[index].NoteTime)
                {
                    UpdateNote(m_NoteDataList[index]);
                }
            }
        }
    }

    private void UpdateNote(NoteData iNoteData)
    {
        Note aNote;
        if (!m_NoteManager.CheckNoteIsSpawn(iNoteData.NoteID))
        {
            aNote = m_NoteManager.GetNote(iNoteData);
            aNote.TrackSetUp(m_TrackSpeed, m_TrackLengthTime, m_TrackBackGround);
            AddNoteToTrack(iNoteData.TrackIndex, aNote);
        }
        else
        {
            aNote = m_NoteManager.GetNote(iNoteData);
        }
        
        if (aNote.GetIsActive())
        {
            aNote.UpdateNote(m_NowTime);
        }
    }

    private void AddNoteToTrack(int iTrackIndex,Note iNote)
    {
        if (!m_TrackList.ContainsKey(iTrackIndex))
        {
            m_TrackList.Add(iTrackIndex,new Track());
        }
        m_TrackList[iTrackIndex].AddNote(iNote);
    }
//-----------------------------------------------
//Variables
//-----------------------------------------------
    [SerializeField]
    private TrackBackGround m_TrackBackGround = null;

    private NoteManager            m_NoteManager;
    private NoteJudgment           m_NoteJudgment;
    private NoteMap                m_NoteMap;
    private List<NoteData>         m_NoteDataList;
    private Dictionary<int, Track> m_TrackList;

    private float m_TrackSpeed;
    private float m_TrackLengthTime;
    private float m_TrackMissTime;
    private float m_NowTime;
}

