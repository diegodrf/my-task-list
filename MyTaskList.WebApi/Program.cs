using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyTaskList.Application.Abstractions.Ports.Driven;
using MyTaskList.Application.Abstractions.Ports.Driver;
using MyTaskList.Application.Entities;
using MyTaskList.Application.Services;
using MyTaskList.Persistence;
using MyTaskList.Persistence.Repositories;
using MyTaskList.WebApi.Options;
using Scalar.AspNetCore;

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
app.MapOpenApi();
app.MapScalarApiReference("/swagger", x =>
{
    x.WithDarkMode(false);
    x.WithTheme(ScalarTheme.Alternate);
});

var v1 = app.MapGroup("/v1");
var itemsRoute = v1.MapGroup("/items");
itemsRoute.MapGet("/", async Task<List<Item>> (IItemService service, CancellationToken cancellationToken) =>
{
    var items = await service.GetAllAsync(cancellationToken);
    return items;
});
itemsRoute.MapGet("/{id}", async Task<Results<Ok<Item>, NotFound>> (Guid id, IItemService service, CancellationToken cancellationToken) =>
{
    var item = await service.GetByIdAsync(id, cancellationToken);
    if (item is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(item);
});

app.Run();