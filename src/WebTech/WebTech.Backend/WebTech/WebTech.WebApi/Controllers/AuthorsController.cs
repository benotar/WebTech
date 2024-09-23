using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.WebApi.Models.Authors;

namespace WebTech.WebApi.Controllers;

[Host("api.bg-local.net")]
[Authorize]
public class AuthorsController: BaseController
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("get-all")]
    public async Task<Result<IEnumerable<Author>>> Get()
    {
        return await _authorService.GetAuthorsAsync();
    }

    [HttpPost("create")]
    public async Task<Result<Author>> Create(CreateOrUpdateAuthorRequestModel createOrUpdateAuthorRequestModel)
    {
        var request = new CreateOrUpdateAuthorDto
        {
            FirstName = createOrUpdateAuthorRequestModel.FirstName,
            LastName = createOrUpdateAuthorRequestModel.LastName,
            BirthDate = createOrUpdateAuthorRequestModel.BirthDate
        };
        
        return await _authorService.CreateAsync(request);
    }

    [HttpPut("update/{authorId:guid}")]
    public async Task<Result<Author>> Update(Guid authorId, CreateOrUpdateAuthorRequestModel createOrUpdateAuthorRequestModel)
    {
        var request = new CreateOrUpdateAuthorDto
        {
            FirstName = createOrUpdateAuthorRequestModel.FirstName,
            LastName = createOrUpdateAuthorRequestModel.LastName,
            BirthDate = createOrUpdateAuthorRequestModel.BirthDate
        };

        return await _authorService.UpdateAsync(authorId, request);
    }

    [HttpDelete("delete/{authorId:guid}")]
    public async Task<Result<None>> Delete(Guid authorId)
    {
        return await _authorService.DeleteAsync(authorId);
    }
}