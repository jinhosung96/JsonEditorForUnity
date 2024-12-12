using System;
using Mine.Code.App.JsonSO;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(JsonObject))]
public class JsonObjectDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty convertSetting = property.FindPropertyRelative($"<{nameof(JsonObject.ConvertSetting)}>k__BackingField");
        SerializedProperty json = property.FindPropertyRelative($"<{nameof(JsonObject.Json)}>k__BackingField");
        SerializedProperty root = property.FindPropertyRelative($"<{nameof(JsonObject.Root)}>k__BackingField");
        
        var convertRect = new Rect(position) {height = EditorGUIUtility.singleLineHeight};
        var jsonRect = new Rect(position) {yMin = convertRect.yMax + 9, height = EditorGUI.GetPropertyHeight(json) * 2};
        var rootRect = new Rect(position) {yMin = jsonRect.yMax + 9, height = EditorGUI.GetPropertyHeight(root)};

        convertSetting.intValue = GUI.Toolbar(convertRect, convertSetting.intValue, Enum.GetNames(typeof(ConvertType)));
        EditorGUI.PropertyField(jsonRect, json, new GUIContent("Json File"));
        EditorGUI.PropertyField(rootRect, root, new GUIContent(property.displayName));
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty json = property.FindPropertyRelative($"<{nameof(JsonObject.Json)}>k__BackingField");
        SerializedProperty root = property.FindPropertyRelative($"<{nameof(JsonObject.Root)}>k__BackingField");
        return EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(json) * 2 + EditorGUI.GetPropertyHeight(root) + 18;
    }
}
