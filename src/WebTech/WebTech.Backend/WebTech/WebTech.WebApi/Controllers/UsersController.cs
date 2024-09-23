using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.Domain.Enums;
using WebTech.WebApi.Models.Authentication;

namespace WebTech.WebApi.Controllers;

[Host("bg-local.com")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;

    private readonly IJwtProvider _jwtProvider;

    private readonly IRefreshTokenSessionService _refreshTokenSessionService;

    private readonly ICookiesProvider _cookiesProvider;
    public UsersController(IUserService userService, IJwtProvider jwtProvider, IRefreshTokenSessionService refreshTokenSessionService, ICookiesProvider cookiesProvider)
    {
        _userService = userService;
        
        _jwtProvider = jwtProvider;
        
        _refreshTokenSessionService = refreshTokenSessionService;
        
        _cookiesProvider = cookiesProvider;
    }

    [HttpGet("get")]
    public async Task<Result<IEnumerable<User>>> Get()
    {
        return await _userService.GetAsync();
    }
    
    [Authorize]
    [HttpGet("me")]
    public async Task<Result<User>> GetCurrent()
    {
        return await _userService.GetCurrentAsync(GetUserId());
    }


    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<User>), StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status400BadRequest)]
    public async Task<Result<None>> Login(LoginRequestModel loginRequestModel)
    {
        var existingUserResult = await _userService.GetExistingUser(loginRequestModel.UserName, loginRequestModel.Password);
        
        if (!existingUserResult.IsSucceed)
        {
            return Result<None>.Error(existingUserResult.ErrorCode);
        }

        var user = existingUserResult.Data;

        var accessToken = _jwtProvider.GenerateToken(user.Id, JwtTokenType.Access);
        var refreshToken = _jwtProvider.GenerateToken(user.Id, JwtTokenType.Refresh);

        await _refreshTokenSessionService.CreateOrUpdateAsync(user.Id, loginRequestModel.Fingerprint, refreshToken);
        
        _cookiesProvider.AddTokensCookiesToResponse(HttpContext.Response,accessToken, refreshToken);
        _cookiesProvider.AddFingerprintCookiesToResponse(HttpContext.Response, loginRequestModel.Fingerprint);
        
        return Result<None>.Success();
    }

    [HttpPost("logout")]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status400BadRequest)]
    public async Task<Result<None>> Logout()
    {
        var refreshToken = _cookiesProvider.GetTokensFromCookies(HttpContext.Request).RefreshToken;

        if (refreshToken is null)
        {
            return Result<None>.Error(ErrorCode.RefreshCookieNotFound);
        }

        if (!_jwtProvider.IsTokenValid(refreshToken, JwtTokenType.Refresh))
        {
            return Result<None>.Error(ErrorCode.InvalidRefreshToken);
        }

        var fingerprint = _cookiesProvider.GetFingerprintFromCookies(HttpContext.Request);
        
        if (string.IsNullOrEmpty(fingerprint))
        {
            return Result<None>.Error(ErrorCode.FingerprintCookieNotFound);
        }

        var userId = _jwtProvider.GetUserIdFromRefreshToken(refreshToken);

        await _refreshTokenSessionService.DeleteAsync(userId, fingerprint);
        
        _cookiesProvider.DeleteCookiesFromResponse(HttpContext.Response);

        return Result<None>.Success();
    }
}