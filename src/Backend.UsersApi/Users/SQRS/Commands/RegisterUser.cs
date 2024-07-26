using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Entities.Database;
using Users.Interfaces;

using s = System.String;

namespace Users.SQRS.Commands;

public class RegisterUserCommand : IRequest<User>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
{
    private readonly IUsersDbContext _db;

    public RegisterUserCommandHandler(IUsersDbContext db)
    {
        _db = db;
    }

    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _db.Users.AnyAsync(u => u.Username == request.Username, cancellationToken);
        
        if (existingUser)
        {
            throw new Exception($"User \'{request.Username}\' already exists!");
        }
        
        // Validate username
        if (s.IsNullOrEmpty(request.Username))
            throw new Exception("Username is required!");
        if (request.Username.Length > 50)
            throw new Exception("Username can't be longer than 50 characters!");
        
        
        // Validate firstname
        if (s.IsNullOrEmpty(request.FirstName))
            throw new Exception("FirstName is required!");
        if (request.FirstName.Length > 100)
            throw new Exception("FirstName can't be longer than 100 characters!");
        
        // Validate lastname
        if (s.IsNullOrEmpty(request.LastName))
            throw new Exception("LastName is required!");
        if (request.LastName.Length > 100)
            throw new Exception("LastName can't be longer than 100 characters!");
        
        // Validate password
        if (request.ConfirmPassword != request.Password)
            throw new Exception("Password don`t match!");
        
        // Validate DateOfBirth
        if (request.DateOfBirth == DateTime.MinValue)
            throw new Exception("DateOfBirth is required!");
        
        // Validate address
        if (request.Address.Length > 200)
            throw new Exception("Address can't be longer than 200 characters");
        
        var user = new User
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address
        };

        using (HMACSHA256 hmac = new())
        {
            user.PasswordSalt = hmac.Key;
        
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        }

        await _db.Users.AddAsync(user, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return user;
    }
}