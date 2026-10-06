using System.Net.Http.Headers;
using HealthcareSolutions.Client.Services;

namespace HealthcareSolutions.Client.Authentication;

public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly TokenStorage _tokenStorage;

    public JwtAuthorizationMessageHandler(
        TokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token =
            await _tokenStorage.GetTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);
        }

        return await base.SendAsync(
            request,
            cancellationToken);
    }
}