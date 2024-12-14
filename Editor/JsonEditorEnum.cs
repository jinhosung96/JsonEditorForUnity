namespace JHS.Library.JsonEditor.Editor
{
    public enum JsonPropertyType
    {
        String,
        Integer,
        Float,
        Boolean,
        Array,
        Object,
        Addressable,
        JsonObject
    }

    public enum ConvertType
    {
        JsonToObject,
        ObjectToJson
    }
}