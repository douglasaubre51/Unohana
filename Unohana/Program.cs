using Unohana.Components;
using Unohana.Services.OtpService;
using Unohana.Services.StorageService;
using Unohana.Services.VerificationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddTransient<StudentVerification>();
// Add Storage service
builder.Services.AddScoped<StudentInfoStorage>();
// Add Request otp service
builder.Services.AddTransient<RequestOtp>();
// Add Request otp verification service
builder.Services.AddTransient<RequestOtpVerification>();

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

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Unohana.Client._Imports).Assembly);

app.Run();
