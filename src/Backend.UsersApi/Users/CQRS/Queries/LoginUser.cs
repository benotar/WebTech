using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Entities.Database;
using Users.Helpers;
using Users.Interfaces;

namespace Users.CQRS.Queries;

public class LoginUserQuery : IRequest<string>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
{
    private readonly IUsersDbContext _db;

    private readonly JwtService _jwtService;
    
    public LoginUserQueryHandler(IUsersDbContext db, JwtService jwtService)
    {
        _db = db;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var existingUser = await _db.Users.Where(u => u.Username == request.Username)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        
        if (existingUser is null)
        {
            throw new Exception($"Invalid username or password!");
        }

        if (!CheckPassword(request.Password, existingUser))
        {
            throw new Exception($"Invalid username or password!");
        }

        var jwt = _jwtService.Generate(existingUser.Id);
        
        return jwt;
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