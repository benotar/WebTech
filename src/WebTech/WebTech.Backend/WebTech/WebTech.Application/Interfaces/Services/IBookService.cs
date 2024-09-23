using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Domain.Entities.Database;

namespace WebTech.Application.Interfaces.Services;

public interface IBookService
{
    Task<Result<Book>> CreateAsync(CreateOrUpdateBookDto createOrUpdateBookDto);
    Task<Result<Book>> GetCurrentAsync(Guid bookId);
    Task<Result<Book>> UpdateAsync(Guid bookId, CreateOrUpdateBookDto createOrUpdateBookDto);
    Task<Result<None>> DeleteAsync(Guid bookId);
}