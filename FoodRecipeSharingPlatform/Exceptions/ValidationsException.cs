using System.Net;

namespace FoodRecipeSharingPlatform.Exceptions;
public class ValidationsException : BaseException
{
    public ValidationsException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
    }
}