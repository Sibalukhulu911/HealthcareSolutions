using System.Net;
using System.Net.Http.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class PractitionerApiService
{
    private readonly ApiHttpClient _apiHttpClient;

    public PractitionerApiService(
        ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<List<PractitionerResponse>> GetAllAsync()
    {
        var response =
            await _apiHttpClient.Client
                .GetAsync("api/Practitioners");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Unable to load practitioners. " +
                $"Status code: {(int)response.StatusCode}");
        }

        var practitioners =
            await response.Content
                .ReadFromJsonAsync<List<PractitionerResponse>>();

        return practitioners ?? new List<PractitionerResponse>();
    }

    public async Task<PractitionerResponse?> GetByIdAsync(
        int id)
    {
        var response =
            await _apiHttpClient.Client
                .GetAsync($"api/Practitioners/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Unable to load practitioner. " +
                $"Status code: {(int)response.StatusCode}");
        }

        return await response.Content
            .ReadFromJsonAsync<PractitionerResponse>();
    }

    public async Task<PractitionerResponse?> CreateAsync(
        PractitionerCreateRequest request)
    {
        var response =
            await _apiHttpClient.Client
                .PostAsJsonAsync(
                    "api/Practitioners",
                    request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(
                    response,
                    "Unable to create practitioner."));
        }

        return await response.Content
            .ReadFromJsonAsync<PractitionerResponse>();
    }

    public async Task<bool> UpdateAsync(
        int id,
        PractitionerUpdateRequest request)
    {
        var response =
            await _apiHttpClient.Client
                .PutAsJsonAsync(
                    $"api/Practitioners/{id}",
                    request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(
                    response,
                    "Unable to update practitioner."));
        }

        return true;
    }

    private static async Task<string> GetErrorMessageAsync(
        HttpResponseMessage response,
        string defaultMessage)
    {
        var body =
            await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(body))
        {
            return body;
        }

        return $"{defaultMessage} " +
               $"Status code: {(int)response.StatusCode}";
    }
}