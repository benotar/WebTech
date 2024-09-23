using Microsoft.EntityFrameworkCore;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Extensions;
using WebTech.Application.Interfaces.Persistence;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.Domain.Enums;

namespace WebTech.Application.Services;

public class BookService : IBookService
{
    private readonly IQueryProvider<Book> _queryBookProvider;

    private readonly IWebTechDbContext _dbContext;
    
    private readonly IAuthorService _authorService;

    public BookService(IQueryProvider<Book> queryBookProvider, IAuthorService authorService, IWebTechDbContext dbContext)
    {
        _queryBookProvider = queryBookProvider;
        
        _authorService = authorService;
        
        _dbContext = dbContext;
    }


    public async Task<Result<Book>> CreateAsync(CreateOrUpdateBookDto createOrUpdateBookDto)
    {
        var existingAuthorResult = await _authorService.GetAuthorByNamesAsync(
            createOrUpdateBookDto.AuthorFirstName,
            createOrUpdateBookDto.AuthorLastName);

        if (!existingAuthorResult.IsSucceed)
        {
            return Result<Book>.Error(ErrorCode.AuthorNotFound);
        }

        var isBookExistsCondition =
            _queryBookProvider.ByPropertyName(nameof(createOrUpdateBookDto.Title), createOrUpdateBookDto.Title);
        
        var isBookExists = await _queryBookProvider.ExecuteQueryAsync(query => query.AnyAsync(),
            isBookExistsCondition);
        
        if (isBookExists)
        {
            return Result<Book>.Error(ErrorCode.BookAlreadyExists);
        }
        
        var newBook = new Book
        {
            Title = createOrUpdateBookDto.Title,
            Genre = createOrUpdateBookDto.Genre,
            PublicationYear = createOrUpdateBookDto.PublicationYear,
            AuthorId = existingAuthorResult.Data.Id
        };

        await _dbContext.Books.AddAsync(newBook);

        await _dbContext.SaveChangesAsync();
        
        return Result<Book>.Success(newBook);
    }

    public Task<Result<Book>> GetCurrentAsync(Guid bookId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Book>> UpdateAsync(Guid bookId, CreateOrUpdateBookDto createOrUpdateBookDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<None>> DeleteAsync(Guid bookId)
    {
        throw new NotImplementedException();
    }
}