using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeSharingPlatform.Controllers;
[ApiController]
[Route("api/[controller]")]
public class cloudinaryController : ControllerBase
{
    private readonly ICloudinaryService _cloudinaryService;
    public cloudinaryController(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> images)
    {
        var result = await _cloudinaryService.UploadImageAsync(images);
        return Ok(Result<string[]>.CommonSuccess(result));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteImage([FromBody] List<string> publicIds)
    {
        var result = await _cloudinaryService.DeleteImageAsync(publicIds);
        return Ok(Result<string[]>.CommonSuccess(result));
    }
}