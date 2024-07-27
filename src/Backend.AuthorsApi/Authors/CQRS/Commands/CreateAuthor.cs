using Authors.Entities.Database;
using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.CQRS.Commands;

public class CreateAuthorCommand : IRequest<Author>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
{
    private readonly IAuthorsDbContext _db;

    public CreateAuthorCommandHandler(IAuthorsDbContext db)
    {
        _db = db;
    }

    public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _db.Authors
            .AnyAsync(a => a.FirstName == request.FirstName && a.LastName == request.LastName,
                cancellationToken);
        
        if (existingAuthor)
        {
            throw new Exception($"Author \'{request.FirstName} {request.LastName}\' already exists!");
        }

        var newAuthor = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth
        };

        _db.Authors.Add(newAuthor);

        await _db.SaveChangesAsync(cancellationToken);

        return newAuthor;
    }
}