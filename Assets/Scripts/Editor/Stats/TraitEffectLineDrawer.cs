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

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            _traitsTypeProperty = property.FindPropertyRelative("_traitsType");
            _statProperty = property.FindPropertyRelative("_stat");
            _resourceProperty = property.FindPropertyRelative("_resource");
            _modificationValueProperty = property.FindPropertyRelative("_modificationValue");
            _modificationTypeProperty = property.FindPropertyRelative("_modificationType");

            var rectDivider = new RectDivider(position, 4);
            
            EditorGUI.PropertyField(rectDivider.GetNext(), _traitsTypeProperty);

            EditorGUI.PropertyField(rectDivider.GetNext(), IsResource() ? _resourceProperty : _statProperty);

            EditorGUI.PropertyField(rectDivider.GetNext(), _modificationValueProperty);

            EditorGUI.PropertyField(rectDivider.GetNext(), _modificationTypeProperty);


        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            _height = base.GetPropertyHeight(property, label);
            
            return _height * 4;
        }

        private bool IsResource()
        {
            return  _traitsTypeProperty.enumValueIndex == 1;
        }
    }
}