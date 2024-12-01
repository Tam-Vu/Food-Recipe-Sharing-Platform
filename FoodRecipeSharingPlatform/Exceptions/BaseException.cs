namespace FoodRecipeSharingPlatform.Exceptions;
[Serializable]
public abstract class BaseException : Exception
{
    public int StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public BaseException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
