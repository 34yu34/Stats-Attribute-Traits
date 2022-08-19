using Stats;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    [CustomPropertyDrawer(typeof(ResourceInstance))]
    public class ResourceInstanceDrawer : PropertyDrawer
    {
        public static int NumberOfRow => 2;
        public static float LabelWidth => 80f;
        
        private float _rowHeight;
        
        private SerializedProperty _maxValue;
        private SerializedProperty _statFloatProperty;
        private SerializedProperty _initialValue;
        private SerializedProperty _currentValue;
        
        private Rect _firstLineRect;
        private Rect _secondLineLabelRect;
        private Rect _secondLineSliderRect;

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            SetupProperties(property);
            
            SetupRect(position);

            DrawGUI(label);
        }
        
        private void SetupProperties(SerializedProperty property)
        {
            _maxValue = property.FindPropertyRelative("_maxValue");
            _statFloatProperty = _maxValue.FindPropertyRelative("_baseValue");
            _initialValue = property.FindPropertyRelative("_initialValue");
            _currentValue = property.FindPropertyRelative("_currentValue");
        }
        
        private void SetupRect(Rect position)
        {
            _rowHeight = position.height / NumberOfRow;

            _firstLineRect = new Rect(position.x, position.y, position.width, _rowHeight);
            _secondLineLabelRect = new Rect(position.x, position.y + _rowHeight, LabelWidth, _rowHeight);
            _secondLineSliderRect = new Rect(position.x + LabelWidth, position.y + _rowHeight, position.width - LabelWidth,
                _rowHeight);
        }

        private void DrawGUI(GUIContent label)
        {
            EditorGUI.PropertyField(_firstLineRect, _maxValue, label);
            EditorGUI.LabelField(_secondLineLabelRect, "Initial value");
            _initialValue.floatValue =
                EditorGUI.Slider(_secondLineSliderRect, _initialValue.floatValue, 0f, _statFloatProperty.floatValue);

            _currentValue.floatValue = _initialValue.floatValue;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * NumberOfRow;
        }
    }
}