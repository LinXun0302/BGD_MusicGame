using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private GameObject mTapPointPrefab = null;
    private Dictionary<int, TapNote> m_NoteList;

    public TapNote GetNote(NoteData iNoteData)
    {
        TapNote aTapNote;
        if (CheckNoteIsSpawn(iNoteData.NoteID))
        {
            aTapNote = GetNoteInDictionary(iNoteData.NoteID);
        }
        else
        {
            switch (iNoteData.NoteType)
            {
                case NoteType.HoldNote:
                    aTapNote = CreateTapNoteAndInitialize<HoldNote>(iNoteData);
                    break;
                default:
                    aTapNote = CreateTapNoteAndInitialize<TapNote>(iNoteData);
                    break;
            }
        }
        return aTapNote;
    }

    public bool CheckNoteIsSpawn(int iNoteID)
    {
        bool aIsSpawn = false;
        aIsSpawn = m_NoteList.ContainsKey(iNoteID);
        return aIsSpawn;
    }

    private void Awake()
    {
        mTapPointPrefab = Resources.Load<GameObject>("Prefabs/TapPoint");
        m_NoteList = new Dictionary<int, TapNote>();
    }

    private T CreateTapNoteAndInitialize<T>(NoteData iNoteData) where T : TapNote
    {
        T aNote = null;
        GameObject aNoteObject = Instantiate(mTapPointPrefab);
        aNote = aNoteObject.AddComponent<T>();
        aNote.Initialize(iNoteData);
        m_NoteList.Add(iNoteData.NoteID, aNote);
        return aNote;
    }

    private TapNote GetNoteInDictionary(int iNoteID)
    {
        TapNote aTapNote = null;
        aTapNote = m_NoteList[iNoteID];
        return aTapNote;
    }
}
