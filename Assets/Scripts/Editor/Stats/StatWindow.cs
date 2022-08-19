using System;
using System.Collections.Generic;
using Stats;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor.Stats
{
    public class StatWindow : EditorWindow
    {
        private string[] _newStatsNames = {};


        [MenuItem("Window/Stats/Stats and Resources Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<StatWindow>();
            window.titleContent = new GUIContent("Stats and Resources");
            window.Show();
        }

        private void Awake()
        {
            _newStatsNames = Enum.GetNames(typeof(Stat));
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            DrawEnumList(Enum.GetNames(typeof(Stat)), "Stat");
            DrawEnumList(Enum.GetNames(typeof(Resource)), "Resource");
            EditorGUILayout.EndHorizontal();

            OpenCodeFileButtons();
            GenerateDefaultFilesButton();
            
            EditorGUILayout.EndVertical();

        }

        private static void OpenCodeFileButtons()
        {
            EditorGUILayout.BeginHorizontal();
            
            OpenFileButtonFor(TraitsTypes.Stat);
            OpenFileButtonFor(TraitsTypes.Resource);
            
            EditorGUILayout.EndHorizontal();
        }
        
        private static void GenerateDefaultFilesButton()
        {
            if (!GUILayout.Button("Generate Default Files")) return;
            
            StatEnumWriter.GenerateBlankFile(TraitsTypes.Stat);
            StatEnumWriter.GenerateBlankFile(TraitsTypes.Resource);
        }
        

        private static void OpenFileButtonFor(TraitsTypes types)
        {
            if (!GUILayout.Button($"Open {StatEnumWriter.ToName(types)} file")) return;

            OpenFile(types);
        }

        private static void OpenFile(TraitsTypes types)
        {
            var asset = LoadFile(types);
            if (!asset)
            {
                asset = GenerateBlankAsset(types);
            }

            AssetDatabase.OpenAsset(asset);
        }

        private static Object LoadFile(TraitsTypes types)
        {
            return AssetDatabase.LoadAssetAtPath(StatEnumWriter.GetPath(types), typeof(MonoScript));
        }

        private static Object GenerateBlankAsset(TraitsTypes types)
        {
            StatEnumWriter.GenerateBlankFile(types);
            var asset = AssetDatabase.LoadAssetAtPath(StatEnumWriter.GetPath(types), typeof(MonoScript));
            return asset;
        }

        private static void DrawEnumList(IReadOnlyList<string> enumNames, string listName)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField($"Current {listName}s", EditorStyles.largeLabel);

            for (int i = 0; i < enumNames.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                GUILayout.Label(enumNames[i]);
                
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }
    }
}