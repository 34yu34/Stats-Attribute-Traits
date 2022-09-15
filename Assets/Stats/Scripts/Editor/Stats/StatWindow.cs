using System;
using System.Collections.Generic;
using Stats;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor.Stats
{
    public class StatWindow : EditorWindow
    {
        private TraitsDatabaseHandler _writer;


        [MenuItem("Window/Stats/Stats and Resources Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<StatWindow>();
            window.titleContent = new GUIContent("Stats and Resources");
            window.Show();
        }

        private void Awake()
        {
            _writer = new TraitsDatabaseHandler();
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

        private void OpenCodeFileButtons()
        {
            EditorGUILayout.BeginHorizontal();
            
            OpenFileButtonFor(TraitsTypes.Stat);
            OpenFileButtonFor(TraitsTypes.Resource);
            
            EditorGUILayout.EndHorizontal();
        }
        
        private void GenerateDefaultFilesButton()
        {
            if (!GUILayout.Button("Generate Default Files")) return;
            
            _writer.GenerateBlankFile(TraitsTypes.Stat);
            _writer.GenerateBlankFile(TraitsTypes.Resource);
        }
        

        private void OpenFileButtonFor(TraitsTypes trait)
        {
            if (!GUILayout.Button($"Open {_writer.GetName(trait)} file")) return;

            OpenFile(trait);
        }

        private  void OpenFile(TraitsTypes types)
        {
            var asset = LoadFile(types);
            if (!asset)
            {
                asset = GenerateBlankAsset(types);
            }

            AssetDatabase.OpenAsset(asset);
        }

        private Object LoadFile(TraitsTypes type)
        {
            return AssetDatabase.LoadAssetAtPath(_writer.ShowPath(type), typeof(MonoScript));
        }

        private Object GenerateBlankAsset(TraitsTypes trait)
        {
            _writer.GenerateBlankFile(trait);
            var asset = AssetDatabase.LoadAssetAtPath(_writer.ShowPath(trait), typeof(MonoScript));
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