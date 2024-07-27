using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Entities.Database;
using Users.Helpers;
using Users.Interfaces;

namespace Users.CQRS.Queries;

public record LoginUserQueryResult(string jwt,string Message, bool IsSuccess);
public class LoginUserQuery : IRequest<LoginUserQueryResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserQueryResult>
{
    private readonly IUsersDbContext _db;

    private readonly JwtService _jwtService;
    
    public LoginUserQueryHandler(IUsersDbContext db, JwtService jwtService)
    {
        _db = db;
        
        _jwtService = jwtService;
    }

    public async Task<LoginUserQueryResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var existingUser = await _db.Users.Where(u => u.Username == request.Username)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        
        if (existingUser is null)
        {
            return new LoginUserQueryResult(string.Empty,$"Invalid username or password!", false);
        }

        if (!CheckPassword(request.Password, existingUser))
        {
            return new LoginUserQueryResult(string.Empty, $"Invalid username or password!", false);
        }

        var jwt = _jwtService.Generate(existingUser.Id);

        return new LoginUserQueryResult(jwt, "Successful login!", true);
    }

    private static bool CheckPassword(string requestPassword, User user)
    {
        var result = false;

        using (HMACSHA256 hmac = new(user.PasswordSalt))
        {
            var compute = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestPassword));

            result = compute.SequenceEqual(user.PasswordHash);
        }

        return result;
    }
}