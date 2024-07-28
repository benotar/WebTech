using Authors.Entities.Database;
using Authors.Models;
using Authors.CQRS.Commands;
using Authors.Helpers;
using Authors.CQRS.Queries;
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
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public IActionResult Login()
    {
        try
        {
            var jwtToken = _jwtService.Generate();

            Response.Cookies.Append("JWT", jwtToken, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok("Successful login!");
        }
        catch (Exception _)
        {
            return BadRequest(_.Message);
        }
    }

    [HttpPost("logout")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("JWT");

        return Ok("Successful logout!");
    }

    [HttpPost("create-author")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateAuthorRequest request)
    {
        try
        {
            var jwt = Request.Cookies["JWT"];

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

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }
    
    [HttpPost("create-book")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
    {
        try
        {
            var jwt = Request.Cookies["JWT"];

            var token = _jwtService.Verify(jwt);

            var bookCommand = new CreateBookCommand
            {
                Name = request.Name,
                PublicAt = request.PublicAt,
                Genre = request.Genre,
                AuthorId = request.AuthorId
            };

            var result = await _mediator.Send(bookCommand);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }

    [HttpGet("get-authors")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ICollection<Author>>> GetAuthors()
    {
        try
        {
            var jwt = Request.Cookies["JWT"];

            var token = _jwtService.Verify(jwt);

            var result = await _mediator.Send(new GetAuthorsQuery());

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Authors);
        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }

    [HttpGet("get-books")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ICollection<Author>>> GetBooks()
    {
        try
        {
            var jwt = Request.Cookies["JWT"];

            var token = _jwtService.Verify(jwt);

            var result = await _mediator.Send(new GetBooksQuery());

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Authors);
        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }

    [HttpPut("update-author")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest request)
    {
        try
        {
            var jwt = Request.Cookies["JWT"];

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
                    return BadRequest("Invalid author birth date format. Expected format is yyyy-MM-dd.");
                }
            }

            var command = new UpdateAuthorCommand
            {
                AuthorId = request.AuthorId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = authorBirthDate
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }

    [HttpPut("update-book")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request)
    {
        try
        {
            var jwt = Request.Cookies["JWT"];

            var token = _jwtService.Verify(jwt);

            var command = new UpdateBookCommand
            {
                BookId = request.BookId,
                Name = request.Name,
                PublicAt = request.PublicAt,
                Genre = request.Genre,
                AuthorId = request.AuthorId
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }

    [HttpDelete("delete-author/{authorId:int}")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteAuthor(int authorId)
    {
        try
        {
            var jwt = Request.Cookies["JWT"];

            var token = _jwtService.Verify(jwt);

            var result = await _mediator.Send(new DeleteAuthorCommand { AuthorId = authorId });

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }

    [HttpDelete("delete-book/{bookId:int}")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteBook(int bookId)
    {
        try
        {
            var jwt = Request.Cookies["JWT"];

            var token = _jwtService.Verify(jwt);

            var result = await _mediator.Send(new DeleteBookCommand { BookId = bookId });

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }
}