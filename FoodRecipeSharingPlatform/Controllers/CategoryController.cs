
using FoodRecipeSharingPlatform.Dtos.CategoryDto.CommandCategory;
using FoodRecipeSharingPlatform.Dtos.CategoryDto.ResponseCategory;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeSharingPlatform.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CommandCategory commandCategory, CancellationToken cancellationToken)
    {
        var response = await _categoryRepository.AddAsync(commandCategory, cancellationToken);
        return Ok(Result<ResponseCommand>.CreatedSuccess(response));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CommandCategory commandCategory, CancellationToken cancellationToken)
    {
        var response = await _categoryRepository.UpdateAsync(id, commandCategory, cancellationToken);
        return Ok(Result<ResponseCommand>.CommonSuccess(response));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var response = await _categoryRepository.DeleteByIdAsync(id, cancellationToken);
        return Ok(Result<ResponseCommand>.CommonSuccess(response));
    }

    // [HttpGet]
    // public async Task<IActionResult> GetListCategories(CancellationToken cancellationToken)
    // {
    //     var response = await _categoryRepository.GetAllCategories(cancellationToken);
    //     return Ok(Result<List<ResponseCategory>>.CommonSuccess(response));
    // }
}