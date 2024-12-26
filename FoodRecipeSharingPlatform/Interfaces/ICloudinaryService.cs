namespace FoodRecipeSharingPlatform.Interfaces;

public interface ICloudinaryService
{
    Task<string[]> UploadImageAsync(List<IFormFile> images);
    Task<string> DeleteImageAsync(List<string> publicIds);
}