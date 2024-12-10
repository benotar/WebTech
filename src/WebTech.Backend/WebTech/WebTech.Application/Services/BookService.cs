using Microsoft.EntityFrameworkCore;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Persistence;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.Domain.Enums;

namespace WebTech.Application.Services;

public class BookService : IBookService
{
    private readonly IQueryProvider<Book> _queryProvider;

    private readonly IWebTechDbContext _dbContext;

    private readonly IAuthorService _authorService;

    private readonly IDateTimeProvider _dateTimeProvider;

    public BookService(IQueryProvider<Book> queryProvider, IAuthorService authorService, IWebTechDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
    {
        _queryProvider = queryProvider;

        _authorService = authorService;

        _dbContext = dbContext;

        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Book>> CreateAsync(CreateBookDto createBookDto)
    {
        var existingAuthorIdResult = await _authorService.GetAuthorIdByAuthorNamesAsync(
            createBookDto.AuthorFirstName,
            createBookDto.AuthorLastName);

        if (!existingAuthorIdResult.IsSucceed)
        {
            return Result<Book>.Error(ErrorCode.AuthorNotFound);
        }

        var isBookExistsCondition =
            _queryProvider.ByPropertyName(nameof(createBookDto.Title), 
                createBookDto.Title);

        var isBookExists = await _queryProvider.ExecuteQueryAsync(query =>
            query.AnyAsync(), isBookExistsCondition);

        if (isBookExists)
        {
            return Result<Book>.Error(ErrorCode.BookAlreadyExists);
        }

        var newBook = new Book
        {
            Title = createBookDto.Title,
            Genre = createBookDto.Genre,
            PublicationYear = createBookDto.PublicationYear,
            AuthorId = existingAuthorIdResult.Data
        };

        await _dbContext.Books.AddAsync(newBook);

        await _dbContext.SaveChangesAsync();

        return Result<Book>.Success(newBook);
    }

    public async Task<Result<IEnumerable<Book>>> GetAsync()
    {
        var books = await _queryProvider.ExecuteQueryAsync(query =>
            query.ToListAsync());

        return Result<IEnumerable<Book>>.Success(books);
    }
    
    public async Task<Result<Book>> UpdateAsync(Guid bookId, UpdateBookDto createBookDto)
    {
        var existingBook = await GetBookByIdAsync(bookId, isTracking: true);
        
        if (existingBook is null)
        {
            return Result<Book>.Error(ErrorCode.BookNotFound);
        }

        if (StringComparer.OrdinalIgnoreCase.Equals(existingBook.Title, createBookDto.Title) &&
            StringComparer.OrdinalIgnoreCase.Equals(existingBook.Genre, createBookDto.Genre) &&
            existingBook.PublicationYear == createBookDto.PublicationYear)
        {
            return Result<Book>.Error(ErrorCode.BookDataIsTheSame);
        }

        existingBook.Title = createBookDto.Title;
        existingBook.Genre = createBookDto.Genre;
        existingBook.PublicationYear = createBookDto.PublicationYear;
        existingBook.UpdatedAt = _dateTimeProvider.UtcNow;

        await _dbContext.SaveChangesAsync();

        return Result<Book>.Success(existingBook);
    }

    public async Task<Result<None>> DeleteAsync(Guid bookId)
    {
        var existingBook = await GetBookByIdAsync(bookId);
        
        if (existingBook is null)
        {
            return Result<None>.Error(ErrorCode.BookNotFound);
        }

        _dbContext.Books.Remove(existingBook);

        await _dbContext.SaveChangesAsync();

        return Result<None>.Success();
    }

    private async Task<Book?> GetBookByIdAsync(Guid bookId, bool isTracking = false, bool isEntityForeignKey = false)
    {
        var condition = _queryProvider.ByEntityId(nameof(bookId),
            bookId, isEntityForeignKey);

        return await _queryProvider.ExecuteQueryAsync(query => query.FirstOrDefaultAsync(),
            condition, isTracking);
    }
}