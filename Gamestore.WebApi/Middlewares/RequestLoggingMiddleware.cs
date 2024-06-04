using System.Diagnostics;
using System.Text;

namespace Gamestore.WebApi.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        context.Request.EnableBuffering();
        var requestContent = await ReadRequestBodyAsync(context.Request);
        context.Request.Body.Position = 0;

        var originalResponseBodyStream = context.Response.Body;

        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await _next(context);

        stopwatch.Stop();

        var responseContent = await ReadResponseBodyAsync(responseBodyStream);
        context.Response.Body.Position = 0;

        // Log the details
        var logDetails = new
        {
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            Url = context.Request.Path,
            context.Response.StatusCode,
            RequestContent = requestContent,
            ResponseContent = responseContent,
            ElapsedTime = stopwatch.ElapsedMilliseconds,
        };

        _logger.LogInformation($"Request: {logDetails.RequestContent}\n" +
                               $"Response: {logDetails.ResponseContent}\n" +
                               $"IP Address: {logDetails.IpAddress}\n" +
                               $"URL: {logDetails.Url}\n" +
                               $"Status Code: {logDetails.StatusCode}\n" +
                               $"Elapsed Time: {logDetails.ElapsedTime} ms");

        await responseBodyStream.CopyToAsync(originalResponseBodyStream);
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return body;
    }

    private static async Task<string> ReadResponseBodyAsync(Stream responseBody)
    {
        responseBody.Position = 0;
        using var reader = new StreamReader(responseBody, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        responseBody.Position = 0;
        return body;
    }
}
