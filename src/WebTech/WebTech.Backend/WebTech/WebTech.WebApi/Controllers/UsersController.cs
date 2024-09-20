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
    public async Task<Result<User>> Register(RegisterRequestModel registerRequestModel)
    {
        var createUser = new CreateUserDto
        {
            UserName = registerRequestModel.UserName,
            FirstName = registerRequestModel.FirstName,
            LastName = registerRequestModel.LastName,
            Password = registerRequestModel.Password,
            BirthDate = registerRequestModel.BirthDate,
            Address = registerRequestModel.Address
        };

        return await _userService.CreateAsync(createUser);
    }

}