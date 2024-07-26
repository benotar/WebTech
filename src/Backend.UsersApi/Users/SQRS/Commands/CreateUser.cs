using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Entities.Database;
using Users.Interfaces;
using s = System.String;

namespace Users.SQRS.Commands;

public class CreateUserCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Address { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IUsersDbContext _db;

    public CreateUserCommandHandler(IUsersDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _db.Users
            .AnyAsync(u => u.Username == request.Username,
                cancellationToken);

        if (existingUser)
        {
            throw new Exception($"User \'{request.Username}\' already exists!");
        }

        var newUser = new User
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.BirthDate.AddHours(3),
            Address = request.Address
        };

        using (HMACSHA256 hmac = new())
        {
            newUser.PasswordSalt = hmac.Key;

            newUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        }

        _db.Users.Add(newUser);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}