using System.Diagnostics;
using System.Text;

namespace Gamestore.WebApi.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        context.Request.EnableBuffering();
        var requestContent = await ReadRequestBodyAsync(context.Request);

        var originalResponseBodyStream = context.Response.Body;

        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await next(context);

        stopwatch.Stop();

        var responseContent = await ReadResponseBodyAsync(responseBodyStream);

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

        logger.LogInformation(
            "Request: {Request}\n Response: {Response}\n Ip Address: {IPAddress}\n URL: {URL}\n Status code: {StatusCode}\n Elapsed time: {ElapsedTime} ms\n",
            logDetails.RequestContent,
            logDetails.ResponseContent,
            logDetails.IpAddress,
            logDetails.Url,
            logDetails.StatusCode,
            logDetails.ElapsedTime);

        await responseBodyStream.CopyToAsync(originalResponseBodyStream);
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.Body.Position = 0;
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
