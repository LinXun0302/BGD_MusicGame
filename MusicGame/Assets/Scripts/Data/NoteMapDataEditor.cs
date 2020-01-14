using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NoteMapData))]
public class NoteMapDataEditor : Editor
{
    public bool IsNeedTap     = true;
    public bool IsNeedRelease = true;

    public string TrackIndexString        = "0";
    public string NoteTimeString          = "0";
    public string HoldEndTrackIndexString = "0";
    public string HoldEndTimeString       = "0";
    public bool   IsHoldNote = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NoteMapData mNoteMapData = (NoteMapData)target;

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("以下為新增輸入用", MessageType.Info);
        TrackIndexString = EditorGUILayout.TextField("TrackIndex", TrackIndexString);
        NoteTimeString   = EditorGUILayout.TextField("NoteTime"  , NoteTimeString);

        IsHoldNote = EditorGUILayout.BeginToggleGroup("IsHoldNote", IsHoldNote);
        HoldEndTrackIndexString = EditorGUILayout.TextField("HoldEndTrackIndex", HoldEndTrackIndexString);
        HoldEndTimeString       = EditorGUILayout.TextField("HoldEndTime"      , HoldEndTimeString);
        IsNeedTap               = EditorGUILayout.Toggle("NeedTap"         , IsNeedTap);
        IsNeedRelease           = EditorGUILayout.Toggle("NeedRelease"     , IsNeedRelease);
        EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("Add Note"))
        {
            NoteData aNoteData          = new NoteData();
            aNoteData.NoteType          = IsHoldNote ? NoteType.HoldNote : NoteType.TapNote;
            aNoteData.TrackIndex        = int.Parse(TrackIndexString);
            aNoteData.NoteTime          = float.Parse(NoteTimeString);
            aNoteData.HoldEndTrackIndex = int.Parse(HoldEndTrackIndexString);
            aNoteData.HoldEndTime       = float.Parse(HoldEndTimeString);
            aNoteData.IsNeedTap         = IsNeedTap;
            aNoteData.IsNeedRelease     = IsNeedRelease;
            mNoteMapData.AddNoteData(aNoteData);
        }

    }
}
