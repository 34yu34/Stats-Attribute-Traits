using System;
using Stats;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    [CustomPropertyDrawer(typeof(StatsHandler))]
    public class StatsHandlerDrawer : HandlersDrawer
    {
        protected override Array TraitEnumNames => Enum.GetNames(typeof(Stat));

        private SerializedProperty _statListProperty;
        protected override SerializedProperty TraitListProperty => _statListProperty;
        protected override string TraitListName => "Stats";
        protected override int LinePerTrait => 1;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _statListProperty = property.FindPropertyRelative("_stats");
            base.OnGUI(position, property, label);
        }
    }
}