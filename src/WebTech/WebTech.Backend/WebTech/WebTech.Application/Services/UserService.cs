using System.Collections;
using Microsoft.EntityFrameworkCore;
using WebTech.Application.Common;
using WebTech.Application.DTOs;
using WebTech.Application.Interfaces.Persistence;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Entities.Database;
using WebTech.Domain.Enums;

namespace WebTech.Application.Services;

public class UserService : IUserService
{
    private readonly IWebTechDbContext _dbContext;

    private readonly IQueryProvider<User> _queryProvider;

    private readonly IEncryptionProvider _hmacSha256Provider;
    
    public UserService(IWebTechDbContext dbContext, IQueryProvider<User> queryProvider, IEncryptionProvider hmacSha256Provider)
    {
        _dbContext = dbContext;

        _queryProvider = queryProvider;
        
        _hmacSha256Provider = hmacSha256Provider;
    }

    public async Task<Result<IEnumerable<User>>> GetAsync()
    {
        var users = await _queryProvider.ExecuteQueryAsync(query => query.ToListAsync());

        return users.Count > 0
            ? Result<IEnumerable<User>>.Success(users)
            : Result<IEnumerable<User>>.Error(ErrorCode.Unknown);
    }

    public async Task<Result<User>> GetCurrentAsync(Guid userId)
    {
        var existingUser = await _queryProvider.ExecuteQueryAsync(query => query.FirstOrDefaultAsync(),
            _queryProvider.ByEntityId(nameof(userId), userId));
        
        return existingUser is not null
            ? Result<User>.Success(existingUser)
            : Result<User>.Error(ErrorCode.UserNotFound);
    }

    public async Task<Result<User>> CreateAsync(CreateUserDto createUserDto)
    {
        var isUserExist = await _queryProvider.ExecuteQueryAsync(query => query.AnyAsync(),
            _queryProvider.ByPropertyName(nameof(createUserDto.UserName), createUserDto.UserName));
        
        if (isUserExist)
        {
            return Result<User>.Error(ErrorCode.UsernameAlreadyExists);
        }

        var passwordSaltAndHash = await _hmacSha256Provider.HashPasswordAsync(createUserDto.Password);

        var newUser = new User
        {
            UserName = createUserDto.UserName,
            PasswordSalt = passwordSaltAndHash.Salt,
            PasswordHash = passwordSaltAndHash.Hash,
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            BirthDate = createUserDto.BirthDate,
            Address = createUserDto.Address
        };

        await _dbContext.Users.AddAsync(newUser);

        await _dbContext.SaveChangesAsync();
        
        return !newUser.Id.Equals(Guid.Empty)
            ? Result<User>.Success(newUser)
            : Result<User>.Error(ErrorCode.DataNotSavedToDatabase);
    }

    public async Task<Result<User>> GetExistingUser(string userName, string password)
    {
        var existingUser = await _queryProvider.ExecuteQueryAsync(query => query.FirstOrDefaultAsync(),
            _queryProvider.ByPropertyName(nameof(userName), userName, isEntityNameBeginLower: true));
        
        if (existingUser is null)
        {
            return Result<User>.Error(ErrorCode.UserNotFound);
        }
        
        var existingUserPasswordSaltAndHash = new SaltAndHash(existingUser.PasswordSalt, existingUser.PasswordHash);

        return await _hmacSha256Provider.VerifyPasswordHash(password, existingUserPasswordSaltAndHash)
            ? Result<User>.Success(existingUser)
            : Result<User>.Error(ErrorCode.AuthenticationFailed);
    }

}