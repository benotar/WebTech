using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Domain.Entities.Database;

namespace WebTech.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<User>> CreateAsync(CreateUserDto createUserDto);
    
    Task<Result<IEnumerable<User>>> GetAsync();

    Task<Result<User>> GetExistingUser(string userName, string password);
}