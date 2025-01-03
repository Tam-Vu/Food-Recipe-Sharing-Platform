using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;
using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeSharingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
    private readonly IFoodRepository _foodRepository;
    public FoodController(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFood([FromBody] CommandFood commandFood, CancellationToken cancellationToken)
    {
        var response = await _foodRepository.CreateFoodAsync(commandFood, cancellationToken);
        return Ok(Result<ResponseCommand>.CreatedSuccess(response));
    }

    // [HttpGet]
    // public async Task<IActionResult> GetFoodsAllFood(CancellationToken cancellationToken)
    // {
    //     var response = await _foodRepository.GetAllFoodsAsync(cancellationToken);
    //     return Ok(Result<List<ResponseFood>>.CommonSuccess(response));
    // }
    [HttpGet("{id}")]

    [HttpGet("/search")]
    public async Task<IActionResult> GetFoodsByName([FromQuery] string name, CancellationToken cancellationToken)
    {
        var response = await _foodRepository.GetFoodsByNameAsync(name.ToLower(), cancellationToken);
        return Ok(Result<List<ResponseFood>>.CommonSuccess(response));
    }
}