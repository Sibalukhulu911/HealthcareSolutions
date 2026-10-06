using System.Net.Http.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class MedicationRequestApiService
{
    private readonly ApiHttpClient _apiHttpClient;

public MedicationRequestApiService(
    ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<IEnumerable<MedicationRequestResponse>>
        GetAllAsync()
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                "api/MedicationRequests");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            IEnumerable<MedicationRequestResponse>>()
            ?? Enumerable.Empty<MedicationRequestResponse>();
    }

    public async Task<IEnumerable<MedicationRequestResponse>>
        GetByPractitionerIdAsync(
            int practitionerId)
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                $"api/MedicationRequests/practitioner/{practitionerId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            IEnumerable<MedicationRequestResponse>>()
            ?? Enumerable.Empty<MedicationRequestResponse>();
    }

    public async Task<MedicationRequestResponse?>
        GetByIdAsync(int id)
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                $"api/MedicationRequests/{id}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<
            MedicationRequestResponse>();
    }

    public async Task<MedicationRequestResponse>
        CreateAsync(
            MedicationRequestCreateRequest request)
    {
        var response =
            await _apiHttpClient.Client.PostAsJsonAsync(
                "api/MedicationRequests",
                request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            MedicationRequestResponse>()
            ?? throw new InvalidOperationException(
                "The API returned an empty medication request.");
    }

    public async Task<MedicationRequestResponse>
        ReviewAsync(
            int id,
            MedicationRequestReviewRequest request)
    {
        var response =
            await _apiHttpClient.Client.PutAsJsonAsync(
                $"api/MedicationRequests/{id}/review",
                request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            MedicationRequestResponse>()
            ?? throw new InvalidOperationException(
                "The API returned an empty medication request.");
    }
}