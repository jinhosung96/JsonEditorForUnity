using UnityEngine;

namespace MyAssets.Scripts.App.JsonEditor.Editor.JsonEditorByPresetScriptableObjectcs
{
    [CreateAssetMenu(fileName = "ReadOnlyJsonScriptableObject", menuName = "ScriptableObjects/ReadOnlyJsonScriptableObject", order = 1)]
    public class ReadOnlyJsonScriptableObject : ScriptableObject
    {
        [field: SerializeField] public ReadOnlyJsonObject Preset { get; private set; }
    }
}