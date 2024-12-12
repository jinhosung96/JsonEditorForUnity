using System;
using Mine.Code.App.JsonSO;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(JsonValue))]
public class JsonValueDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var typeProperty = property.FindPropertyRelative("propertyType");
        var stringValueProperty = property.FindPropertyRelative("stringValue");
        var intValueProperty = property.FindPropertyRelative("intValue");
        var floatValueProperty = property.FindPropertyRelative("floatValue");
        var boolValueProperty = property.FindPropertyRelative("boolValue");
        var arrayValueProperty = property.FindPropertyRelative("arrayValue");
        var objectValueProperty = property.FindPropertyRelative("objectValue");
        var assetReferenceValueProperty = property.FindPropertyRelative("assetReferenceValue");
        var jsonObjectValueProperty = property.FindPropertyRelative("jsonObjectValue");

        var typeRect = new Rect(position.x, position.y, 100, EditorGUIUtility.singleLineHeight);
        typeProperty.intValue = EditorGUI.Popup(typeRect, typeProperty.intValue, Enum.GetNames(typeof(JsonPropertyType)));
        switch (typeProperty.intValue)
        {
            case (int)JsonPropertyType.String:
            {
                var origin = GUI.enabled;
                GUI.enabled = true;
                var valueRect = new Rect(typeRect.xMax + 4, position.y, position.width - 104, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(valueRect, stringValueProperty, GUIContent.none);
                GUI.enabled = origin;
                break;
            }
            case (int)JsonPropertyType.Integer:
            {
                var origin = GUI.enabled;
                GUI.enabled = true;
                var valueRect = new Rect(typeRect.xMax + 4, position.y, position.width - 104, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(valueRect, intValueProperty, GUIContent.none);
                GUI.enabled = origin;
                break;
            }
            case (int)JsonPropertyType.Float:
            {
                var origin = GUI.enabled;
                GUI.enabled = true;
                var valueRect = new Rect(typeRect.xMax + 4, position.y, position.width - 104, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(valueRect, floatValueProperty, GUIContent.none);
                GUI.enabled = origin;
                break;
            }
            case (int)JsonPropertyType.Boolean:
            {
                var origin = GUI.enabled;
                GUI.enabled = true;
                var valueRect = new Rect(typeRect.xMax + 4, position.y, position.width - 104, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(valueRect, boolValueProperty, GUIContent.none);
                GUI.enabled = origin;
                break;
            }
            case (int)JsonPropertyType.Array:
            {
                var valueRect = new Rect(typeRect.xMax + 18, position.y, position.width - 118, EditorGUI.GetPropertyHeight(arrayValueProperty, arrayValueProperty.isExpanded));
                EditorGUI.PropertyField(valueRect, arrayValueProperty, new GUIContent("Array"));
                break;
            }
            case (int)JsonPropertyType.Object:
            {
                var valueRect = new Rect(typeRect.xMax + 18, position.y, position.width - 118, EditorGUI.GetPropertyHeight(objectValueProperty, objectValueProperty.isExpanded));
                EditorGUI.PropertyField(valueRect, objectValueProperty, new GUIContent("Object"));
                break;
            }
            case (int)JsonPropertyType.Addressable:
            {
                var origin = GUI.enabled;
                GUI.enabled = true;
                var valueRect = new Rect(typeRect.xMax + 4, position.y, position.width - 104, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(valueRect, assetReferenceValueProperty, GUIContent.none);
                GUI.enabled = origin;
                break;
            }
            case (int)JsonPropertyType.JsonObject:
            {
                var origin = GUI.enabled;
                GUI.enabled = true;
                var valueRect = new Rect(typeRect.xMax + 4, position.y, position.width - 104, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(valueRect, jsonObjectValueProperty, GUIContent.none);
                GUI.enabled = origin;
                break;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var typeProperty = property.FindPropertyRelative("propertyType");
        var arrayValueProperty = property.FindPropertyRelative("arrayValue");
        var objectValueProperty = property.FindPropertyRelative("objectValue");
        switch (typeProperty.intValue)
        {
            case (int)JsonPropertyType.Array:
                return EditorGUI.GetPropertyHeight(arrayValueProperty, arrayValueProperty.isExpanded);
            case (int)JsonPropertyType.Object:
                return EditorGUI.GetPropertyHeight(objectValueProperty, objectValueProperty.isExpanded);
            default:
                return EditorGUIUtility.singleLineHeight;
        }
    }
}
