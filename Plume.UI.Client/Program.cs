using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Plume.UI.Client.Auth;
using Plume.UI.Client.Contracts.Local;
using Plume.UI.Client.Services.Local;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add local storage
builder.Services.AddBlazoredLocalStorage();

// Add HttpClient with 401 → login redirect handler
builder.Services.AddScoped<UnauthorizedHandler>();
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<UnauthorizedHandler>();
    handler.InnerHandler = new HttpClientHandler();
    return new HttpClient(handler)
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    };
});

// Add WASM-side theme preference service (reads darkMode cookie via JS)
builder.Services.AddScoped<IThemePreferenceService, ClientThemePreferenceService>();

// Add authentication services
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthenticationStateProvider>());
builder.Services.AddScoped<IClientAuthService, ClientAuthService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
