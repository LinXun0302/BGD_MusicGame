using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    [SerializeField]
    private TrackBackGround m_TrackBackGround = null;

    private NoteManager m_NoteManager;
    private NoteJudgment m_NoteJudgment;
    private NoteMap m_NoteMap;
    private List<NoteData> m_NoteDataList;
    private Dictionary<int, Track> m_TrackList;

    private float m_TrackSpeed;
    private float m_TrackLengthTime;
    private float m_NowTime;

    private void Awake()
    {
        m_NoteManager = this.gameObject.AddComponent<NoteManager>();
        m_NoteJudgment = this.gameObject.AddComponent<NoteJudgment>();
        m_TrackList = new Dictionary<int, Track>();
        m_NoteMap = new NoteMap();
    }

    private void Start()
    {
        m_NowTime = 0;
        m_TrackSpeed = 50;
        m_TrackLengthTime = m_TrackBackGround.GetTrackLength() / m_TrackSpeed;
        m_NoteDataList = m_NoteMap.GetNoteDataList();
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
            if (m_NowTime + m_TrackLengthTime >= m_NoteDataList[index].NoteTime)
            {
                UpdateNote(m_NoteDataList[index]);
            }
        }
    }

    private void UpdateNote(NoteData iNoteData)
    {
        TapNote aTapNote;
        if (!m_NoteManager.CheckNoteIsSpawn(iNoteData.NoteID))
        {
            aTapNote = m_NoteManager.GetNote(iNoteData);
            aTapNote.TrackSetUp(m_TrackSpeed, m_TrackLengthTime, m_TrackBackGround);
            AddNoteToTrack(iNoteData.TrackIndex, aTapNote);
        }
        else
        {
            aTapNote = m_NoteManager.GetNote(iNoteData);
        }
        
        if (aTapNote.GetIsActive())
        {
            aTapNote.UpdateNote(m_NowTime);
        }
    }

    private void AddNoteToTrack(int iTrackIndex,TapNote iTapNote)
    {
        if (!m_TrackList.ContainsKey(iTrackIndex))
        {
            m_TrackList.Add(iTrackIndex,new Track());
        }
        m_TrackList[iTrackIndex].AddTapNote(iTapNote);
    }
}

