using Authors.Entities.Database;
using Authors.Models;
using Authors.CQRS.Commands;
using Authors.SQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authors.Controllers;

[ApiController]
[Route("Authors")]
public class ApplicationController : Controller
{
    private readonly IMediator _mediator;

    public ApplicationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRequest request)
    {
        DateTime authorBirthDate = default;

        if (!string.IsNullOrWhiteSpace(request.AuthorDateOfBirth))
        {
            if (DateTime.TryParseExact(request.AuthorDateOfBirth, "yyyy-MM-dd", null,
                    System.Globalization.DateTimeStyles.None,
                    out var parsedDate))
            {
                authorBirthDate = parsedDate.AddHours(3);
            }
            else
            {
                return BadRequest(new { Message = "Invalid author birth date format. Expected format is yyyy-MM-dd." });
            }
        }

        var authorCommand = new CreateAuthorCommand
        {
            FirstName = request.AuthorFirstName,
            LastName = request.AuthorLastName,
            DateOfBirth = authorBirthDate
        };

        try
        {
            var author = await _mediator.Send(authorCommand);

            var bookCommand = new CreateBookCommand
            {
                Name = request.BookName,
                PublicAt = request.BookPublicAt,
                Genre = request.BookGenre,
                AuthorId = author.Id
            };

            if (!await _mediator.Send(bookCommand))
            {
                return BadRequest(new
                {
                    message = "Failed to create book!"
                });
            }

            return Ok(new
            {
                message = "Success"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet("get")]
    [ProducesResponseType(typeof(List<Author>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<Author>>> Get()
    {
        var result = await _mediator.Send(new GetAuthorsBooksQuery());

        if (result is null)
        {
            return BadRequest(new
            {
                message = "Failed to get authors!"
            });
        }

        return Ok(result);
    }
}