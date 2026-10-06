using System.Net;
using System.Net.Http.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class PatientApiService
{
    private readonly ApiHttpClient _apiHttpClient;

    public PatientApiService(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<List<PatientResponse>> GetAllAsync()
    {
        var response =
            await _apiHttpClient.Client
                .GetAsync("api/Patients");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Unable to load patients. " +
                $"Status code: {(int)response.StatusCode}");
        }

        var patients =
            await response.Content
                .ReadFromJsonAsync<List<PatientResponse>>();

        return patients ?? new List<PatientResponse>();
    }

    public async Task<PatientResponse?> GetByIdAsync(int id)
    {
        var response =
            await _apiHttpClient.Client
                .GetAsync($"api/Patients/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Unable to load patient. " +
                $"Status code: {(int)response.StatusCode}");
        }

        return await response.Content
            .ReadFromJsonAsync<PatientResponse>();
    }

    public async Task<PatientResponse?> CreateAsync(
        PatientCreateRequest request)
    {
        var response =
            await _apiHttpClient.Client
                .PostAsJsonAsync(
                    "api/Patients",
                    request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(
                    response,
                    "Unable to create patient."));
        }

        return await response.Content
            .ReadFromJsonAsync<PatientResponse>();
    }

    public async Task<bool> UpdateAsync(
        int id,
        PatientUpdateRequest request)
    {
        var response =
            await _apiHttpClient.Client
                .PutAsJsonAsync(
                    $"api/Patients/{id}",
                    request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(
                    response,
                    "Unable to update patient."));
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