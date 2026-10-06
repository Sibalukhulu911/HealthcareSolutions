using System.Net.Http.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class PrescriptionApiService
{
    private readonly ApiHttpClient _apiHttpClient;

    public PrescriptionApiService(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<IEnumerable<PrescriptionResponse>>
        GetByMedicalRecordIdAsync(int medicalRecordId)
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                $"api/Prescriptions/medical-record/{medicalRecordId}");

        response.EnsureSuccessStatusCode();

        var prescriptions =
            await response.Content
                .ReadFromJsonAsync<IEnumerable<PrescriptionResponse>>();

        return prescriptions ?? Enumerable.Empty<PrescriptionResponse>();
    }

    public async Task<PrescriptionResponse?> GetByIdAsync(int id)
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                $"api/Prescriptions/{id}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content
            .ReadFromJsonAsync<PrescriptionResponse>();
    }

    public async Task<PrescriptionResponse> CreateAsync(
        PrescriptionCreateRequest request)
    {
        var response =
            await _apiHttpClient.Client.PostAsJsonAsync(
                "api/Prescriptions",
                request);

        response.EnsureSuccessStatusCode();

        var prescription =
            await response.Content
                .ReadFromJsonAsync<PrescriptionResponse>();

        return prescription
            ?? throw new InvalidOperationException(
                "The API returned an empty prescription.");
    }

    public async Task UpdateStatusAsync(
        int id,
        PrescriptionUpdateStatusRequest request)
    {
        var response =
            await _apiHttpClient.Client.PutAsJsonAsync(
                $"api/Prescriptions/{id}/status",
                request);

        response.EnsureSuccessStatusCode();
    }
}
