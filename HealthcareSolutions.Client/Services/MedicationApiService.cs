using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class MedicationApiService
{
    private readonly ApiHttpClient _apiHttpClient;

    public MedicationApiService(
        ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<List<MedicationResponse>> GetAllAsync()
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                "api/Medications");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }

        var medications =
            await response.Content
                .ReadFromJsonAsync<List<MedicationResponse>>();

        return medications ?? new List<MedicationResponse>();
    }

    public async Task<MedicationResponse?> GetByIdAsync(int id)
    {
        var response =
            await _apiHttpClient.Client.GetAsync(
                $"api/Medications/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }

        return await response.Content
            .ReadFromJsonAsync<MedicationResponse>();
    }

    public async Task<MedicationResponse?> CreateAsync(
        MedicationCreateRequest request)
    {
        var response =
            await _apiHttpClient.Client.PostAsJsonAsync(
                "api/Medications",
                request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }

        return await response.Content
            .ReadFromJsonAsync<MedicationResponse>();
    }

    public async Task UpdateAsync(
        int id,
        MedicationUpdateRequest request)
    {
        var response =
            await _apiHttpClient.Client.PutAsJsonAsync(
                $"api/Medications/{id}",
                request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }
    }

    public async Task DeleteAsync(int id)
    {
        var response =
            await _apiHttpClient.Client.DeleteAsync(
                $"api/Medications/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(response));
        }
    }

    private static async Task<string> GetErrorMessageAsync(
        HttpResponseMessage response)
    {
        var body =
            await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(body))
        {
            try
            {
                using var document =
                    JsonDocument.Parse(body);

                if (document.RootElement.TryGetProperty(
                    "message",
                    out var message))
                {
                    var messageText =
                        message.GetString();

                    if (!string.IsNullOrWhiteSpace(messageText))
                    {
                        return messageText;
                    }
                }
            }
            catch (JsonException)
            {
                // Response was not JSON.
                // Fall back to the raw response body.
            }

            return body;
        }

        return
            $"Request failed with status code {(int)response.StatusCode}.";
    }
}
