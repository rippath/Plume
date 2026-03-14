using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.FileProviders;
using MudBlazor.Services;
using Plume.Application;
using Plume.Identity;
using Plume.Persistence;
using Plume.UI.Client.Auth;
using Plume.UI.Client.Contracts.Local;
using Plume.UI.Client.Pages;
using Plume.UI.Components;
using Plume.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add Persistence (DbContexts + repositories)
builder.Services.AddPersistence(connectionString);

// Add Application services
builder.Services.AddApplication();

// Add Identity services (includes authentication and authorization)
builder.Services.AddIdentityServices(builder.Configuration);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add HTTP client for API calls
builder.Services.AddHttpClient();

// Add controllers for API endpoints
builder.Services.AddControllers();

// Add HttpContextAccessor for server-side auth
builder.Services.AddHttpContextAccessor();

// Add server-side auth services for prerendering
builder.Services.AddScoped<IClientAuthService, ServerAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthStateProvider>();

// Add server-side theme preference service (reads darkMode cookie from HttpContext)
builder.Services.AddScoped<IThemePreferenceService, ServerThemePreferenceService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Authentication & Authorization middleware (must be before endpoints)
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

// Serve user-uploaded files from outside wwwroot so the dev file-watcher
// doesn't trigger a hot-reload on every upload.
var uploadsRoot = Path.Combine(builder.Environment.ContentRootPath, "uploads");
Directory.CreateDirectory(uploadsRoot);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsRoot),
    RequestPath = "/uploads"
});

// Map API controllers
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Plume.UI.Client._Imports).Assembly);

app.Run();
