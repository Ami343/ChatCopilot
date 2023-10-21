using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middlewares;

public class GlobalExceptionsMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionsMiddleware> _logger;

    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    

    public GlobalExceptionsMiddleware(ILogger<GlobalExceptionsMiddleware> logger)
    {
        _logger = logger;
        
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            _logger.LogError("Unknown error occured: {message}, error: {error}", e.Message, e);

            context.Response.StatusCode = (int)ErrorResult.GenericError.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(ErrorResult.GenericError,_jsonOptions));
        }
    }
}

public static class GlobalExceptionsMiddlewareExtensions
{
    public static IServiceCollection AddGlobalExceptionMiddleware(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionsMiddleware>();

        return services;
    }

    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionsMiddleware>();

        return app;
    }
}