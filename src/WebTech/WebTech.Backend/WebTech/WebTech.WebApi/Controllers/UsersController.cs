using Microsoft.AspNetCore.Mvc;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.WebApi.Models.Authentication;

namespace WebTech.WebApi.Controllers;

[Host("bg-local.com")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        return Ok("Hello from bg-local.com");
    }
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<User>), StatusCodes.Status400BadRequest)]
    public async Task<Result<User>> Register(CreateUserDto registerRequestModel)
        => await _userService.CreateAsync(registerRequestModel);

}