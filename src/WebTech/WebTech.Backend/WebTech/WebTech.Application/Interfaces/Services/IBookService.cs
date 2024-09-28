using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Domain.Entities.Database;

namespace WebTech.Application.Interfaces.Services;

public interface IBookService
{
    Task<Result<Book>> CreateAsync(CreateBookDto createBookDto);

    Task<Result<IEnumerable<Book>>> GetAsync();
    
    Task<Result<Book>> UpdateAsync(Guid bookId, UpdateBookDto createBookDto);
    
    Task<Result<None>> DeleteAsync(Guid bookId);
}