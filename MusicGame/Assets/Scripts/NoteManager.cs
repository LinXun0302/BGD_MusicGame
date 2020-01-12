using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : Singleton<NoteManager>
{
//-----------------------------------------------
//Public
//-----------------------------------------------
    public Note GetNote(NoteData iNoteData)
    {
        Note aNote;
        if (CheckNoteIsSpawn(iNoteData.NoteID))
        {
            aNote = GetNoteInDictionary(iNoteData.NoteID);
        }
        else
        {
            switch (iNoteData.NoteType)
            {
                case NoteType.HoldNote:
                    aNote = GetNote<HoldNote>();
                    aNote.Initialize(iNoteData);
                    m_UseNoteList.Add(iNoteData.NoteID, aNote);
                    break;
                default:
                    aNote = GetNote<TapNote>();
                    aNote.Initialize(iNoteData);
                    m_UseNoteList.Add(iNoteData.NoteID, aNote);
                    break;
            }
        }
        return aNote;
    }

    public bool CheckNoteIsSpawn(int iNoteID)
    {
        bool aIsSpawn = false;
        aIsSpawn = m_UseNoteList.ContainsKey(iNoteID);
        return aIsSpawn;
    }

    public void RecycleNote(Note iNote)
    {
        m_UnuseNoteList.Add(iNote);
        iNote.gameObject.SetActive(false);
    }

    public override void Awake()
    {
        base.Awake();
        mTapPointPrefab = Resources.Load<GameObject>("Prefabs/TapPoint");
        m_UseNoteList = new Dictionary<int, Note>();
        m_UnuseNoteList = new List<Note>();
        CreateNoteToUnusePool<TapNote>(0);
        CreateNoteToUnusePool<HoldNote>(0);
    }
//-----------------------------------------------
//private
//-----------------------------------------------
#region
    private void CreateNoteToUnusePool<T>(int iCount) where T : Note
    {
        for (int aIndex = 0; aIndex < iCount; aIndex++)
        {
            T aNote = CreateNote<T>();
            m_UnuseNoteList.Add(aNote);
        }
    }

    private T GetNote<T>() where T : Note
    {
        T aNote = null;
        for (int aIndex = 0; aIndex < m_UnuseNoteList.Count; aIndex++)
        {
            aNote = m_UnuseNoteList[aIndex] as T;
            if (aNote != null)
            {
                m_UnuseNoteList.Remove(aNote);
                break;
            }
        }
        if (aNote == null)
        {
            aNote = CreateNote<T>();
        }
        aNote.gameObject.SetActive(true);
        return aNote;
    }

    private T CreateNote<T>() where T : Note
    {
        T aNote = null;
        GameObject aNoteObject = Instantiate(mTapPointPrefab);
        aNote = aNoteObject.AddComponent<T>();
        aNote.gameObject.name = typeof(T).ToString();
        aNote.transform.SetParent(this.transform);
        aNote.gameObject.SetActive(false);
        return aNote;
    }

    private Note GetNoteInDictionary(int iNoteID)
    {
        Note aNote = null;
        aNote = m_UseNoteList[iNoteID];
        return aNote;
    }
    #endregion
//-----------------------------------------------
//Variables
//-----------------------------------------------
    private GameObject mTapPointPrefab = null;
    private Dictionary<int, Note> m_UseNoteList;
    private List<Note> m_UnuseNoteList;
}
