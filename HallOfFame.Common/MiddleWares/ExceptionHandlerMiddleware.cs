using System.Diagnostics;
using System.Net;
using HallOfFame.Common.Exceptions;
using HallOfFame.Common.Other;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HallOfFame.Common.MiddleWares;

public class ExceptionHandlerMiddleware {
     private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger) {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Handle exception
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        }
        catch (NotFoundException ex) {
            var errorDetails = new ErrorDetails {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = ex.Message,
                TraceId = Activity.Current?.Id ?? context.TraceIdentifier
            };
            _logger.LogError(ex, "{Message}", errorDetails.ToString());
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorDetails.ToString());
        }
        catch (Exception ex) {
            var errorDetails = new ErrorDetails {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Something went wrong",
                TraceId = Activity.Current?.Id ?? context.TraceIdentifier
            };
            _logger.LogError(ex, "{Message}", errorDetails.ToString());
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}

/// <summary>
/// Exception handler middleware extension
/// </summary>
public static class ExceptionHandlerMiddlewareExtension {
    /// <summary>
    /// Extension method
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseErrorHandleMiddleware(this IApplicationBuilder builder) {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}