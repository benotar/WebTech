using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Domain.Entities.Database;

namespace WebTech.Application.Interfaces.Services;

public interface IAuthorService
{
    Task<Result<Author>> CreateAsync(CreateOrUpdateAuthorDto createOrUpdateAuthorDto);

    Task<Result<IEnumerable<Author>>> GetAsync();
    
    Task<Result<Author>> UpdateAsync(Guid authorId, CreateOrUpdateAuthorDto createOrUpdateAuthorDto);
    
    Task<Result<None>> DeleteAsync(Guid authorId);

    Task<Result<Guid>> GetAuthorIdByAuthorNamesAsync(string firstName, string lastName);
}