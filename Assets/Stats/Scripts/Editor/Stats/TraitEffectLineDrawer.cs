using System;
using System.Collections.Generic;
using Stats;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    [CustomPropertyDrawer(typeof(TraitEffectLine))]
    public class TraitEffectLineDrawer : PropertyDrawer
    {
        private SerializedProperty _traitsTypeProperty;
        private SerializedProperty _statProperty;
        private SerializedProperty _resourceProperty;
        private SerializedProperty _modificationValueProperty;
        private SerializedProperty _modificationTypeProperty;
        private float _height;

        private const float BoxSize = 50.0f;

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            _traitsTypeProperty = property.FindPropertyRelative("_traitsType");
            _statProperty = property.FindPropertyRelative("_stat");
            _resourceProperty = property.FindPropertyRelative("_resource");
            _modificationValueProperty = property.FindPropertyRelative("_modificationValue");
            _modificationTypeProperty = property.FindPropertyRelative("_modificationType");

            var rectDivider = new RectDivider(position, 3);
            
            EditorGUI.PropertyField(rectDivider.GetNext(), _traitsTypeProperty);

            EditorGUI.PropertyField(rectDivider.GetNext(), IsResource() ? _resourceProperty : _statProperty);


            var lastRect = rectDivider.GetNext();

            var halfSize = lastRect.width - BoxSize;
            
            var firstHalf = new Rect(lastRect.x, lastRect.y,halfSize, lastRect.height);
            var lastHalf = new Rect(lastRect.x + halfSize, lastRect.y, lastRect.width - halfSize, lastRect.height);
            
            EditorGUI.PropertyField(firstHalf, _modificationValueProperty);
            _modificationTypeProperty.intValue = EditorGUI.Popup(lastHalf, _modificationTypeProperty.intValue, GetAllText());
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            _height = base.GetPropertyHeight(property, label);
            
            return _height * 4;
        }

        private bool IsResource()
        {
            return  _traitsTypeProperty.enumValueIndex == (int)TraitsTypes.Resource;
        }

        private string ToText(TraitModificationType type)
        {
            switch (type)
            {
                case TraitModificationType.Flat:
                    return "abs";
                case TraitModificationType.Relative:
                    return "%";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private TraitModificationType FromText(string text)
        {
            foreach (var value in Enum.GetValues(typeof(TraitModificationType)))
            {
                if (ToText((TraitModificationType)value) == text)
                {
                    return (TraitModificationType)value;
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        private string[] GetAllText()
        {
            var list =  new List<string>();
            foreach (var value in Enum.GetValues(typeof(TraitModificationType)))
            {
                list.Add(ToText((TraitModificationType)value));
            }

            return list.ToArray();
        }
    }
}