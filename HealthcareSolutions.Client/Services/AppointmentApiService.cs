using System.Net;
using System.Net.Http.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class AppointmentApiService
{
    private readonly ApiHttpClient _apiHttpClient;

    public AppointmentApiService(
        ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<List<AppointmentResponse>> GetAllAsync()
    {
        var response =
            await _apiHttpClient.Client
                .GetAsync("api/Appointments");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Unable to load appointments. " +
                $"Status code: {(int)response.StatusCode}");
        }

        var appointments =
            await response.Content
                .ReadFromJsonAsync<List<AppointmentResponse>>();

        return appointments ?? new List<AppointmentResponse>();
    }

    public async Task<AppointmentResponse?> GetByIdAsync(
        int id)
    {
        var response =
            await _apiHttpClient.Client
                .GetAsync($"api/Appointments/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Unable to load appointment. " +
                $"Status code: {(int)response.StatusCode}");
        }

        return await response.Content
            .ReadFromJsonAsync<AppointmentResponse>();
    }

    public async Task<AppointmentResponse?> CreateAsync(
        AppointmentCreateRequest request)
    {
        var response =
            await _apiHttpClient.Client
                .PostAsJsonAsync(
                    "api/Appointments",
                    request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(
                    response,
                    "Unable to create appointment."));
        }

        return await response.Content
            .ReadFromJsonAsync<AppointmentResponse>();
    }

    public async Task<bool> UpdateAsync(
        int id,
        AppointmentUpdateRequest request)
    {
        var response =
            await _apiHttpClient.Client
                .PutAsJsonAsync(
                    $"api/Appointments/{id}",
                    request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(
                    response,
                    "Unable to update appointment."));
        }

        return true;
    }

    public async Task<bool> CancelAsync(int id)
    {
        var response =
            await _apiHttpClient.Client
                .DeleteAsync(
                    $"api/Appointments/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                await GetErrorMessageAsync(
                    response,
                    "Unable to cancel appointment."));
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