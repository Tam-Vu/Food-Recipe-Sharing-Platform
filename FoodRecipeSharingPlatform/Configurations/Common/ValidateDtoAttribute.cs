using FoodRecipeSharingPlatform.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodRecipeSharingPlatform.Configurations.Common;
public class ValidateDtoAttribute : Attribute, IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage).ToArray();
            var listErrors = string.Join(", ", errors);
            throw new ValidationsException(listErrors);
        }
        await next();
    }
}