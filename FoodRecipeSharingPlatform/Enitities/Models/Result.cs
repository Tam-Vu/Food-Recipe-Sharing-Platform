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
    public static Result<TEntity> CommonSuccess(object data)
    {
        return new Result<TEntity> { StatusCode = 200, Data = data, Message = "Command execute successfully" };
    }

    public static Result<TEntity> CreatedSuccess(TEntity data)
    {
        return new Result<TEntity>(201, data!, "Command executed successfully");
    }
    public static Result<TEntity> Failure(int statusCode, string message)
    {
        return new Result<TEntity> { StatusCode = statusCode, Message = message, Data = null };
    }
}