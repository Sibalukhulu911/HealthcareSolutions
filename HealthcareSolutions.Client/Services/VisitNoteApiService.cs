using System.Net;
using System.Net.Http.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class VisitNoteApiService
{
    private readonly ApiHttpClient _apiHttpClient;

    public VisitNoteApiService(
        ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<List<VisitNoteResponse>>
        GetByMedicalRecordIdAsync(int medicalRecordId)
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                $"api/VisitNotes/medical-record/{medicalRecordId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }

        var notes =
            await response.Content
                .ReadFromJsonAsync<List<VisitNoteResponse>>();

        return notes ?? new List<VisitNoteResponse>();
    }

    public async Task<VisitNoteResponse?> GetByIdAsync(int id)
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                $"api/VisitNotes/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }

        return await response.Content
            .ReadFromJsonAsync<VisitNoteResponse>();
    }

    public async Task<VisitNoteResponse?> CreateAsync(
        VisitNoteCreateRequest request)
    {
        var response =
            await _apiHttpClient.Client.PostAsJsonAsync(
                "api/VisitNotes",
                request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }

        return await response.Content
            .ReadFromJsonAsync<VisitNoteResponse>();
    }

    public async Task UpdateAsync(
        int id,
        VisitNoteUpdateRequest request)
    {
        var response =
            await _apiHttpClient.Client.PutAsJsonAsync(
                $"api/VisitNotes/{id}",
                request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }
    }

    private static async Task<string>
        GetErrorMessageAsync(
            HttpResponseMessage response)
    {
        var body =
            await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(body))
            return body;

        return
            $"Request failed with status code {(int)response.StatusCode}.";
    }
}
