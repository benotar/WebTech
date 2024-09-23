using Microsoft.AspNetCore.Mvc;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.WebApi.Models.Authors;

namespace WebTech.WebApi.Controllers;

[Host("api.bg-local.net")]
public class AuthorsController: BaseController
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        return Ok("Hello from api.bg-local.net");
    }

    [HttpPost("create")]
    public async Task<Result<Author>> Create(CreateAuthorRequestModel createAuthorRequestModel)
    {
        var request = new CreateOrUpdateAuthorDto
        {
            FirstName = createAuthorRequestModel.FirstName,
            LastName = createAuthorRequestModel.LastName,
            BirthDate = createAuthorRequestModel.BirthDate
        };
        
        return await _authorService.CreateAsync(request);
    }
} 