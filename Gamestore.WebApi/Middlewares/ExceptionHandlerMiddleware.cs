using System.Text.Json;
using Gamestore.BLL.Exceptions;
using Gamestore.WebApi.Helpers;

namespace Gamestore.WebApi.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogException(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        if (exception is ArgumentException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else if (exception is GamestoreException or InvalidOperationException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }

        return context.Response.WriteAsync(CreateResponseMessage(context, exception));
    }

    private static string CreateResponseMessage(HttpContext context, Exception exception)
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
            ? CreateDevelopmentMessage(context, exception)
            : CreateProductionMessage(context, exception);
    }

    private static string CreateProductionMessage(HttpContext context, Exception exception)
    {
        return JsonSerializer.Serialize(new
        {
            context.Response.StatusCode,
            exception.Message,
        });
    }

    private static string CreateDevelopmentMessage(HttpContext context, Exception exception)
    {
        var errorDetails = new
        {
            context.Response.StatusCode,
            exception.Message,
            exception.StackTrace,
            exception.Data,
        };

        return JsonSerializer.Serialize(errorDetails);
    }
}
