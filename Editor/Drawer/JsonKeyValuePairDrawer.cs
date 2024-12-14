using UnityEditor;
using UnityEngine;

namespace JHS.Library.JsonEditor.Editor.Drawer
{
    [CustomPropertyDrawer(typeof(JsonKeyValuePair))]
    public class JsonKeyValuePairDrawer : PropertyDrawer
    {
        SerializedProperty keyProperty;
        SerializedProperty valueProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            keyProperty = property.FindPropertyRelative("key").FindPropertyRelative("key");
            valueProperty = property.FindPropertyRelative("value");

            var keyRect = new Rect(position.x, position.y, 150, EditorGUIUtility.singleLineHeight);
            var valueRect = new Rect(keyRect.xMax + 4, position.y, position.width - 154, EditorGUI.GetPropertyHeight(valueProperty));

            EditorGUI.PropertyField(keyRect, keyProperty, GUIContent.none);
            EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none);
        }
    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            valueProperty = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }
    }
}