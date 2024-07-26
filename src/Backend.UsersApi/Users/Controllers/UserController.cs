using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Entities.Database;
using Users.Models;
using Users.SQRS.Commands;
using Users.SQRS.Queries;

namespace Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand
        {
            Username = request.Username,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Address = request.Address
        };
        
        try
        {
            var result = await _mediator.Send(command);

            return Ok(new { Message = "User registered successfully!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message,});
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
            var user = await _mediator.Send(command);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message});
        }
    }
}