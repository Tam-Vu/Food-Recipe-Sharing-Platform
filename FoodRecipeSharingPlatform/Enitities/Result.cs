namespace FoodRecipeSharingPlatform.Enitities
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string[] Message { get; set; }
        public object Data { get; set; }

        public Result Success(object data)
        {
            return new Result { IsSuccess = true, Data = data, Message = new string[] { "Success" } };
        }
    }
}