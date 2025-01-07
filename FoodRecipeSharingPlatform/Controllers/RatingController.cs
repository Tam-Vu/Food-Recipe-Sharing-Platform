
using FoodRecipeSharingPlatform.Dtos.RatingDto.CommandRating;
using FoodRecipeSharingPlatform.Dtos.RatingDto.ResponseRating;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeSharingPlatform.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ratingController : ControllerBase
{
    private readonly IRatingRepository _ratingRepository;
    public ratingController(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    [HttpPost("{FoodId}")]
    public async Task<IActionResult> AddRating(Guid FoodId, [FromBody] CommandRating commandRating, CancellationToken cancellationToken)
    {
        var result = await _ratingRepository.AddRating(FoodId, commandRating, cancellationToken);
        return Ok(Result<ResponseCommand>.CreatedSuccess(result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveRating(Guid id, CancellationToken cancellationToken)
    {
        var rating = await _ratingRepository.RemoveRating(id, cancellationToken);
        return Ok(Result<ResponseCommand>.CreatedSuccess(rating));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRating(Guid id, [FromBody] CommandRating commandRating, CancellationToken cancellationToken)
    {
        var result = await _ratingRepository.UpdateRating(id, commandRating, cancellationToken);
        return Ok(Result<ResponseCommand>.CommonSuccess(result));
    }

    [AllowAnonymous]
    [HttpGet("food/{FoodId}")]
    public async Task<IActionResult> GetRatingsByFoodId(Guid FoodId, CancellationToken cancellationToken)
    {
        var result = await _ratingRepository.GetRatingsByFoodId(FoodId, cancellationToken);
        return Ok(Result<List<ResponseRating>>.CommonSuccess(result));
    }

    [AllowAnonymous]
    [HttpGet("food/{FoodId}/rate")]
    public async Task<IActionResult> GetRatingsByFoodIdAndStar(Guid FoodId, [FromQuery] int Star, CancellationToken cancellationToken)
    {
        var result = await _ratingRepository.GetRatingsByFoodIdAndStar(FoodId, Star, cancellationToken);
        return Ok(Result<List<ResponseRating>>.CommonSuccess(result));
    }

    [AllowAnonymous]
    [HttpGet("food/{FoodId}/list")]
    public async Task<IActionResult> GetRatingsByFoodIdOrderByStart(Guid FoodId, [FromQuery] string Order, CancellationToken cancellationToken)
    {
        var result = await _ratingRepository.GetRatingsByFoodIdOrderByStar(FoodId, Order, cancellationToken);
        return Ok(Result<List<ResponseRating>>.CommonSuccess(result));
    }
}