using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace JHS.Library.JsonEditor.Editor
{
    [System.Serializable]
    public class JsonValue : ISerializationCallbackReceiver
    {
        [SerializeField] int propertyType;
        [SerializeField] string stringValue;
        [SerializeField] int intValue;
        [SerializeField] float floatValue;
        [SerializeField] bool boolValue;
        [SerializeField] JsonValue[] arrayValue;
        [SerializeField] JsonKeyValuePair[] objectValue;
        [SerializeField] AssetReference assetReferenceValue;
        [SerializeField] JsonScriptableObject jsonObjectValue;

        public JsonPropertyType PropertyType
        {
            get => (JsonPropertyType)propertyType;
            set => propertyType = (int)value;
        }

        public JToken GetFullToken()
        {
            switch (PropertyType)
            {
                case JsonPropertyType.String:
                    return stringValue;
                case JsonPropertyType.Integer:
                    return intValue;
                case JsonPropertyType.Float:
                    return floatValue;
                case JsonPropertyType.Boolean:
                    return boolValue;
                case JsonPropertyType.Array:
                {
                    var jArray = new JArray();
                    foreach (var value in arrayValue) jArray.Add(value.GetFullToken());
                    return jArray;
                }
                case JsonPropertyType.Object:
                {
                    var jObject = new JObject();
                    jObject.Add(objectValue.Select(pair => new JProperty(pair.Key, pair.GetFullToken())));
                    return jObject;
                }
                case JsonPropertyType.Addressable:
                {
                    var jObject = new JObject();
                    jObject.Add("Guid", assetReferenceValue.AssetGUID);
                    return jObject;
                }
                case JsonPropertyType.JsonObject:
                {
                    if(jsonObjectValue == null) return null;
                    var jObject = new JObject();
                    jObject.Add(jsonObjectValue.Data.Root.Select(pair => new JProperty(pair.Key, pair.GetFullToken())));
                    return jObject;
                }
                default:
                    return null;
            }
        }

        public JToken GetToken()
        {
            switch (PropertyType)
            {
                case JsonPropertyType.String:
                    return stringValue;
                case JsonPropertyType.Integer:
                    return intValue;
                case JsonPropertyType.Float:
                    return floatValue;
                case JsonPropertyType.Boolean:
                    return boolValue;
                case JsonPropertyType.Array:
                {
                    var jArray = new JArray();
                    foreach (var value in arrayValue) jArray.Add(value.GetToken());
                    return jArray;
                }
                case JsonPropertyType.Object:
                {
                    var jObject = new JObject();
                    jObject.Add(objectValue.Select(pair => new JProperty(pair.Key, pair.GetToken())));
                    return jObject;
                }
                case JsonPropertyType.Addressable:
                {
                    var jObject = new JObject();
                    jObject.Add("Guid", assetReferenceValue.AssetGUID);
                    return jObject;
                }
                case JsonPropertyType.JsonObject:
                {
                    var jObject = new JObject();
                    jObject.Add(new JProperty("AssetPath", jsonObjectValue ? AssetDatabase.GetAssetPath(jsonObjectValue) : ""));
                    return jObject;
                }
                default:
                    return null;
            }
        }

        public void SetByToken(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.String:
                    PropertyType = JsonPropertyType.String;
                    stringValue = token.Value<string>();
                    break;
                case JTokenType.Integer:
                    PropertyType = JsonPropertyType.Integer;
                    intValue = token.Value<int>();
                    break;
                case JTokenType.Float:
                    PropertyType = JsonPropertyType.Float;
                    floatValue = token.Value<float>();
                    break;
                case JTokenType.Boolean:
                    PropertyType = JsonPropertyType.Boolean;
                    boolValue = token.Value<bool>();
                    break;
                case JTokenType.Array:
                    PropertyType = JsonPropertyType.Array;
                    var jArray = token.Value<JArray>();
                    arrayValue = jArray.Select(x =>
                    {
                        var value = new JsonValue();
                        value.SetByToken(x);
                        return value;
                    }).ToArray();
                    break;
                case JTokenType.Object:
                    if (token.Count() == 1 && token.SelectToken("AssetPath") != null)
                    {
                        PropertyType = JsonPropertyType.JsonObject;
                        jsonObjectValue = AssetDatabase.LoadAssetAtPath<JsonScriptableObject>(token.Value<string>("AssetPath"));
                    }
                    else if (token.Count() == 1 && token.SelectToken("Guid") != null)
                    {
                        PropertyType = JsonPropertyType.Addressable;
                        assetReferenceValue = new AssetReference(token.Value<string>("Guid"));
                    }
                    else
                    {
                        PropertyType = JsonPropertyType.Object;
                        objectValue = token.ToObject<JObject>()!.Properties().Select(x =>
                        {
                            var pair = new JsonKeyValuePair();
                            pair.SetByToken(x);
                            return pair;
                        }).ToArray();
                    }

                    break;
            }
        }

        public object Value() => PropertyType switch
        {
            JsonPropertyType.String => stringValue,
            JsonPropertyType.Integer => intValue,
            JsonPropertyType.Float => floatValue,
            JsonPropertyType.Boolean => boolValue,
            JsonPropertyType.Array => arrayValue,
            JsonPropertyType.Object => objectValue,
            JsonPropertyType.Addressable => assetReferenceValue.AssetGUID,
            JsonPropertyType.JsonObject => jsonObjectValue,
            _ => default
        };

        public T Value<T>() => (T)Value();
        public T Value<T>(string key) => objectValue.FirstOrDefault(pair => pair.Key == key)!.Value<T>();
        public T Value<T>(int index) => arrayValue[index].Value<T>();

        public JsonValue Clone()
        {
            var value = new JsonValue();
            value.propertyType = propertyType;
            value.stringValue = stringValue;
            value.intValue = intValue;
            value.floatValue = floatValue;
            value.boolValue = boolValue;
            value.arrayValue = arrayValue?.Select(x => x.Clone()).ToArray();
            value.objectValue = objectValue?.Select(x => x.Clone()).ToArray();
            value.assetReferenceValue = assetReferenceValue;
            value.jsonObjectValue = jsonObjectValue;
            return value;
        }

        public void OnBeforeSerialize()
        {
            switch (PropertyType)
            {
                case JsonPropertyType.String:
                    intValue = default;
                    floatValue = default;
                    boolValue = default;
                    arrayValue = default;
                    objectValue = default;
                    assetReferenceValue = default;
                    jsonObjectValue = default;
                    break;
                case JsonPropertyType.Integer:
                    stringValue = default;
                    floatValue = default;
                    boolValue = default;
                    arrayValue = default;
                    objectValue = default;
                    assetReferenceValue = default;
                    jsonObjectValue = default;
                    break;
                case JsonPropertyType.Float:
                    stringValue = default;
                    intValue = default;
                    boolValue = default;
                    arrayValue = default;
                    objectValue = default;
                    assetReferenceValue = default;
                    jsonObjectValue = default;
                    break;
                case JsonPropertyType.Boolean:
                    stringValue = default;
                    intValue = default;
                    floatValue = default;
                    arrayValue = default;
                    objectValue = default;
                    assetReferenceValue = default;
                    jsonObjectValue = default;
                    break;
                case JsonPropertyType.Array:
                    stringValue = default;
                    intValue = default;
                    floatValue = default;
                    boolValue = default;
                    objectValue = default;
                    assetReferenceValue = default;
                    jsonObjectValue = default;
                    break;
                case JsonPropertyType.Object:
                    stringValue = default;
                    intValue = default;
                    floatValue = default;
                    boolValue = default;
                    arrayValue = default;
                    assetReferenceValue = default;
                    jsonObjectValue = default;
                    break;
                case JsonPropertyType.Addressable:
                    stringValue = default;
                    intValue = default;
                    floatValue = default;
                    boolValue = default;
                    arrayValue = default;
                    objectValue = default;
                    jsonObjectValue = default;
                    break;
                case JsonPropertyType.JsonObject:
                    stringValue = default;
                    intValue = default;
                    floatValue = default;
                    boolValue = default;
                    arrayValue = default;
                    objectValue = default;
                    assetReferenceValue = default;
                    break;
            }
        }

        public void OnAfterDeserialize() { }
    }
}