
using AutoMapper;
using FoodRecipeSharingPlatform.Dtos.IngredientDto.CommandIngredient;
using FoodRecipeSharingPlatform.Dtos.IngredientDto.ResposeIngredient;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeSharingPlatform.Controllers;
[ApiController]
[Route("api/[controller]")]
public class IngredientController : ControllerBase
{
    private readonly IIngredientRepository _ingredientRepository;
    public IngredientController(IIngredientRepository ingredientRepository, IMapper mapper)
    {
        _ingredientRepository = ingredientRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddIngredient([FromBody] CommandIngredient commandGredient, CancellationToken cancellationToken)
    {
        var result = await _ingredientRepository.AddIngredient(commandGredient, cancellationToken);
        return Ok(Result<ResponseCommand>.CreatedSuccess(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllIngredients(CancellationToken cancellationToken)
    {
        var result = await _ingredientRepository.GetAllIngredients(cancellationToken);
        return Ok(Result<List<ResposeIngredient>>.CommonSuccess(result));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] CommandIngredient commandIngredient, CancellationToken cancellationToken)
    {
        var result = await _ingredientRepository.UpdateIngredient(id, commandIngredient, cancellationToken);
        return Ok(Result<ResponseCommand>.CommonSuccess(result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIngredient(Guid id, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientRepository.DeleteIngredient(id, cancellationToken);
        return Ok(Result<ResponseCommand>.CreatedSuccess(ingredient));
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> FindIngredientsByName(string name, CancellationToken cancellationToken)
    {
        var ingredients = await _ingredientRepository.GetAllIngredientsByName(name, cancellationToken);
        return Ok(Result<List<ResposeIngredient>>.CommonSuccess(ingredients));
    }

}