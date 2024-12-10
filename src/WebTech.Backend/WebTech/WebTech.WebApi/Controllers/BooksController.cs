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
    public async Task<Result<Book>> Create(CreateBookRequestModel createBookRequestModel)
    {
        
        var request = new CreateBookDto
        {
            AuthorFirstName = createBookRequestModel.AuthorFirstName,
            AuthorLastName = createBookRequestModel.AuthorLastName,
            PublicationYear = createBookRequestModel.PublicationYear,
            Title = createBookRequestModel.Title,
            Genre = createBookRequestModel.Genre
        };

        return await _bookService.CreateAsync(request);
    }
    
    [HttpPut("update/{bookId:guid}")]
    [ProducesResponseType(typeof(Result<Book>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Book>), StatusCodes.Status401Unauthorized)]
    public async Task<Result<Book>> Update(Guid bookId, UpdateBookDto createOrUpdateBookRequestModel)
    {
        var request = new UpdateBookDto
        {
            Title = createOrUpdateBookRequestModel.Title,
            Genre = createOrUpdateBookRequestModel.Genre,
            PublicationYear = createOrUpdateBookRequestModel.PublicationYear,
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