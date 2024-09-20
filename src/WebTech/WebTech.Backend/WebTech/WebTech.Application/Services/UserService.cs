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
        var users = await _queryProvider.GetAsync(query => query.ToListAsync());

        return users.Count > 0
            ? Result<IEnumerable<User>>.Success(users)
            : Result<IEnumerable<User>>.Error(ErrorCode.Unknown);
    }
    
    public async Task<Result<User>> CreateAsync(CreateUserDto createUserDto)
    {
        var condition = _queryProvider.ByUserName(createUserDto.UserName);

        var isUserExist = await _queryProvider.FindByConditionAsync(condition,
            query => query.AnyAsync());

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
}