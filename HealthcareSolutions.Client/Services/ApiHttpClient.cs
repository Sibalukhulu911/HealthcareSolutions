using System.Net.Http;
using HealthcareSolutions.Client.Authentication;

namespace HealthcareSolutions.Client.Services;

public class ApiHttpClient
{
    private readonly HttpClient _httpClient;

    public ApiHttpClient(
        IHttpClientFactory httpClientFactory)
    {
        _httpClient =
            httpClientFactory.CreateClient("ApiClient");
    }

    public HttpClient Client => _httpClient;
}