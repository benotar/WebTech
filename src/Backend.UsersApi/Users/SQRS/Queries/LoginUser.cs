using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Entities.Database;
using Users.Interfaces;

namespace Users.SQRS.Queries;

public class LoginUserQuery : IRequest<User>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, User>
{
    private readonly IUsersDbContext _db;

    public LoginUserQueryHandler(IUsersDbContext db)
    {
        _db = db;
    }

    public async Task<User> Handle(LoginUserQuery request, CancellationToken cancellationToken)
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
        
        return existingUser;
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