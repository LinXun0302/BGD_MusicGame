using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBackGround : MonoBehaviour
{
//-----------------------------------------------
//Public
//-----------------------------------------------
    public float GetTrackLength()
    {
        return TRACK_LENGTH;
    }

    public Vector3 GetJudgeLinePosition()
    {
        return m_JudgeLine.transform.position;
    }

    public List<GameObject> GetTrackPointList()
    {
        return m_TrackPointList;
    }

    public Vector3 GetTrackPointPosition(int iTrackIndex)
    {
        Vector3 aPosition = Vector3.zero;
        if (iTrackIndex > m_TrackPointList.Count -1)
        {
            Debug.Log("Out of Track Range");
        }
        else
        {
            aPosition = m_TrackPointList[iTrackIndex].transform.position;
        }
        return aPosition;
    }

//-----------------------------------------------
//Variables
//-----------------------------------------------
    [SerializeField]
    private GameObject m_JudgeLine = null;
    [SerializeField]
    private List<GameObject> m_TrackPointList = new List<GameObject>();

//-----------------------------------------------
//Const
//-----------------------------------------------
    private const float TRACK_LENGTH = 150.0f;

}
