using System;
using System.Collections.Generic;
using System.IO;
using Codice.Client.BaseCommands;
using Stats;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    public class StatEnumWriter
    {
        public const string EXTENSION = ".cs";
        public const string PATH = "Assets/Resources/";
        public const string STAT_FILE_NAME = "Stat";
        public const string RESOURCE_FILE_NAME = "Resource";

        public static string[] BasicStats => new[] {"Strength", "Wisdom", "Luck"};
        public static string[] BasicResources => new[] {"Health", "Stamina", "Exp"};

        public static string FullStatPath => PATH + STAT_FILE_NAME + EXTENSION;
        public static string FullResourcePath => PATH + RESOURCE_FILE_NAME + EXTENSION;
        
        public static void WriteEnumFile(List<string> names, string name)
        {
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            
            using (StreamWriter file = File.CreateText(PATH + name + EXTENSION))
            {
                file.WriteLine($"\n// This file should be updated with all the {name}s one wants.");
                file.WriteLine("// No value should be given to the enum as default values are necessary.\n");
                
                file.WriteLine("namespace Stats\n{");
                file.WriteLine("\tpublic enum " + name + " \n\t{");
 
                var i = 0;
                foreach (var line in names)
                {
                    var lineRep = line.Replace(" ", string.Empty);
                    if (string.IsNullOrEmpty(lineRep)) continue;
                    
                    file.WriteLine($"\t\t{lineRep},");
                    i++;
                }
 
                file.WriteLine("\t}");
                file.WriteLine("}");
            }
 
            AssetDatabase.ImportAsset(PATH + name + EXTENSION);
        }

        public static void GenerateBlankFile(TraitsTypes type)
        {
            WriteEnumFile(new List<string>(GetBasicEnumValue(type)), ToName(type));
        }

        public static string ToName(TraitsTypes toName)
        {
            switch (toName)
            {
                case TraitsTypes.Resource:
                    return "Resource";
                case TraitsTypes.Stat:
                default:
                    return "Stat";
            }
        }

        public static string[] GetBasicEnumValue(TraitsTypes type)
        {
            switch (type)
            {
                case TraitsTypes.Resource:
                    return BasicResources;
                case TraitsTypes.Stat:
                default:
                    return BasicStats;
            }
        }

        public static string GetPath(TraitsTypes type)
        {
            switch (type)
            {
                case TraitsTypes.Resource:
                    return FullResourcePath;
                case TraitsTypes.Stat:
                default:
                    return FullStatPath;
            }
        }
    }
}