using Authors.Entities.Database;
using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.SQRS.Commands;

public class CreateBookCommand : IRequest<bool>
{
    public string Name { get; set; }

    public int PublicAt { get; set; }

    public string Genre { get; set; }

    public int AuthorId { get; set; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
{
    private readonly IAuthorsDbContext _db;

    public CreateBookCommandHandler(IAuthorsDbContext db)
    {
        _db = db;
    }


    public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _db.Authors
            .Where(a => a.Id == request.AuthorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingAuthor is null)
        {
            throw new Exception($"Author with ID {request.AuthorId} not found.");
        }

        var existingBook = await _db.Books
            .AnyAsync(b => b.Name == request.Name && b.PublicAt == request.PublicAt && b.Genre == request.Genre
                           && b.AuthorId == request.AuthorId, cancellationToken);
        
        if (existingBook)
        {
            throw new Exception($"Book '{request.Name}' by Author: {existingAuthor.LastName} {existingAuthor.FirstName} already exists.");
        }

        var newBook = new Book
        {
            Name = request.Name,
            PublicAt = request.PublicAt,
            Genre = request.Genre,
            AuthorId = existingAuthor.Id
        };

        _db.Books.Add(newBook);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}