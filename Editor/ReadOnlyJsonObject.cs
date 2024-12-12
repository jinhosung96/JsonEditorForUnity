using System;
using System.Linq;
using Mine.Code.Framework.Extension;
using MyAssets.Scripts.App.JsonEditor.Editor.JsonPresetScriptableObjectcs;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public class ReadOnlyJsonObject : ISerializationCallbackReceiver
{
    [field: SerializeField, TextArea] public string Json { get; private set; }
    [field: SerializeField] public JsonScriptableObject Preset { get; private set; }
    [field: SerializeField] public JsonKeyValuePair[] Root { get; set; }

    public T Value<T>(string key) => Preset.Data.Root.FirstOrDefault(pair => pair.Key == key)!.Value<T>();

    public void OnBeforeSerialize()
    {
        try
        {
            var jObject = new JObject();
            jObject.Add(Root.Select(pair => new JProperty(pair.Key, pair.GetFullToken())));
            Json = jObject.ToString();
        }
        catch { }
    }

    public void OnAfterDeserialize() { }

    public void GetClone(JsonObject preset)
    {
        Root = preset.Clone();
    }
}