using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new NoteMapData", menuName = "CreateData/Create NoteMapData", order = 1)]
[System.Serializable]
public class NoteMapData : ScriptableObject
{
    public int ID;
    public List<NoteData> m_NoteDataList;

    public void AddNoteData(NoteData iNoteData)
    {
        iNoteData.NoteID = m_NoteDataList.Count;
        m_NoteDataList.Add(iNoteData);
    }
}
