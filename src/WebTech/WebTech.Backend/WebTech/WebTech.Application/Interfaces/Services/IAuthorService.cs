using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Domain.Entities.Database;

namespace WebTech.Application.Interfaces.Services;

public interface IAuthorService
{
    Task<Result<Author>> CreateAsync(CreateOrUpdateAuthorDto createOrUpdateAuthorDto);
    Task<Result<Author>> GetCurrentAsync(Guid userId);
    Task<Result<Author>> UpdateAsync(Guid userId, CreateOrUpdateAuthorDto createOrUpdateAuthorDto);
    Task<Result<Author>> DeleteAsync(Guid userId, CreateOrUpdateAuthorDto createOrUpdateAuthorDto);
}