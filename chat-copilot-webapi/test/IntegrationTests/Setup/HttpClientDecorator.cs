using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntegrationTests.Setup;

public class HttpClientDecorator
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

    public HttpClientDecorator(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(HttpStatusCode statusCode, T result)> GetAsync<T>(string uri)
    {
        var response = await _httpClient.GetAsync(uri);
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

        return (response!.StatusCode, result!);
    }

    public async Task<(HttpStatusCode statusCode, TResponse result)> PostAsync<TRequest, TResponse>(
        string uri,
        TRequest body)
    {
        var response = await _httpClient.PostAsync(
            uri,
            new StringContent(
                JsonSerializer.Serialize(body, _jsonSerializerOptions)
                , Encoding.UTF8,
                "application/json"));
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TResponse>(json, _jsonSerializerOptions);

        return (response!.StatusCode, result!);
    }
}

