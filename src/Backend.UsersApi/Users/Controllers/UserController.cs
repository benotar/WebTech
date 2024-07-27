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

        try
        {
            var result = await _mediator.Send(command);

            return Ok(new { Message = "User registered successfully!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message, });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] LoginUserRequest request)
    {
        var command = new LoginUserQuery
        {
            Username = request.Username,
            Password = request.Password
        };

        try
        {
            var jwtToken = await _mediator.Send(command);

            Response.Cookies.Append("jwt", jwtToken, new CookieOptions
            {
                HttpOnly = true
            });
            
            return Ok(new
            {
                message = "Success"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetUser()
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

            var user = await _mediator.Send(command);


            return Ok(user);
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");

        return Ok(new
        {
            message = "Success"
        });
    }
    
}