using System;
using Stats;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    [CustomPropertyDrawer(typeof(ResourcesHandler))]
    public class ResourcesHandlerDrawer : HandlersDrawer
    {
        protected override Array TraitEnumNames => Enum.GetNames(typeof(Resource));
        private SerializedProperty _resourcesProperty;
        protected override SerializedProperty TraitListProperty => _resourcesProperty;
        protected override string TraitListName => "Resources";
        protected override int LinePerTrait => 2;

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            _resourcesProperty = property.FindPropertyRelative("_resources");
            base.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
}