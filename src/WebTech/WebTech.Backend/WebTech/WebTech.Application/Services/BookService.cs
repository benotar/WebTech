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


    public async Task<Result<Book>> CreateAsync(CreateOrUpdateBookDto createOrUpdateBookDto)
    {
        var existingAuthorIdResult = await _authorService.GetAuthorIdByAuthorNamesAsync(
            createOrUpdateBookDto.AuthorFirstName,
            createOrUpdateBookDto.AuthorLastName);

        if (!existingAuthorIdResult.IsSucceed)
        {
            return Result<Book>.Error(ErrorCode.AuthorNotFound);
        }

        var isBookExistsCondition =
            _queryProvider.ByPropertyName(nameof(createOrUpdateBookDto.Title), createOrUpdateBookDto.Title);

        var isBookExists = await _queryProvider.ExecuteQueryAsync(query => query.AnyAsync(),
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
            AuthorId = existingAuthorIdResult.Data
        };

        await _dbContext.Books.AddAsync(newBook);

        await _dbContext.SaveChangesAsync();

        return Result<Book>.Success(newBook);
    }

    public async Task<Result<IEnumerable<Book>>> GetBooksAsync()
    {
        var books = await _queryProvider.ExecuteQueryAsync(query => query.ToListAsync());

        return books.Count > 0
            ? Result<IEnumerable<Book>>.Success(books)
            : Result<IEnumerable<Book>>.Error(ErrorCode.UnknownError);
    }

    public async Task<Result<Book>> GetByIdAndAuthorAsync(Guid bookId, string authorFirstName, string authorLastName)
    {
        var existingAuthorIdResult = await _authorService.GetAuthorIdByAuthorNamesAsync(
            authorFirstName, authorLastName);

        if (!existingAuthorIdResult.IsSucceed)
        {
            return Result<Book>.Error(ErrorCode.AuthorNotFound);
        }

        var authorId = existingAuthorIdResult.Data;

        var existingBookCondition = _queryProvider.ByEntityId(nameof(bookId), bookId)
            .And(_queryProvider.ByEntityId(nameof(authorId), authorId, isEntityForeignKey: true));

        var existingBook = await _queryProvider.ExecuteQueryAsync(query => query.FirstOrDefaultAsync(),
            existingBookCondition);

        return existingBook is null
            ? Result<Book>.Error(ErrorCode.BookNotFound)
            : Result<Book>.Success(existingBook);
    }

    public async Task<Result<Book>> UpdateAsync(Guid bookId, CreateOrUpdateBookDto createOrUpdateBookDto)
    {
        var existingBook = await GetBookByIdAsync(bookId, isTracking: true);
        
        if (existingBook is null)
        {
            return Result<Book>.Error(ErrorCode.BookNotFound);
        }

        if (StringComparer.OrdinalIgnoreCase.Equals(existingBook.Title, createOrUpdateBookDto.Title) &&
            StringComparer.OrdinalIgnoreCase.Equals(existingBook.Genre, createOrUpdateBookDto.Genre) &&
            existingBook.PublicationYear == createOrUpdateBookDto.PublicationYear)
        {
            return Result<Book>.Error(ErrorCode.BookDataIsTheSame);
        }

        existingBook.Title = createOrUpdateBookDto.Title;
        existingBook.Genre = createOrUpdateBookDto.Genre;
        existingBook.PublicationYear = createOrUpdateBookDto.PublicationYear;
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

    private async Task<Book> GetBookByIdAsync(Guid bookId, bool isTracking = false, bool isEntityForeignKey = false)
    {
        var condition = _queryProvider.ByEntityId(nameof(bookId), bookId, isEntityForeignKey);

        return await _queryProvider.ExecuteQueryAsync(query => query.FirstOrDefaultAsync(),
            condition, isTracking);
    }
}