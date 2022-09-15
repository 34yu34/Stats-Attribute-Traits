using System;
using System.IO;
using System.Linq;
using Stats;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    public class TraitsDatabaseHandler
    {
        public static string[][] BasicEnums => new[]
        {
            new[] {"Strength", "Wisdom", "Luck"},
            new[] {"Health", "Stamina", "Exp"}
        };
        public static string[] Names => new [] {"Stat", "Resource"};

        private const string Extension = ".cs";

        private static string FileName(TraitsTypes type) => Names[(int)type] + Extension;
        private string CompleteFilePath(TraitsTypes type) => GetPath(type) + FileName(type);
        
        private string[] _paths;

        private string GetPath(TraitsTypes trait)
        {
            FindPath(trait);

            return _paths[(int)trait];
        }

        public TraitsDatabaseHandler()
        {
            _paths = new string[2];

            FindPath(TraitsTypes.Stat);
            FindPath(TraitsTypes.Resource);
        }

        private void FindPath(TraitsTypes trait)
        {
            var paths = AssetDatabase.FindAssets($"{GetName(trait)} t:MonoScript").Select(AssetDatabase.GUIDToAssetPath);

            foreach (var path in paths)
            {
                var name = Path.GetFileName(path);
                if (name == FileName(trait))
                {
                    _paths[(int)trait] = path;
                }
            }
        }

        private void CreateEnumFile(TraitsTypes traitsType, string[] enumNames)
        {
            var type = (int)traitsType;
            var path = GetPath(traitsType);
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            using (StreamWriter file = File.CreateText(CompleteFilePath(traitsType)))
            {
                file.WriteLine($"\n// This file should be updated with all the {GetName(traitsType)}s one wants.");
                file.WriteLine("// No value should be given to the enum as default values are necessary.\n");
                
                file.WriteLine("namespace Stats\n{");
                file.WriteLine("\tpublic enum " + GetName(traitsType) + " \n\t{");
 
                var i = 0;
                foreach (var line in enumNames)
                {
                    var lineRep = line.Replace(" ", string.Empty);
                    if (string.IsNullOrEmpty(lineRep)) continue;
                    
                    file.WriteLine($"\t\t{lineRep},");
                    i++;
                }
 
                file.WriteLine("\t}");
                file.WriteLine("}");
            }
 
            AssetDatabase.ImportAsset(CompleteFilePath(traitsType));
        }

        public void GenerateBlankFile(TraitsTypes type)
        {
            CreateEnumFile(type, BasicEnums[(int)type]);
        }

        public string ShowPath(TraitsTypes type)
        {
            return GetPath(type);
        }

        public string GetName(TraitsTypes trait)
        {
            return Names[(int)trait];
        }
    }
}