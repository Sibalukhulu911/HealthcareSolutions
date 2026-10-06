using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using HealthcareSolutions.Client;
using HealthcareSolutions.Client.Authentication;
using HealthcareSolutions.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// --------------------------------------------------
// Authorization
// --------------------------------------------------

builder.Services.AddAuthorizationCore();


// --------------------------------------------------
// Authentication / JWT
// --------------------------------------------------

builder.Services.AddScoped<TokenStorage>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<
    AuthenticationStateProvider,
    AuthStateProvider>();

builder.Services.AddScoped<JwtAuthorizationMessageHandler>();


// --------------------------------------------------
// Plain HttpClient
// Used by AuthService for login
// --------------------------------------------------

builder.Services.AddScoped(sp =>
{
    var apiBaseUrl =
        builder.Configuration["ApiBaseUrl"]
        ?? "http://localhost:5292/";

    return new HttpClient
    {
        BaseAddress = new Uri(apiBaseUrl)
    };
});


// --------------------------------------------------
// Authenticated API HttpClient
// JWT token is attached automatically
// --------------------------------------------------

builder.Services.AddHttpClient(
    "ApiClient",
    client =>
    {
        var apiBaseUrl =
            builder.Configuration["ApiBaseUrl"]
            ?? "http://localhost:5292/";

        client.BaseAddress = new Uri(apiBaseUrl);
    })
    .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();


// --------------------------------------------------
// Application API Client
// --------------------------------------------------

builder.Services.AddScoped<ApiHttpClient>();


// --------------------------------------------------
// API Services
// --------------------------------------------------

builder.Services.AddScoped<PatientApiService>();
builder.Services.AddScoped<PractitionerApiService>();
builder.Services.AddScoped<UserApiService>();
builder.Services.AddScoped<AppointmentApiService>();
builder.Services.AddScoped<MedicalRecordApiService>();
builder.Services.AddScoped<VisitNoteApiService>();
builder.Services.AddScoped<MedicationApiService>();
builder.Services.AddScoped<MedicationRequestApiService>();
builder.Services.AddScoped<PrescriptionApiService>();



await builder.Build().RunAsync();