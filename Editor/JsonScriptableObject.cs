using UnityEngine;

namespace JHS.Library.JsonEditor.Editor
{
    [CreateAssetMenu(fileName = "JsonScriptableObject", menuName = "ScriptableObjects/JsonScriptableObject", order = 1)]
    public class JsonScriptableObject : ScriptableObject
    {
        [field: SerializeField] public JsonObject Data { get; private set; }
    }
}