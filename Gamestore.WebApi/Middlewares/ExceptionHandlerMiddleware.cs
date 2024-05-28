using System.Text.Json;
using Gamestore.BLL.Exceptions;

namespace Gamestore.WebApi.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
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

        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            context.Response.StatusCode,
            exception.Message,
        }));
    }
}
