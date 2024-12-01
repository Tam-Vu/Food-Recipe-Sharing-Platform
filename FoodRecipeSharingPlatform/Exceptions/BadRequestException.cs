using System.Net;

namespace FoodRecipeSharingPlatform.Exceptions;
public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
    }
}