using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tools.GUIEditor
{
    public class GUITools
    {
        static GUIStyle logStyle;
        public static void Label(string text, Color color = default, int size = 12, FontStyle style = FontStyle.Normal, params GUILayoutOption[] options)
        {
            if (logStyle == null)
                logStyle = new GUIStyle(EditorStyles.label);

            if (color == default)
                color = Color.white;

            logStyle.normal.textColor = color;
            logStyle.fontStyle = style;
            logStyle.fontSize = size;
            logStyle.hover = logStyle.normal;

            GUILayout.Label(text, logStyle, options);
        }

        public static void DrawLine(Color color = default, int thickness = 2, int padding = 10)
        {
            if (color == default)
                color = Color.gray;

            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        static GUIStyle titleStyle;
        public static void DrawTitle(string text, Color color = default, bool padding = true, bool underline = true)
        {
            if (titleStyle == null)
            {
                titleStyle = new GUIStyle(EditorStyles.label);
                titleStyle.fontSize = 14;
                titleStyle.fontStyle = FontStyle.Bold;
            }

            if (color == default)
                color = Color.blue + Color.white * 0.5f;

            titleStyle.normal.textColor = color;

            GUILayout.Label(text, titleStyle);
            if (underline) 
                GUITools.DrawLine(color, thickness: 1, padding: padding ? 4 : 0);
        }
    }
}