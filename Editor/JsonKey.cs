using UnityEngine;

[System.Serializable]
public class JsonKey
{
    [SerializeField] string key;
    public JsonKey() { }

    public JsonKey(string key)
    {
        Key = key;
    }

    public string Key
    {
        get => key;
        set => key = value;
    }
    
    public JsonKey Clone()
    {
        var key = new JsonKey();
        key.Key = Key;
        return key;
    }
}