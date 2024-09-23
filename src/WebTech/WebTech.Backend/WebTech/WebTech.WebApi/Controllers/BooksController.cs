using Microsoft.AspNetCore.Mvc;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.WebApi.Models.Book;

namespace WebTech.WebApi.Controllers;

[Host("api.bg-local.net")]
public class BooksController : BaseController
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("get")]
    public async Task<Result<Book>> Get (GetBookRequestModel getBookRequestModel)
    {
        return await _bookService.GetByIdAndAuthorAsync(getBookRequestModel.BookId,
            getBookRequestModel.AuthorFirstName,
            getBookRequestModel.AuthorLastName);
    }

    [HttpPost("create")]
    public async Task<Result<Book>> Create(CreateBookRequestModel createBookRequestModel)
    {
        var request = new CreateOrUpdateBookDto
        {
            AuthorFirstName = createBookRequestModel.AuthorFirstName,
            AuthorLastName = createBookRequestModel.AuthorLastName,
            PublicationYear = createBookRequestModel.PublicationYear,
            Title = createBookRequestModel.Title,
            Genre = createBookRequestModel.Genre
        };

        return await _bookService.CreateAsync(request);
    }
}