using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        SelectSong,
        PlayingSong,
        Result,
        None
    }

    public void NoteJudge(NoteJudgment.Judgment iJudgment)
    {
        if (iJudgment == NoteJudgment.Judgment.Good || iJudgment == NoteJudgment.Judgment.Bad || iJudgment == NoteJudgment.Judgment.Miss)
        {
            mCombo = 0;
        }
        else
        {
            mCombo++;
        }
        mGameUI.ShowCombo(mCombo);
        mGameUI.ShowJudge(iJudgment);
    }

    private void Start()
    {
        mGameState = GameState.PlayingSong;
        mTrackController.StartPlaySong(0);
        mCombo = 0;
    }

    private void Update()
    {
        switch (mGameState)
        {
            case GameState.PlayingSong:
                mTrackController.TrackControllerUpdate();
                break;
        }
    }

    private GameState mGameState = GameState.None;
    [SerializeField]
    private TrackController mTrackController = null;
    [SerializeField]
    private GameUI mGameUI = null;
    private int    mCombo  = 0;
}
