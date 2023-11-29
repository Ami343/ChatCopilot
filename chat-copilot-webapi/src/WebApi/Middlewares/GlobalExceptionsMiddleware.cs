using System.Net;
using System.Text.Json;
using Application.Common;
using Application.Common.Exceptions;

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

            await HandleException(context, e);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var (statusCode, error) = GetStatusCodeAndError(exception);

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(error, _jsonOptions));
    }

    private static (HttpStatusCode httpStatusCode, ErrorResult error) GetStatusCodeAndError(
        Exception exception) =>
        exception switch
        {
            ValidationResultException validationException => (HttpStatusCode.BadRequest, validationException.Error),
            _ => (HttpStatusCode.InternalServerError, ErrorResult.GenericError)
        };
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