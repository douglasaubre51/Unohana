using System.Diagnostics;
using Unohana.Api.Data;
using Unohana.Api.Interfaces;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Api.Repository;
using Unohana.Api.Services.Authentication;
using Unohana.Api.Services.Email;
using Unohana.Api.Services.Otp;

var builder = WebApplication.CreateBuilder(args);

// Built in middlewares
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Authentication
// Add student auth
builder.Services.AddScoped<CreateStudentAccount>();
builder.Services.AddScoped<SignInStudent>();

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
builder.Services.AddTransient<IStudentInfoRepository, StudentInfoRepository>();

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
app.UseAuthorization();
app.MapControllers();

app.Run();
