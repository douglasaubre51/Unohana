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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
