namespace FoodRecipeSharingPlatform.Enitities.Models;
public class Result<TEntity>
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    private Result() { }
    private Result(int statusCode, object data, string message)
    {
        this.StatusCode = statusCode;
        this.Data = data;
        this.Message = message;
    }
    public Result<TEntity> CommonSuccess(int statusCode, object data, string message)
    {
        return new Result<TEntity> { StatusCode = statusCode, Data = data, Message = message };
    }

    public static Result<TEntity> CreatedSuccess(TEntity data)
    {
        return new Result<TEntity>(201, data!, $"Created {typeof(TEntity).Name} successfully");
    }
    public static Result<TEntity> Failure(int statusCode, string message)
    {
        return new Result<TEntity> { StatusCode = statusCode, Message = message, Data = null };
    }
}