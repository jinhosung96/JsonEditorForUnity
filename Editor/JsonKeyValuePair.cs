using Mine.Code.App.JsonSO;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public class JsonKeyValuePair
{
    [SerializeField] JsonKey key;
    [SerializeField] JsonValue value;

    public string Key => key.Key;

    public JsonPropertyType PropertyType
    {
        get => value.PropertyType;
        set => this.value.PropertyType = value;
    }
    public JToken GetFullToken() => value.GetFullToken();
    public JToken GetToken() => value.GetToken();
    
    public void SetByToken(JProperty property)
    {
        key = new JsonKey(key: property.Name.Replace("$", ""));
        value = new JsonValue();
        value.SetByToken(property.Value);
    }

    public object Value() => value.Value();
    public T Value<T>() => value.Value<T>();
    
    public JsonKeyValuePair Clone()
    {
        var pair = new JsonKeyValuePair();
        pair.key = key.Clone();
        pair.value = value.Clone();
        return pair;
    }
}