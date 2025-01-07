using FoodRecipeSharingPlatform.Dtos.UserDto.CommnadUser;
using FoodRecipeSharingPlatform.Dtos.UserDto.ResponseUser;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeSharingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class userController : ControllerBase
{
    private readonly IUserServiceRepository _userServiceRepository;
    public userController(IUserServiceRepository userServiceRepository)
    {
        _userServiceRepository = userServiceRepository;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUserDto()
    {
        var user = await _userServiceRepository.GetCurrentUserDto();
        return Ok(Result<ResponseUser>.CommonSuccess(user!));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _userServiceRepository.GetAllUser();
        return Ok(Result<List<ResponseUser>>.CommonSuccess(users));
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateUser([FromBody] CommandUser commandUser, CancellationToken cancellationToken)
    {
        var result = await _userServiceRepository.UpdateUser(commandUser);
        return Ok(Result<ResponseCommand>.CommonSuccess(result));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveUser(Guid id, CancellationToken cancellationToken)
    {
        var result = await _userServiceRepository.RemoveUser(id);
        return Ok(Result<ResponseCommand>.CommonSuccess(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userServiceRepository.GetUserDtoById(id);
        return Ok(Result<ResponseUser>.CommonSuccess(user!));
    }
}