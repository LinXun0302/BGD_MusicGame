using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMap
{
    private List<NoteData> m_NoteDataList;

    public List<NoteData> GetNoteDataList()
    {
        return m_NoteDataList;
    }

    public NoteMap()
    {
        m_NoteDataList = new List<NoteData>();
        //For Test
        m_NoteDataList.Add(CreateNoteData(NoteType.TapNote, 1, 0, 2.0f));
        m_NoteDataList.Add(CreateNoteData(NoteType.TapNote, 2, 0, 2.5f));
        m_NoteDataList.Add(CreateNoteData(NoteType.TapNote, 3, 3, 5.0f));
        m_NoteDataList.Add(CreateNoteData(NoteType.TapNote, 5, 4, 5.5f));
        m_NoteDataList.Add(CreateNoteData(NoteType.TapNote, 6, 0, 6.0f));
        m_NoteDataList.Add(CreateHoldNoteData(NoteType.HoldNote, 8, 4, 8.30f, 6, 8.80f, true, false));
        m_NoteDataList.Add(CreateHoldNoteData(NoteType.HoldNote, 9, 6, 8.80f, 4, 9.30f, false, true));
        m_NoteDataList.Add(CreateHoldNoteData(NoteType.HoldNote, 12, 3, 9.8f, 3, 10.8f, true, true));
    }

    private NoteData CreateHoldNoteData(NoteType iNoteType,int iNoteID, int iTrackIndex,float iNoteTime,int iEndTrack, float iHoldEndTime,bool iNeedTap, bool iNeedRelease)
    {
        NoteData aNoteData = CreateNoteData(iNoteType, iNoteID, iTrackIndex, iNoteTime);

        aNoteData.HoldEndTime       = iHoldEndTime;
        aNoteData.HoldEndTrackIndex = iEndTrack;
        aNoteData.IsNeedTap         = iNeedTap;
        aNoteData.IsNeedRelease     = iNeedRelease;

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
