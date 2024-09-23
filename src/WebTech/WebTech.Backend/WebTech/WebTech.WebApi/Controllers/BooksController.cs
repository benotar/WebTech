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
    public async Task<Result<IEnumerable<Book>>> Get()
    {
        return await _bookService.GetBooksAsync();
    }
    
    [HttpGet("get-book")]
    public async Task<Result<Book>> GetBook(GetBookRequestModel getBookRequestModel)
    {
        return await _bookService.GetByIdAndAuthorAsync(getBookRequestModel.BookId,
            getBookRequestModel.AuthorFirstName,
            getBookRequestModel.AuthorLastName);
    }

    [HttpPost("create")]
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
    public async Task<Result<None>> Delete(Guid bookId)
    {
        return await _bookService.DeleteAsync(bookId);
    }
}