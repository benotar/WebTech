using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.WebApi.Models.Book;

namespace WebTech.WebApi.Controllers;

[Host("api.bg-local.net")]
[Authorize]
public class BooksController : BaseController
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(Result<IEnumerable<Book>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<Book>>), StatusCodes.Status401Unauthorized)]
    public async Task<Result<IEnumerable<Book>>> Get()
    {
        return await _bookService.GetAsync();
    }
    
    [HttpPost("create")]
    [ProducesResponseType(typeof(Result<Book>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Book>), StatusCodes.Status401Unauthorized)]
    public async Task<Result<Book>> Create(CreateOrUpdateBookRequestModel createOrUpdateBookRequestModel)
    {
        
        var request = new CreateOrUpdateBookDto
        {
            AuthorFirstName = createOrUpdateBookRequestModel.AuthorFirstName,
            AuthorLastName = createOrUpdateBookRequestModel.AuthorLastName,
            PublicationYear = createOrUpdateBookRequestModel.PublicationYear,
            Title = createOrUpdateBookRequestModel.Title,
            Genre = createOrUpdateBookRequestModel.Genre
        };

        return await _bookService.CreateAsync(request);
    }
    
    [HttpPut("update/{bookId:guid}")]
    [ProducesResponseType(typeof(Result<Book>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Book>), StatusCodes.Status401Unauthorized)]
    public async Task<Result<Book>> Update(Guid bookId, CreateOrUpdateBookRequestModel createOrUpdateBookRequestModel)
    {
        var request = new CreateOrUpdateBookDto
        {
            Title = createOrUpdateBookRequestModel.Title,
            Genre = createOrUpdateBookRequestModel.Genre,
            PublicationYear = createOrUpdateBookRequestModel.PublicationYear,
            AuthorFirstName = createOrUpdateBookRequestModel.AuthorFirstName,
            AuthorLastName = createOrUpdateBookRequestModel.AuthorLastName
        };

        return await _bookService.UpdateAsync(bookId, request);
    }

    [HttpDelete("delete/{bookId:guid}")]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status401Unauthorized)]
    public async Task<Result<None>> Delete(Guid bookId)
    {
        return await _bookService.DeleteAsync(bookId);
    }
}