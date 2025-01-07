using FoodRecipeSharingPlatform.Enitities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace FoodRecipeSharingPlatform.Middlewares;

public class CustomAuthorizationMiddleware : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Forbidden)
        {
            var statusCode = (int)StatusCodes.Status403Forbidden;
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            const string message = "You are not authorized to access this resource.";
            context.Response.ContentType = "application/json";
            var result = Result<object>.Failure(statusCode, message);
            var jsonResult = System.Text.Json.JsonSerializer.Serialize(result);
            await context.Response.WriteAsync(jsonResult);
            return;
        }
        await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}