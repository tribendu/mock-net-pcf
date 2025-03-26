using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MockNetPcf.Api.Services;
using MockNetPcf.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register WireMock configuration
builder.Services.Configure<WireMockConfig>(
    builder.Configuration.GetSection("WireMock"));

// Register services
builder.Services.AddSingleton<IMockService, MockService>();
builder.Services.AddSingleton<IRecordingService, RecordingService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Mock Service API", 
        Version = "v1",
        Description = "API for managing mock services and recording with WireMock.NET"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable Swagger UI for all environments
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mock Service API v1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the root
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
