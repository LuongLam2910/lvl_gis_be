using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace App.Core.Common;

public class RequestInputLoggingMiddleware
{
    private readonly ILogger<RequestInputLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public RequestInputLoggingMiddleware(RequestDelegate next, ILogger<RequestInputLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var injectedRequestStream = new MemoryStream();
        try
        {
            var bodyLog = string.Empty;
            if (context.Request.ContentType != null &&
                !context.Request.ContentType.StartsWith("multipart/form-data; boundary="))
                using (var bodyReader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    var bodyAsText = await bodyReader.ReadToEndAsync();
                    if (string.IsNullOrWhiteSpace(bodyAsText) == false) bodyLog += bodyAsText;

                    var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                    injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                    injectedRequestStream.Seek(0, SeekOrigin.Begin);
                    context.Request.Body = injectedRequestStream;
                }

            _logger.LogInformation($"IP Address : {context.Connection.RemoteIpAddress.MapToIPv4()}");
            _logger.LogInformation($"REQUEST BODY: \n{bodyLog}");
            _logger.LogInformation($"REQUEST PATH: {context.Request.Path}");
            _logger.LogInformation(
                $"REQUEST HEADERS: \n{string.Join("\n", context.Request.Headers.Select(t => $"{t.Key}: {SensitiveDataRedacted.HeaderRedactedString(t.Key, t.Value)}"))}");
            _logger.LogInformation(
                $"REQUEST QUERIES: \n{string.Join("\n", context.Request.Query.Select(t => $"{t.Key}: {t.Value}"))}");
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
        }
        finally
        {
            injectedRequestStream.Dispose();
        }
    }
}

public class SensitiveDataRedacted
{
    private static readonly string[] SensitiveHeaders =
    {
        "AUTHORIZATION",
        "COOKIE",
        "SET-COOKIE"
    };

    private static readonly string _redactedString = "****REDACTED****";

    public static string HeaderRedactedString(string key, string value)
    {
        return SensitiveHeaders.Contains(key.Trim().ToUpper())
            ? _redactedString
            : value;
    }
}