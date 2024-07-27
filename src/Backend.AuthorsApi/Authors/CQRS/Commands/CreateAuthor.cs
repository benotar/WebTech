using Authors.Entities.Database;
using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.CQRS.Commands;

public class CreateAuthorCommand : IRequest<(string message, bool isCreated)>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, (string message, bool isCreated)>
{
    private readonly IAuthorsDbContext _db;

    public CreateAuthorCommandHandler(IAuthorsDbContext db)
    {
        _db = db;
    }

    public async Task<(string message, bool isCreated)> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _db.Authors
            .AnyAsync(a => a.FirstName == request.FirstName && a.LastName == request.LastName,
                cancellationToken);
        
        if (existingAuthor)
        {
            return ($"Author \'{request.FirstName} {request.LastName}\' already exists!", false);
        }

        var newAuthor = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth
        };

        _db.Authors.Add(newAuthor);

        await _db.SaveChangesAsync(cancellationToken);

        return ($"The author \'{request.FirstName} {request.LastName}\' has been successfully created!",true);
    }
}