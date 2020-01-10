using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteJudgment : MonoBehaviour
{
    public enum Judgment
    {
        Perfect,
        Great,
        Good,
        Bad,
        Miss,
        None
    }

    private List<float> m_JudgeTimeList;
    private InputManager m_InputManager;

    private const float PERFECT_TIME_RANGE = 0.033f;
    private const float GREAT_TIME_RANGE   = 0.065f;
    private const float GOOD_TIME_RANGE    = 0.083f;
    private const float BAD_TIME_RANGE     = 0.1f;

    public void Initializer()
    {

    }
    public void JudgmentUpdate(float iNowTime, Dictionary<int, Track> iTrackList)
    {
        List<InputData> aTouchDataList = m_InputManager.TouchUpdate();
        TouchJudge(aTouchDataList,iNowTime, iTrackList);
        MissJudge(iNowTime, iTrackList);
    }

    private void Awake()
    {
        m_InputManager = InputManager.Instance;
        m_JudgeTimeList = new List<float>();
        m_JudgeTimeList.Add(PERFECT_TIME_RANGE);
        m_JudgeTimeList.Add(GREAT_TIME_RANGE);
        m_JudgeTimeList.Add(GOOD_TIME_RANGE);
        m_JudgeTimeList.Add(BAD_TIME_RANGE);
    }

    private void TouchJudge(List<InputData> iTouchDataList, float iNowTime, Dictionary<int, Track> iTrackList)
    {
        for (int index = 0;index < iTouchDataList.Count;index++)
        {
            if (iTouchDataList[index].TouchType == TouchTypes.TouchRelease)
            {
                TapNote aNote = null;
                Dictionary<int, Track>.KeyCollection aKeyColl = iTrackList.Keys;
                foreach (int TrackID in aKeyColl)
                {
                    aNote = iTrackList[TrackID].GetHoldNoteByTouchID(iTouchDataList[index].TouchID);
                }
                NoteJudge(iNowTime, aNote, iTouchDataList[index]);
            }
            else
            {
                int aTouchTrack = iTouchDataList[index].TouchTrackIndex;
                TapNote aTapeNote = null;
                if (iTrackList.ContainsKey(aTouchTrack))
                {
                    aTapeNote = iTrackList[aTouchTrack].GetClosestNote(iNowTime);
                }
                NoteJudge(iNowTime, aTapeNote, iTouchDataList[index]);
            }
        }
    }

    private void MissJudge(float iNowTime, Dictionary<int, Track> iTrackList)
    {
        Dictionary<int, Track>.KeyCollection aKeyColl = iTrackList.Keys;
        foreach (int index in aKeyColl)
        {
            List<TapNote> aNoteList = iTrackList[index].GetTrackNoteList();
            foreach (TapNote note in aNoteList)
            {
                if (note.GetIsActive())
                {
                    float aNoteTime = note.GetNoteTime();

                    if (note.GetNoteType() == NoteType.HoldNote)
                    {
                        HoldNote aHoldNote = note as HoldNote;
                        if (aHoldNote.GetHoldNoteState() == HoldNote.HoldNoteState.Tap)
                        {
                            aNoteTime = aHoldNote.GetNoteTime();
                        }
                        else if (aHoldNote.GetHoldNoteState() == HoldNote.HoldNoteState.Hold && aHoldNote.GetIsNeedRelease())
                        {
                            aNoteTime = aHoldNote.GetNoteEndTime();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    float aTime = iNowTime - aNoteTime;
                    if (aTime >= m_JudgeTimeList[(int)Judgment.Bad])
                    {
                        NoteResult(note, Judgment.Miss);
                    }
                }
            }
        }
    }

    private void NoteJudge(float iTime, TapNote iTapNote, InputData iInputData)
    {
        if (iTapNote == null)
        {
            return;
        }
        switch (iTapNote.GetNoteType())
        {
            case NoteType.TapNote:
                TapNoteJudge(iTime, iTapNote);
                break;
            case NoteType.HoldNote:
                HoldNoteJudge(iTime, iTapNote as HoldNote, iInputData);
                break;
        }
    }

    private void TapNoteJudge(float iNowTime, TapNote iTapNote)
    {
        Judgment aJudgment = TimeJudge(iNowTime, iTapNote.GetNoteTime());
        NoteResult(iTapNote, aJudgment);
    }

    private void SlideNoteJudge(float iNowTime, TapNote iTapNote, InputData iInputData)
    {
        if (iInputData.TouchType == TouchTypes.Slide)
        {
            Judgment aJudgment = TimeJudge(iNowTime, iTapNote.GetNoteTime());
            NoteResult(iTapNote, aJudgment);
        }
    }

    private void HoldNoteJudge(float iNowTime, HoldNote iHoldNote, InputData iInputData)
    {
        switch (iInputData.TouchType) {
            case TouchTypes.TouchDown:
                if (iHoldNote.GetHoldNoteState() == HoldNote.HoldNoteState.Tap)
                {
                    Judgment aJudgment = TimeJudge(iNowTime, iHoldNote.GetNoteTime());
                    NoteResult(iHoldNote, aJudgment);
                }
                break;
            case TouchTypes.TouchHold:
                if (iHoldNote.GetHoldNoteState() == HoldNote.HoldNoteState.Tap)
                {
                    if (iHoldNote.GetIsNeedTap() == false)
                    {
                        Judgment aJudgment = TimeJudge(iNowTime, iHoldNote.GetNoteTime());
                        if (aJudgment == Judgment.Perfect)
                        {
                            iHoldNote.ChangeStateByJudge(aJudgment);
                        }
                    }
                }
                break;
            case TouchTypes.TouchRelease:
                if (iHoldNote.GetHoldNoteState() == HoldNote.HoldNoteState.Hold)
                {
                    Judgment aJudgment = Judgment.Miss;
                    if (iHoldNote.GetNoteEndTrack() == iInputData.TouchTrackIndex)
                    {
                        aJudgment = TimeJudge(iNowTime, iHoldNote.GetNoteEndTime());
                        aJudgment = aJudgment == Judgment.None ? Judgment.Miss : aJudgment;
                    }
                    NoteResult(iHoldNote, aJudgment);
                }
                break;
        }
    }

    private Judgment TimeJudge(float iNowTime,float iTargetTime)
    {
        float aTime = Mathf.Abs(iNowTime - iTargetTime);
        Judgment aJudgment = Judgment.None;
        for (int index = 0; index < m_JudgeTimeList.Count; index++)
        {
            if (aTime <= m_JudgeTimeList[index])
            {
                aJudgment = (Judgment)index;
                break;
            }
        }
        return aJudgment;
    }

    private void NoteResult(TapNote iNote, Judgment iJudgment)
    {
        if (iJudgment == Judgment.None)
        {
            return;
        }
        print(iJudgment);
        iNote.ChangeStateByJudge(iJudgment);
    }
}
