using Authors.Entities.Database;
using Authors.Models;
using Authors.CQRS.Commands;
using Authors.Helpers;
using Authors.SQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authors.Controllers;

[ApiController]
[Route("Authors")]
public class ApplicationController : Controller
{
    private readonly IMediator _mediator;

    private JwtService _jwtService;

    public ApplicationController(IMediator mediator, JwtService jwtService)
    {
        _mediator = mediator;

        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        try
        {
            var jwtToken = _jwtService.Generate();

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

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");

        return Ok(new
        {
            message = "Success"
        });
    }

    [HttpPost("create-author")]
    public async Task<IActionResult> Create([FromBody] CreateAuthorRequest request)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];

            var token = _jwtService.Verify(jwt);
            
            DateTime authorBirthDate = default;

            if (!string.IsNullOrWhiteSpace(request.DateOfBirth))
            {
                if (DateTime.TryParseExact(request.DateOfBirth, "yyyy-MM-dd", null,
                        System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    authorBirthDate = parsedDate.AddHours(3);
                }
                else
                {
                    return BadRequest(new
                        { Message = "Invalid author birth date format. Expected format is yyyy-MM-dd." });
                }
            }

            var authorCommand = new CreateAuthorCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = authorBirthDate
            };

            var result = await _mediator.Send(authorCommand);

            if (!result.isCreated)
            {
                return BadRequest(new
                {
                    message = result.message
                });
            }

            return Ok(new
            {
                message = result.message
            });
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }


    [HttpPost("create-book")]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];

            var token = _jwtService.Verify(jwt);
            
            var bookCommand = new CreateBookCommand
            {
                Name = request.Name,
                PublicAt = request.PublicAt,
                Genre = request.Genre,
                AuthorId = request.AuthorId
            };

            var result = await _mediator.Send(bookCommand);

            if (!result.isCreated)
            {
                return BadRequest(new
                {
                    message = result.message
                });
            }

            return Ok(new
            {
                message = result.message
            });
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }

    [HttpGet("get-authors")]
    public async Task<ActionResult<ICollection<Author>>> GetAuthors()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];

            var token = _jwtService.Verify(jwt);

            var result = await _mediator.Send(new GetAuthorsBooksQuery());

            if (!result.Item2.isSuccess)
            {
                return BadRequest(result.Item2.message);
            }

            return Ok(result.authors);
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
}