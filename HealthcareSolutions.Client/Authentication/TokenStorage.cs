using System.Text.Json;
using Microsoft.JSInterop;
using HealthcareSolutions.Client.Models;

namespace HealthcareSolutions.Client.Services;

public class TokenStorage
{
    private readonly IJSRuntime _jsRuntime;

    private const string SessionKey = "healthcare_session";

    public TokenStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<LoginResponse?> GetSessionAsync()
    {
        var json =
            await _jsRuntime.InvokeAsync<string?>(
                "sessionStorage.getItem",
                SessionKey);

        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<LoginResponse>(json);
        }
        catch
        {
            return null;
        }
    }

    public async Task SetSessionAsync(
        LoginResponse session)
    {
        var json =
            JsonSerializer.Serialize(session);

        await _jsRuntime.InvokeVoidAsync(
            "sessionStorage.setItem",
            SessionKey,
            json);
    }

    public async Task ClearSessionAsync()
    {
        await _jsRuntime.InvokeVoidAsync(
            "sessionStorage.removeItem",
            SessionKey);
    }

    public async Task<string?> GetTokenAsync()
    {
        var session =
            await GetSessionAsync();

        return session?.Token;
    }

    public async Task<bool> HasTokenAsync()
    {
        var token =
            await GetTokenAsync();

        return !string.IsNullOrWhiteSpace(token);
    }
}