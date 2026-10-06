using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using HealthcareSolutions.Client.Services;

namespace HealthcareSolutions.Client.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;

    public AuthStateProvider(AuthService authService)
    {
        _authService = authService;

        _authService.AuthenticationStateChanged +=
            OnAuthenticationStateChanged;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var currentUser = _authService.CurrentUser;

        if (currentUser == null ||
            string.IsNullOrWhiteSpace(currentUser.Token))
        {
            var anonymous =
                new ClaimsPrincipal(
                    new ClaimsIdentity());

            return Task.FromResult(
                new AuthenticationState(anonymous));
        }

        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.Name,
                currentUser.Username),

            new Claim(
                ClaimTypes.NameIdentifier,
                currentUser.UserId.ToString()),

            new Claim(
                ClaimTypes.Role,
                currentUser.Role)
        };

        var identity =
            new ClaimsIdentity(
                claims,
                authenticationType: "jwt");

        var user =
            new ClaimsPrincipal(identity);

        return Task.FromResult(
            new AuthenticationState(user));
    }

    private void OnAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(
            GetAuthenticationStateAsync());
    }
}