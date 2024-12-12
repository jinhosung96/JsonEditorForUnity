using System;
using System.Linq;
using Mine.Code.App.JsonSO;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public class JsonObject : ISerializationCallbackReceiver
{
    [field: SerializeField] public int ConvertSetting { get; private set; }
    [field: SerializeField, TextArea] public string Json { get; set; }
    [field: SerializeField] public JsonKeyValuePair[] Root { get; private set; }

    public T Value<T>(string key) => Root.FirstOrDefault(pair => pair.Key == key)!.Value<T>();

    public JsonKeyValuePair[] Clone() => Root.Select(pair => pair.Clone()).ToArray();

    public void OnBeforeSerialize()
    {
        switch ((ConvertType)ConvertSetting)
        {
            case ConvertType.JsonToObject:
                JsonToObject();
                break;
            case ConvertType.ObjectToJson:
                ObjectToJson();
                break;
        }
    }

    public void OnAfterDeserialize()
    {
        
    }

    void JsonToObject()
    {
        try
        {
            var jObject = JObject.Parse(Json);
            Root = jObject.Properties().Select(x =>
            {
                var pair = new JsonKeyValuePair();
                pair.SetByToken(x);
                return pair;
            }).ToArray();
        }
        catch { }
    }

    void ObjectToJson()
    {
        try
        {
            var jObject = new JObject();
            jObject.Add(Root.Select(pair => new JProperty(pair.Key, pair.GetToken())));
            Json = jObject.ToString();
        }
        catch { }
    }
    
    public void ObjectToFullJson()
    {
        try
        {
            var jObject = new JObject();
            jObject.Add(Root.Select(pair => new JProperty(pair.Key, pair.GetToken())));
            Json = jObject.ToString();
        }
        catch { }
    }
}