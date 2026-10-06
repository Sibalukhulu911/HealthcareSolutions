using System.Net;
using System.Net.Http.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class MedicalRecordApiService
{
    private readonly ApiHttpClient _apiHttpClient;

    public MedicalRecordApiService(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<MedicalRecordResponse?> GetByIdAsync(int id)
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                $"api/MedicalRecords/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }

        return await response.Content
            .ReadFromJsonAsync<MedicalRecordResponse>();
    }

    public async Task<MedicalRecordResponse?> GetByPatientIdAsync(
        int patientId)
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                $"api/MedicalRecords/patient/{patientId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }

        return await response.Content
            .ReadFromJsonAsync<MedicalRecordResponse>();
    }

    public async Task<MedicalRecordResponse?> CreateAsync(
        MedicalRecordCreateRequest request)
    {
        var response =
            await _apiHttpClient.Client.PostAsJsonAsync(
                "api/MedicalRecords",
                request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }

        return await response.Content
            .ReadFromJsonAsync<MedicalRecordResponse>();
    }

    private static async Task<string> GetErrorMessageAsync(
        HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(body))
            return body;

        return $"Request failed with status code {(int)response.StatusCode}.";
    }
}