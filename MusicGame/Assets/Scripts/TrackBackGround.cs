using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBackGround : MonoBehaviour
{
    [SerializeField]
    private GameObject m_JudgeLine = null;
    [SerializeField]
    private List<GameObject> m_TrackPointList;

    private float m_TrackLength;

    public float GetTrackLength()
    {
        return m_TrackLength;
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
    private void Awake()
    {
        m_TrackLength = 150.0f;
    }
}
