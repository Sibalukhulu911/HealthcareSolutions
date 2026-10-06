using System.Net.Http.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class UserApiService
{
    private readonly ApiHttpClient _apiHttpClient;

    public UserApiService(
        ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        var response =
            await _apiHttpClient.Client
                .GetAsync("api/Users");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Unable to load users. " +
                $"Status code: {(int)response.StatusCode}");
        }

        var users =
            await response.Content
                .ReadFromJsonAsync<List<UserResponse>>();

        return users ?? new List<UserResponse>();
    }

    public async Task<UserResponse?> GetByIdAsync(int id)
    {
        var response =
            await _apiHttpClient.Client
                .GetAsync($"api/Users/{id}");

        if (response.StatusCode ==
            System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Unable to load user. " +
                $"Status code: {(int)response.StatusCode}");
        }

        return await response.Content
            .ReadFromJsonAsync<UserResponse>();
    }

    public async Task<UserResponse> CreateAsync(
        UserCreateRequest request)
    {
        var response =
            await _apiHttpClient.Client
                .PostAsJsonAsync(
                    "api/Users",
                    request);

        if (!response.IsSuccessStatusCode)
        {
            var message =
                await ExtractErrorMessage(response);

            throw new InvalidOperationException(message);
        }

        var user =
            await response.Content
                .ReadFromJsonAsync<UserResponse>();

        if (user == null)
        {
            throw new InvalidOperationException(
                "The server returned an empty user response.");
        }

        return user;
    }

    public async Task UpdateAsync(
        int id,
        UserUpdateRequest request)
    {
        var response =
            await _apiHttpClient.Client
                .PutAsJsonAsync(
                    $"api/Users/{id}",
                    request);

        if (!response.IsSuccessStatusCode)
        {
            var message =
                await ExtractErrorMessage(response);

            throw new InvalidOperationException(message);
        }
    }

    public async Task DeactivateAsync(int id)
    {
        var response =
            await _apiHttpClient.Client
                .DeleteAsync($"api/Users/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var message =
                await ExtractErrorMessage(response);

            throw new InvalidOperationException(message);
        }
    }

    private static async Task<string> ExtractErrorMessage(
        HttpResponseMessage response)
    {
        try
        {
            var error =
                await response.Content
                    .ReadFromJsonAsync<ErrorResponse>();

            if (!string.IsNullOrWhiteSpace(error?.Message))
            {
                return error.Message;
            }
        }
        catch
        {
            // Fall back to status code below.
        }

        return
            $"Request failed. " +
            $"Status code: {(int)response.StatusCode}";
    }

    private class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}