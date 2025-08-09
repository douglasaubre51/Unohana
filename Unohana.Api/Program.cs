using System.Diagnostics;
using Unohana.Api.Data;
using Unohana.Api.Interfaces;
using Unohana.Api.Models.ServiceSettings;
using Unohana.Api.Repository;
using Unohana.Api.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// student auth
builder.Services.AddScoped<StudentAuthentication>();

// add mongo db collections
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbDataString")
);

// Add repositories to the container.

builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();


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
