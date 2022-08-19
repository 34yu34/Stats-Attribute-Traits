using System;
using Stats;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    [CustomEditor(typeof(Traits))]
    public class TraitsEditor : UnityEditor.Editor
    {
        private SerializedProperty _statHandlerProperty;
        private SerializedProperty _resourceHandlerProperty;
        
        private void OnEnable()
        {
            _statHandlerProperty = serializedObject.FindProperty("_statsHandler");
            _resourceHandlerProperty = serializedObject.FindProperty("_resourcesHandler");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(_statHandlerProperty);
            EditorGUILayout.PropertyField(_resourceHandlerProperty);

            if (GUILayout.Button("Open Stat Window"))
            {
                StatWindow.ShowWindow();
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}