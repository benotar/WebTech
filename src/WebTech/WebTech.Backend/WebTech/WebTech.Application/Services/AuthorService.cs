using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;

namespace WebTech.Application.Services;

public class AuthorService : IAuthorService
{
    public Task<Result<Author>> CreateAsync(CreateOrUpdateAuthorDto createOrUpdateAuthorDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Author>> GetCurrentAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Author>> UpdateAsync(Guid userId, CreateOrUpdateAuthorDto createOrUpdateAuthorDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Author>> DeleteAsync(Guid userId, CreateOrUpdateAuthorDto createOrUpdateAuthorDto)
    {
        throw new NotImplementedException();
    }
}