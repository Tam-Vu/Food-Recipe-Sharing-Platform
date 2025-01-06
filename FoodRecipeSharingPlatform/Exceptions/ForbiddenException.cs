using System.Net;

namespace FoodRecipeSharingPlatform.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.Forbidden;
    }
}