using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyTaskList.Application.Abstractions.Ports.Driven;
using MyTaskList.Application.Abstractions.Ports.Driver;
using MyTaskList.Application.Services;
using MyTaskList.Persistence;
using MyTaskList.Persistence.Repositories;
using MyTaskList.WebApi.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddUserSecrets<Program>();

builder.Services.Configure<ConnectionStringOptions>(
    builder.Configuration.GetSection(ConnectionStringOptions.ConnectionStrings));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddDbContext<MyTaskListDbContext>((services, options) =>
{
    var connectionStrings = services.GetRequiredService<IOptions<ConnectionStringOptions>>();
    options.UseSqlServer(
        connectionStrings.Value.AZURE_SQL_CONNECTIONSTRING, 
        x => x.EnableRetryOnFailure());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}