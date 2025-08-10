using System.Diagnostics;
using Unohana.Api.Data;
using Unohana.Api.Interfaces;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Api.Repository;
using Unohana.Api.Services.Authentication;
using Unohana.Api.Services.Email;
using Unohana.Api.Services.Otp;

var builder = WebApplication.CreateBuilder(args);

// Load env vars to Environment class
DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Add student auth
builder.Services.AddScoped<StudentAuthentication>();
// Add otp service
builder.Services.AddScoped<CreateOtp>();
// Add send otp in email service
builder.Services.AddScoped<SendOtpInEmail>();
// Add mongo db configuration
builder.Services.Configure<MongoDbSettings>(
    options =>
    {
        // Get environment variables
        options.ConnectionURI = Environment.GetEnvironmentVariable("ConnectionURI");
        options.DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
        options.StudentCollection = Environment.GetEnvironmentVariable("StudentCollection");
        options.TeacherCollection = Environment.GetEnvironmentVariable("TeacherCollection");
        options.ChannelCollection = Environment.GetEnvironmentVariable("ChannelCollection");
        options.MessageCollection = Environment.GetEnvironmentVariable("MessageCollection");
        options.StudentInfoCollection = Environment.GetEnvironmentVariable("StudentInfoCollection");
        options.TeacherInfoCollection = Environment.GetEnvironmentVariable("TeacherInfoCollection");
    }
);
// Add repositories to the container.
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddTransient<IStudentInfoRepository, StudentInfoRepository>();

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
