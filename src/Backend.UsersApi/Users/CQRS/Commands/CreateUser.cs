using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Entities.Database;
using Users.Interfaces;

namespace Users.CQRS.Commands;

public record CreateUserCommandResult(string Message, bool IsSuccess);
public class CreateUserCommand : IRequest<CreateUserCommandResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Address { get; set; }
}
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResult>
{
    private readonly IUsersDbContext _db;

    public CreateUserCommandHandler(IUsersDbContext db)
    {
        _db = db;
    }

    public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _db.Users
            .AnyAsync(u => u.Username == request.Username,
                cancellationToken);

        if (existingUser)
        {
            return new CreateUserCommandResult($"User \'{request.Username}\' already exists!", false);
        }
        
        var newUser = new User
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.BirthDate,
            Address = request.Address
        };

        using (HMACSHA256 hmac = new())
        {
            newUser.PasswordSalt = hmac.Key;

            newUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        }

        _db.Users.Add(newUser);

        await _db.SaveChangesAsync(cancellationToken);

        return new CreateUserCommandResult($"The user \'{request.FirstName} {request.LastName}\' has been successfully created!", true);
    }
}