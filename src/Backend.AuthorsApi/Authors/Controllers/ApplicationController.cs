using Authors.Models;
using Authors.SQRS.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Authors.Controllers;

[ApiController]
[Route("Authors")]
public class ApplicationController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAuthorRequest authorRequest, [FromBody] CreateBookRequest bookRequest)
    {
        DateTime authorBirthDate = default;

        if (!string.IsNullOrWhiteSpace(authorRequest.DateOfBirth))
        {
            if (DateTime.TryParseExact(authorRequest.DateOfBirth, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None,
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
            FirstName = authorRequest.FirstName,
            LastName = authorRequest.LastName,
            DateOfBirth = authorBirthDate
        };
        
        /////
        return Ok();
    }
}