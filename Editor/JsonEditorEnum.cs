namespace Mine.Code.App.JsonSO
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