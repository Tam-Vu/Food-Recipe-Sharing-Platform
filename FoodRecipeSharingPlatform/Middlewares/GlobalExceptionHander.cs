using System.Net;
using System.Text.Json;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace FoodRecipeSharingPlatform.Middlewares;

public class GlobalExceptionHander : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHander> _logger;
    public GlobalExceptionHander(ILogger<GlobalExceptionHander> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
        if (exception is BaseException baseException)
        {
            var Response = new Result<string>();
            Response.Failure(baseException.StatusCode, baseException.Message);
            var result = JsonSerializer.Serialize(Response);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = baseException.StatusCode;
            await httpContext.Response.WriteAsync(result);
            return true;
        }
        else
        {
            var Response = new Result<string>();
            Response.Failure((int)HttpStatusCode.InternalServerError, "Internal Server Error");
            var result = JsonSerializer.Serialize(Response);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync(result);
        }
        return true;
    }
}