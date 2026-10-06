using System.Net.Http.Json;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly TokenStorage _tokenStorage;

    public LoginResponse? CurrentUser { get; private set; }

    public bool IsAuthenticated =>
        CurrentUser != null &&
        !string.IsNullOrWhiteSpace(CurrentUser.Token);

    public event Action? AuthenticationStateChanged;

    public AuthService(
        HttpClient httpClient,
        TokenStorage tokenStorage)
    {
        _httpClient = httpClient;
        _tokenStorage = tokenStorage;
    }

    public async Task<bool> LoginAsync(
        string username,
        string password)
    {
        var request = new LoginRequest
        {
            Username = username,
            Password = password
        };

        try
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/Auth/login",
                    request);

            if (!response.IsSuccessStatusCode)
            {
                CurrentUser = null;

                await _tokenStorage.ClearSessionAsync();

                AuthenticationStateChanged?.Invoke();

                return false;
            }

            var loginResponse =
                await response.Content
                    .ReadFromJsonAsync<LoginResponse>();

            if (loginResponse == null ||
                string.IsNullOrWhiteSpace(loginResponse.Token))
            {
                CurrentUser = null;

                await _tokenStorage.ClearSessionAsync();

                AuthenticationStateChanged?.Invoke();

                return false;
            }

            CurrentUser = loginResponse;

            await _tokenStorage.SetSessionAsync(loginResponse);

            AuthenticationStateChanged?.Invoke();

            return true;
        }
        catch
        {
            CurrentUser = null;

            await _tokenStorage.ClearSessionAsync();

            AuthenticationStateChanged?.Invoke();

            return false;
        }
    }

    public async Task RestoreSessionAsync()
    {
        var session =
            await _tokenStorage.GetSessionAsync();

        if (session == null ||
            string.IsNullOrWhiteSpace(session.Token))
        {
            CurrentUser = null;

            AuthenticationStateChanged?.Invoke();

            return;
        }

        CurrentUser = session;

        AuthenticationStateChanged?.Invoke();
    }

    public async Task LogoutAsync()
    {
        CurrentUser = null;

        await _tokenStorage.ClearSessionAsync();

        AuthenticationStateChanged?.Invoke();
    }
}