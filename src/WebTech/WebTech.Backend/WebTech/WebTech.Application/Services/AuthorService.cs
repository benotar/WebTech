using Microsoft.EntityFrameworkCore;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Persistence;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.Domain.Enums;

namespace WebTech.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IQueryProvider<Author> _queryProvider;

    private readonly IWebTechDbContext _dbContext;

    private readonly IDateTimeProvider _dateTimeProvider;

    public AuthorService(IQueryProvider<Author> queryProvider, IWebTechDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
    {
        _queryProvider = queryProvider;

        _dbContext = dbContext;

        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Author>> CreateAsync(CreateOrUpdateAuthorDto createOrUpdateAuthorDto)
    {
        var condition = _queryProvider.ByAuthorFullName(
            createOrUpdateAuthorDto.FirstName,
            createOrUpdateAuthorDto.LastName);

        var isAuthorExists = await _queryProvider.ExecuteQueryAsync(query => query.AnyAsync(),
            condition);

        if (isAuthorExists)
        {
            return Result<Author>.Error(ErrorCode.AuthorAlreadyExists);
        }

        var newAuthor = new Author
        {
            FirstName = createOrUpdateAuthorDto.FirstName,
            LastName = createOrUpdateAuthorDto.LastName,
            BirthDate = createOrUpdateAuthorDto.BirthDate
        };

        await _dbContext.Authors.AddAsync(newAuthor);

        await _dbContext.SaveChangesAsync();

        return Result<Author>.Success(newAuthor);
    }

    public async Task<Result<IEnumerable<Author>>> GetAsync()
    {
        var authors = await _queryProvider.ExecuteQueryAsync(query => query.ToListAsync());

        return Result<IEnumerable<Author>>.Success(authors);
    }
    
    public async Task<Result<Author>> UpdateAsync(Guid authorId, CreateOrUpdateAuthorDto createOrUpdateAuthorDto)
    {
        var existingAuthor = await GetAuthorByIdAsync(authorId, isTracking: true);

        if (existingAuthor is null)
        {
            return Result<Author>.Error(ErrorCode.AuthorNotFound);
        }

        if (StringComparer.OrdinalIgnoreCase.Equals(createOrUpdateAuthorDto.FirstName, existingAuthor.FirstName) &&
            StringComparer.OrdinalIgnoreCase.Equals(createOrUpdateAuthorDto.LastName, existingAuthor.LastName) &&
            createOrUpdateAuthorDto.BirthDate == existingAuthor.BirthDate)
        {
            return Result<Author>.Error(ErrorCode.AuthorDataIsTheSame);
        }

        existingAuthor.FirstName = createOrUpdateAuthorDto.FirstName;
        existingAuthor.LastName = createOrUpdateAuthorDto.LastName;
        existingAuthor.BirthDate = createOrUpdateAuthorDto.BirthDate;
        existingAuthor.UpdatedAt = _dateTimeProvider.UtcNow;

        await _dbContext.SaveChangesAsync();

        return Result<Author>.Success(existingAuthor);
    }

    public async Task<Result<None>> DeleteAsync(Guid authorId)
    {
        var existingAuthor = await GetAuthorByIdAsync(authorId);

        if (existingAuthor is null)
        {
            return Result<None>.Error(ErrorCode.AuthorNotFound);
        }

        _dbContext.Authors.Remove(existingAuthor);

        await _dbContext.SaveChangesAsync();

        return Result<None>.Success();
    }

    public async Task<Result<Guid>> GetAuthorIdByAuthorNamesAsync(string firstName, string lastName)
    {
        var existingAuthorCondition = _queryProvider.ByAuthorFullName(firstName, lastName);

        var authorId = await _queryProvider.ExecuteQueryAsync(query =>
            query.Where(existingAuthorCondition).Select(author => author.Id).FirstOrDefaultAsync());

        return authorId == Guid.Empty
            ? Result<Guid>.Error(ErrorCode.AuthorNotFound)
            : Result<Guid>.Success(authorId);
    }

    // Helper method
    private async Task<Author> GetAuthorByIdAsync(Guid authorId, bool isTracking = false,
        bool isEntityForeignKey = false)
    {
        var condition = _queryProvider.ByEntityId(nameof(authorId), authorId, isEntityForeignKey);

        return await _queryProvider.ExecuteQueryAsync(query => query.FirstOrDefaultAsync(),
            condition, isTracking);
    }
}