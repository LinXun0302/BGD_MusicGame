using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public void ShowJudge(NoteJudgment.Judgment iJudgment)
    {
        mJudgeText.text = iJudgment.ToString();
    }
    public void ShowCombo(int iCombo)
    {
        mComboText.text = iCombo.ToString();
    }
    private void Awake()
    {
        mCombo = 0;
    }
    [SerializeField]
    private Text mJudgeText = null;
    [SerializeField]
    private Text mComboText = null;
    private int mCombo = 0;
}
