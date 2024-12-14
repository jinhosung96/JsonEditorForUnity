using UnityEngine;

namespace JHS.Library.JsonEditor.Editor
{
    [CreateAssetMenu(fileName = "ReadOnlyJsonScriptableObject", menuName = "ScriptableObjects/ReadOnlyJsonScriptableObject", order = 1)]
    public class ReadOnlyJsonScriptableObject : ScriptableObject
    {
        [field: SerializeField] public ReadOnlyJsonObject Preset { get; private set; }
    }
}