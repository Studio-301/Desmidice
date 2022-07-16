using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad]
public static class Toolbar
{
    static Toolbar()
    {
        ToolbarExtender.RightToolbarGUI.Add(DrawRightGUI);
    }

    static void DrawRightGUI()
    {
        var deadline = new DateTime(2022, 7, 17, 19, 0, 0, 0);
        var till = deadline.Subtract(DateTime.Now);
        var style = new GUIStyle(EditorStyles.boldLabel);
        style.normal.textColor = Color.red * .9f;
        GUILayout.Label($"{till.Hours + till.Days * 24}:{till.Minutes}:{till.Seconds}", style);
    }
}