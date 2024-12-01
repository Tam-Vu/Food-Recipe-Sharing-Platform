namespace FoodRecipeSharingPlatform.Enitities.Models;
public class Result<TEntity>
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    public Result<TEntity> Success(int statusCode, object data, string message)
    {
        return new Result<TEntity> { StatusCode = statusCode, Data = data, Message = message };
    }

    public Result<TEntity> Failure(int statusCode, string message)
    {
        return new Result<TEntity> { StatusCode = statusCode, Message = message, Data = null };
    }
}