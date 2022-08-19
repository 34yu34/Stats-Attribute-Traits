using System;
using Stats;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    [CustomEditor(typeof(Traits))]
    public class TraitsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_stats"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_resources"));

            if (GUILayout.Button("Open Stat Window"))
            {
                StatWindow.ShowWindow();
            }
        }
    }
}