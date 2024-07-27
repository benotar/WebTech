using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Entities.Database;
using Users.Helpers;
using Users.Models;
using Users.CQRS.Commands;
using Users.CQRS.Queries;

namespace Users.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    private readonly JwtService _jwtService;
    public UserController(IMediator mediator, JwtService jwtService)
    {
        _mediator = mediator;
        
        _jwtService = jwtService;
    }


    [HttpPost("register")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        DateTime birthDate = default;

        if (!string.IsNullOrWhiteSpace(request.BirthDate))
        {
            if (DateTime.TryParseExact(request.BirthDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None,
                    out var parsedDate))
            {
                birthDate = parsedDate.AddHours(3);
            }
            else
            {
                return BadRequest(new { Message = "Invalid birth date format. Expected format is yyyy-MM-dd." });
            }
        }
        
        var command = new CreateUserCommand
        {
            Username = request.Username,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = birthDate,
            Address = request.Address
        };

        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ActionResult<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ActionResult<User>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> Login([FromBody] LoginUserRequest request)
    {
        var command = new LoginUserQuery
        {
            Username = request.Username,
            Password = request.Password
        };

        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        var jwtToken = result.jwt;
        
        Response.Cookies.Append("jwt", jwtToken, new CookieOptions
        {
            HttpOnly = true
        });
        
        return Ok(result.Message);
    }

    [HttpPost("logout")]
    [ProducesResponseType(typeof(ActionResult<User>), StatusCodes.Status200OK)]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");

        return Ok(new
        {
            message = "Success"
        });
    }
    
    [HttpGet("me")]
    [ProducesResponseType(typeof(ActionResult<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ActionResult<User>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ActionResult<User>), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<User>> GetUser()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];

            var token = _jwtService.Verify(jwt);

            var command = new GetUserByIdQuery();

            if (!int.TryParse(token.Issuer, out int userId))
            {
                return BadRequest("Invalid data!");
            }

            command.Id = userId;

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            
            var user = result.User;

            return Ok(new
            {
                message = result.Message,
                user
            });
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
}