using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FoodRecipeSharingPlatform.Configurations.Binding;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;

namespace FoodRecipeSharingPlatform.Services.Common;

public class CloudinaryService : ICloudinaryService
{
    private readonly CloudinaryAccount _cloudinaryAccount;
    private readonly Cloudinary _cloudinary;
    public CloudinaryService(CloudinaryAccount cloudinaryAccount)
    {
        _cloudinaryAccount = cloudinaryAccount;
        _cloudinary = new Cloudinary(new Account(
            _cloudinaryAccount.CloudName,
            _cloudinaryAccount.ApiKey,
            _cloudinaryAccount.ApiSecret
        ));
    }
    public async Task<string> DeleteImageAsync(List<string> publicIds)
    {
        try
        {
            foreach (var publicId in publicIds)
            {
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);
            }
            return "Images deleted successfully";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Failed to delete image, please try later");
        }
    }

    public async Task<string[]> UploadImageAsync(List<IFormFile> images)
    {
        try
        {
            var urls = new List<string>();
            foreach (var image in images)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                    UseFilename = true,
                    Overwrite = true,
                    Folder = "recipe"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                urls.Add(uploadResult.Url.ToString());
            }
            return [.. urls];
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Failed to upload image, please try later");
        }
    }
}