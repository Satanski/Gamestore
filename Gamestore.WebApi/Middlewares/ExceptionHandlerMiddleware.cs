using System.Text;
using Gamestore.BLL.Exceptions;
using Gamestore.WebApi.Helpers;

namespace Gamestore.WebApi.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger = logger;

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

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(ExceptionLogHelpers.CreateLogMessage(exception));

        context.Response.ContentType = "text/html";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        if (exception is ArgumentException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else if (exception is GamestoreException or InvalidOperationException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }

        return context.Response.WriteAsync(CreateresponseMessage(context, exception));
    }

    private static string CreateresponseMessage(HttpContext context, Exception exception)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<!DOCTYPE html><html><body><center>");
        sb.Append("<h2>There was an error processing Your request</h2>");
        sb.Append($"Status code: {context.Response.StatusCode}<br>");
        sb.Append($"{exception.Message}");
        sb.Append("</center></body></html>");
        return sb.ToString();
    }
}
