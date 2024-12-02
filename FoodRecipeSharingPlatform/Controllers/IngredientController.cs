
using FoodRecipeSharingPlatform.Dtos.Gredient.CommandGredient;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeSharingPlatform.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IngredientController : ControllerBase
{
    private readonly IIngredientRepository _ingredientRepository;
    public IngredientController(IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddIngredient([FromBody] CommandGredient commandGredient, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ingredientRepository.AddAsync(commandGredient, cancellationToken);
            return Ok(Result<ResponseCommand>.CreatedSuccess(result));
        }
        catch (Exception error)
        {
            throw new BadRequestException(error.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllIngredients(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ingredientRepository.GetAllAsync(cancellationToken);
            return Ok(Result<List<CommandGredient>>.CreatedSuccess(result));
        }
        catch (Exception error)
        {
            throw new BadRequestException(error.Message);
        }
    }
}