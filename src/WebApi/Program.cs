using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfiguration();

builder.Services.AddSingleton<ILogger>(sp => sp.GetRequiredService<ILogger<Program>>());

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

app.MapControllers();
app.MapHealthChecks("/health");


app.UseSwagger();
app.UseSwaggerUI();

app.MapWhen(
    ctx => ctx.Request.Path == "/",
    appBuilder => appBuilder.Run(async ctx => await Task.Run(() => ctx.Response.Redirect("/swagger"))));

app.Run();

