namespace FleetManagement.Application.JsonExtensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public static T FromJson<T>(string json) =>
        JsonSerializer.Deserialize<T>(json, _jsonOptions);

    public static string ToJson<T>(T obj) =>
        JsonSerializer.Serialize<T>(obj, _jsonOptions);
}
