using System.Text.Json;
using System.Text.Json.Serialization;
using Application;
using Carter;
using Infrastructure;
using WebApi.Extensions;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Load configurations 
builder.Host.AddConfiguration();

// Configure json serializer
builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    opt.SerializerOptions.WriteIndented = true;
});

// Add options 
builder.Services
    .AddSingleton<ILogger>(sp => sp.GetRequiredService<ILogger<Program>>())
    .AddAppOptions();

// Add semantic kernel and chat services 
builder.Services
    .AddSemanticKernel();

// Add application services
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

builder.Services
    .AddHealthChecks();

builder.Services
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddCarter()
    .AddGlobalExceptionMiddleware();

builder.Services
    .AddCorsPolicy(builder.Configuration);

var app = builder.Build();

// Configure middleware 
app.UseGlobalExceptionMiddleware();

// Configure CORS 
app.UseCors();

// Configure health check
app.MapHealthChecks("/health");

// Configure swagger
app.UseSwagger();
app.UseSwaggerUI();
app.MapWhen(
    ctx => ctx.Request.Path == "/",
    appBuilder => appBuilder.Run(async ctx => await Task.Run(() => ctx.Response.Redirect("/swagger"))));

app.MapCarter();

app.Run();

