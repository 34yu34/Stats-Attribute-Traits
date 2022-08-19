using System;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    public abstract class HandlersDrawer : PropertyDrawer
    {

        protected Rect _baseRect;
        protected float _rowHeight;

        protected bool _showList;
        
        protected abstract Array TraitEnumNames { get; }
        protected abstract SerializedProperty TraitListProperty { get; }
        protected abstract string TraitListName { get; }
        
        protected abstract int LinePerTrait { get; }
        
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            _baseRect = position;
            _rowHeight = base.GetPropertyHeight(property, label);
            
            _showList =  EditorGUI.Foldout(GetFirstRect(), _showList, TraitListName);

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
                GetRectForLine(index), 
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

        private Rect GetFirstRect()
        {
            return new Rect(
                new Vector2(_baseRect.x, _baseRect.y),
                new Vector2(_baseRect.width, _rowHeight)
            );
        }

        private Rect GetRectForLine(int index)
        {
            return new Rect(
                new Vector2(_baseRect.x, _baseRect.y + _rowHeight + _rowHeight * index * LinePerTrait),
                new Vector2(_baseRect.width, _rowHeight * LinePerTrait)
            );
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