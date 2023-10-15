using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Load configurations 
builder.Host.AddConfiguration();

// Add options 
builder.Services
    .AddSingleton<ILogger>(sp => sp.GetRequiredService<ILogger<Program>>())
    .AddAppOptions();

// Add semantic kernel services 
builder.Services.AddSemanticKernel();

// Add rest services
builder.Services
    .AddSwaggerGen()
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure endpoints
app.MapControllers();
app.MapHealthChecks("/health");

// Configure swagger
app.UseSwagger();
app.UseSwaggerUI();
app.MapWhen(
    ctx => ctx.Request.Path == "/",
    appBuilder => appBuilder.Run(async ctx => await Task.Run(() => ctx.Response.Redirect("/swagger"))));

app.Run();

