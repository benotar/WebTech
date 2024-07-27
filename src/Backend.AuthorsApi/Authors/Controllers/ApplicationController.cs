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

    [HttpPost("create-author")]
    public async Task<IActionResult> Create([FromBody] CreateAuthorRequest request)
    {
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
                return BadRequest(new { Message = "Invalid author birth date format. Expected format is yyyy-MM-dd." });
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


    [HttpPost("create-book")]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
    {
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
}