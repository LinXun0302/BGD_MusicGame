using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMapManager
{
 //-----------------------------------------------
 //Public
 //-----------------------------------------------
    public List<NoteData> GetNoteDataList(int iSongIndex)
    {
        return m_NoteMapData[iSongIndex].m_NoteDataList;
    }

    public NoteMapManager()
    {
        NoteMapData[] aNoteMapDatas = Resources.LoadAll<NoteMapData>(RESOURCES_NOTE_MAP_DATA_PATH);
        for (int index = 0; index < aNoteMapDatas.Length; index++)
        {
            m_NoteMapData.Add(aNoteMapDatas[index].ID, aNoteMapDatas[index]);
        }
    }
//-----------------------------------------------
//Variables
//-----------------------------------------------
    private List<NoteData> m_NoteDataList;
    private Dictionary<int,NoteMapData> m_NoteMapData = new Dictionary<int, NoteMapData>();
    private const string RESOURCES_NOTE_MAP_DATA_PATH = "MapData/";
}
