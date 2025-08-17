using Unohana.Components;
using Unohana.Services;
using Unohana.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpContextAccessor();

// Give an HttpClient for each user!
builder.Services.AddScoped(sp =>
{
    var handler = new HttpClientHandler
    {
        UseCookies = true,
        CookieContainer = new System.Net.CookieContainer()
    };

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7031/")
    };
});

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Unohana.Client._Imports).Assembly);

app.Run();
