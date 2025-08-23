using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text;
using Unohana.Api.Data;
using Unohana.Api.Helpers;
using Unohana.Api.Interfaces;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Api.Repository;
using Unohana.Api.Services.Email;
using Unohana.Api.Services.Otp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// Load env vars to Environment class
DotNetEnv.Env.Load();
// Add mongo db configuration
builder.Services.Configure<MongoDbSettings>(
    options =>
    {
        // Get environment variables
        options.ConnectionURI = Environment.GetEnvironmentVariable(
            "ConnectionURI"
        );
        options.DatabaseName = Environment.GetEnvironmentVariable(
            "DatabaseName"
        );
        options.StudentCollection = Environment.GetEnvironmentVariable(
"StudentCollection"
        );
        options.TeacherCollection = Environment.GetEnvironmentVariable(
"TeacherCollection"
        );
        options.ChannelCollection = Environment.GetEnvironmentVariable(
"ChannelCollection"
        );
        options.MessageCollection = Environment.GetEnvironmentVariable(
"MessageCollection"
        );
        options.StudentInfoCollection = Environment.GetEnvironmentVariable(
"StudentInfoCollection"
        );
        options.TeacherInfoCollection = Environment.GetEnvironmentVariable(
"TeacherInfoCollection"
        );
        options.OtpTemporaryCache = Environment.GetEnvironmentVariable(
"OtpTemporaryCache"
        );
    }
);
// Add mongodb context!
builder.Services.AddSingleton<MongoDbContext>();

// Otp
// Add create otp service
builder.Services.AddScoped<CreateOtp>();
// Add save otp service
builder.Services.AddScoped<SaveOtp>();
// Add verify otp service
builder.Services.AddScoped<VerifyOtp>();
// Add send otp in email service
builder.Services.AddScoped<SendOtpInEmail>();

// Add repositories to the container.
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
// Add csv models info repositories
builder.Services.AddScoped<IStudentInfoRepository, StudentInfoRepository>();
builder.Services.AddScoped<ITeacherInfoRepository, TeacherInfoRepository>();

// Add Jwt token creator service
builder.Services.AddScoped<JwtCreater>();

// Add jwt config
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "localhost",
            ValidAudience = "localhost",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
                    )
                )
        };
    });

var app = builder.Build();

// Run seed data methods
if (args.Length == 1 && args[0].ToLower() == "seed-student-info")
{
    Debug.WriteLine("triggered seeding");
    SeedStudentInfo.SeedCSVData(app);
    return;
}
if (args.Length == 1 && args[0].ToLower() == "seed-teacher-info")
{
    SeedTeacherInfo.SeedCSVData(app);
    return;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("UnohanaBackendAccess");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
