using System.Collections;
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
    public async Task<Result<IEnumerable<User>>> Get()
    {
        return await _userService.GetAsync();
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

    [HttpPost("login")]
    public async Task<Result<None>> Login(LoginRequestModel loginRequestModel)
    {
        var existingUserResult = await _userService.GetExistingUser(loginRequestModel.UserName, loginRequestModel.Password);
        
        if (!existingUserResult.IsSucceed)
        {
            return Result<None>.Error(existingUserResult.ErrorCode);
        }
        
        return Result<None>.Success();
    }
}