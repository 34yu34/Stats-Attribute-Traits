using System;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    public abstract class HandlersDrawer : PropertyDrawer
    {
        protected bool _showList;
        private RectDivider _rectDivider;

        protected abstract Array TraitEnumNames { get; }
        protected abstract SerializedProperty TraitListProperty { get; }
        protected abstract string TraitListName { get; }
        
        protected abstract int LinePerTrait { get; }
        
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            _rectDivider = new RectDivider(position, 1 + (_showList ? LinePerTrait * TraitEnumNames.Length : 0));
            _showList =  EditorGUI.Foldout(_rectDivider.GetNext(), _showList, TraitListName);
            

            if (!_showList) return;
            
            for (var i = 0; i < TraitEnumNames.Length; i++)
            {
                DrawTraitLine(i);
            }
        }
        
        private void DrawTraitLine(int index)
        {
            AssureArraySize(index);

            EditorGUI.PropertyField(
                _rectDivider.GetNext(LinePerTrait), 
                TraitListProperty.GetArrayElementAtIndex(index),
                new GUIContent((string) TraitEnumNames.GetValue(index))
            );
        }

        private void AssureArraySize(int index)
        {
            if (TraitListProperty.arraySize < index + 1)
            {
                TraitListProperty.InsertArrayElementAtIndex(index);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var lineHeight = base.GetPropertyHeight(property, label);
            
            var totalHeight = lineHeight;
            
            if (_showList)
            {
                totalHeight += lineHeight * TraitEnumNames.Length * LinePerTrait;
            }
            
            return totalHeight;
        }
    }
}