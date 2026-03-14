using System.Net;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Plume.UI.Client.Auth;

public class UnauthorizedHandler : DelegatingHandler
{
    private readonly NavigationManager _navigation;
    private readonly ILocalStorageService _localStorage;
    private readonly JwtAuthenticationStateProvider _authStateProvider;

    private const string AccessTokenKey = "accessToken";
    private const string AccessTokenExpirationKey = "accessTokenExpiration";

    public UnauthorizedHandler(
        NavigationManager navigation,
        ILocalStorageService localStorage,
        JwtAuthenticationStateProvider authStateProvider)
    {
        _navigation = navigation;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await _localStorage.RemoveItemAsync(AccessTokenKey);
            await _localStorage.RemoveItemAsync(AccessTokenExpirationKey);
            _authStateProvider.NotifyAuthenticationStateChanged();

            var returnUrl = Uri.EscapeDataString(_navigation.Uri);
            _navigation.NavigateTo($"/login?returnUrl={returnUrl}");
        }

        return response;
    }
}
