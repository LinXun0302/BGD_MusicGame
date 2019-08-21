using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMap
{
    private List<NoteData> m_TapNoteDataList;

    public List<NoteData> GetNoteDataList()
    {
        return m_TapNoteDataList;
    }

    public NoteMap()
    {
        m_TapNoteDataList = new List<NoteData>();
        //For Test
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 1, 1, 2.0f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 2, 1, 2.0375f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 3, 3, 5.0f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 5, 4, 5.5f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 6, 0, 6.0f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 7, 6, 8.15f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.HoldNote, 8, 4, 8.30f, 6, 8.80f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.HoldNote, 9, 6, 8.80f, 4, 9.30f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 10, 0, 9.0f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 11, 4, 9.3f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.HoldNote, 12, 3, 9.8f, 3, 10.8f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 13, 1, 10.5f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 14, 2, 10.6f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 15, 2, 11.0f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 16, 5, 12.0f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 17, 4, 13.0f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 18, 6, 14.0f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 19, 5, 14.35f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 20, 6, 14.88f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 21, 4, 15.21f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.HoldNote, 22, 6, 16.21f, 0, 17.21f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 23, 6, 17.21f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.HoldNote, 24, 0, 17.5f, 6, 18.5f));
        m_TapNoteDataList.Add(CreateNoteData(NoteType.TapNote, 25, 0, 18.5f));
    }

    private NoteData CreateNoteData(NoteType iNoteType,int iNoteID, int iTrackIndex,float iNoteTime,int iEndTrack, float iHoldEndTime)
    {
        NoteData aNoteData = CreateNoteData(iNoteType, iNoteID, iTrackIndex, iNoteTime);

        aNoteData.HoldEndTime = iHoldEndTime;
        aNoteData.HoldEndTrackIndex = iEndTrack;

        return aNoteData;
    }
    private NoteData CreateNoteData(NoteType iNoteType, int iNoteID, int iTrackIndex, float iNoteTime)
    {
        NoteData aNoteData = new NoteData();
        aNoteData.NoteType = iNoteType;
        aNoteData.NoteID = iNoteID;
        aNoteData.TrackIndex = iTrackIndex;
        aNoteData.NoteTime = iNoteTime;

        return aNoteData;
    }
}
