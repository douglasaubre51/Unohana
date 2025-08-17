using Unohana.Components;
using Unohana.Services;
using Unohana.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Give an HttpClient for each user!
builder.Services.AddScoped<HttpClient>();

// user id verification service
builder.Services.AddScoped<VerificationService>();

// Add user storage service
builder.Services.AddScoped<UserStorage>();

// Add otp service
builder.Services.AddScoped<OtpService>();

// Add StudentAuthentication service
builder.Services.AddScoped<Student>();

// Add Teacher Authentication service
builder.Services.AddScoped<Teacher>();

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
