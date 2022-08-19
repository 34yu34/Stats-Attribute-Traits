using Stats;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    [CustomPropertyDrawer(typeof(StatInstance))]
    public class StatInstanceDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            var baseValue = property.FindPropertyRelative("_baseValue");
            var calculatedValue = property.FindPropertyRelative("_calculatedValue");
            
            EditorGUI.PropertyField(position, baseValue, label);

            calculatedValue.floatValue = baseValue.floatValue;
        }
    }
}