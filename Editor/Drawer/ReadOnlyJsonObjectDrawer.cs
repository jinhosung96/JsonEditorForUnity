using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace JHS.Library.JsonEditor.Editor.Drawer
{
    [CustomPropertyDrawer(typeof(ReadOnlyJsonObject))]
    public class ReadOnlyJsonObjectDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty json = property.FindPropertyRelative($"<{nameof(ReadOnlyJsonObject.Json)}>k__BackingField");
            SerializedProperty preset = property.FindPropertyRelative($"<{nameof(ReadOnlyJsonObject.Preset)}>k__BackingField");
            SerializedProperty root = property.FindPropertyRelative($"<{nameof(ReadOnlyJsonObject.Root)}>k__BackingField");

            var presetRect = new Rect(position) { height = EditorGUIUtility.singleLineHeight };
            var jsonRect = new Rect(position) { yMin = presetRect.yMax, height = EditorGUI.GetPropertyHeight(json) * 2 };

            var origin = preset.objectReferenceValue;

            EditorGUI.BeginChangeCheck();

            property.serializedObject.Update();
            EditorGUI.PropertyField(presetRect, preset, new GUIContent("Preset"));
            property.serializedObject.ApplyModifiedProperties();

            object target = GetTargetObjectOfProperty(property);
            if (preset.objectReferenceValue && origin != preset.objectReferenceValue)
            {
                SerializedProperty presetData = new SerializedObject(preset.objectReferenceValue)
                    .FindProperty($"<{nameof(JsonScriptableObject.Data)}>k__BackingField");

                var targetPreset = GetTargetObjectOfProperty(presetData);
                ((ReadOnlyJsonObject)target).GetClone((JsonObject)targetPreset);
            }
            property.serializedObject.Update();

            if (preset.objectReferenceValue)
            {
                EditorGUI.PropertyField(jsonRect, json, new GUIContent("Json File"));

                var rootRect = new Rect(position) { yMin = jsonRect.yMax + 9, height = EditorGUI.GetPropertyHeight(root) };

                GUI.enabled = false;
                EditorGUI.PropertyField(rootRect, root, new GUIContent(property.displayName));
                GUI.enabled = true;
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        public static object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            if (prop == null) return null;

            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }

            return obj;
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }

            return null;
        }

        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }

            return enm.Current;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty json = property.FindPropertyRelative($"<{nameof(ReadOnlyJsonObject.Json)}>k__BackingField");
            SerializedProperty preset = property.FindPropertyRelative($"<{nameof(ReadOnlyJsonObject.Preset)}>k__BackingField");
            SerializedProperty root = property.FindPropertyRelative($"<{nameof(ReadOnlyJsonObject.Root)}>k__BackingField");
            if (preset.objectReferenceValue)
            {
                return EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(json) * 2 + EditorGUI.GetPropertyHeight(root) + 18;
            }

            return EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(json) * 2 + 9;
        }
    }
}