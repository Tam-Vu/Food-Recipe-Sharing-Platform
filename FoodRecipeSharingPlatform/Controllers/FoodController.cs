using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;
using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeSharingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class foodController : ControllerBase
{
    private readonly IFoodRepository _foodRepository;
    public foodController(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    [Authorize]
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
    public async Task<IActionResult> GetFoodById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _foodRepository.GetFoodByIdAsync(id, cancellationToken);
        return Ok(Result<ResponseFood>.CommonSuccess(response));
    }

    [HttpGet("/search")]
    public async Task<IActionResult> GetFoodsByName([FromQuery] string name, CancellationToken cancellationToken)
    {
        var response = await _foodRepository.GetFoodsByNameAsync(name.ToLower(), cancellationToken);
        return Ok(Result<List<ResponseFood>>.CommonSuccess(response));
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFood(Guid id, [FromBody] CommandFood commandFood, CancellationToken cancellationToken)
    {
        var response = await _foodRepository.UpdateFoodAsync(id, commandFood, cancellationToken);
        return Ok(Result<ResponseCommand>.CommonSuccess(response));
    }

    [HttpGet("/category/{id}")]
    public async Task<IActionResult> GetFoodsByCategory(Guid id, CancellationToken cancellationToken)
    {
        var response = await _foodRepository.GetFoodsByCategory(id, cancellationToken);
        return Ok(Result<List<ResponseFood>>.CommonSuccess(response));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFood(Guid id, CancellationToken cancellationToken)
    {
        var response = await _foodRepository.DeleteFoodAsync(id, cancellationToken);
        return Ok(Result<ResponseCommand>.CommonSuccess(response));
    }

    [Authorize]
    [HttpGet("/my-foods")]
    public async Task<IActionResult> GetFoodsOfCurrentUser(CancellationToken cancellationToken)
    {
        var response = await _foodRepository.GetFoodsOfCurrentUser(cancellationToken);
        return Ok(Result<List<ResponseFood>>.CommonSuccess(response));
    }

    [HttpGet("/user/{id}")]
    public async Task<IActionResult> GetFoodsByUserId(Guid id, CancellationToken cancellationToken)
    {
        var response = await _foodRepository.GetFoodsByUserId(id, cancellationToken);
        return Ok(Result<List<ResponseFood>>.CommonSuccess(response));
    }
}