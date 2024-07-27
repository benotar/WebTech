using Authors.Entities.Database;
using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.CQRS.Commands;

public record CreateBookCommandResult(string Message, bool IsCreated);

public class CreateBookCommand()
    : IRequest<CreateBookCommandResult>
{
    public string Name { get; set; }
    
    public int PublicAt { get; set; }
    
    public string Genre { get; set; }
    
    public int AuthorId { get; set; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreateBookCommandResult>
{
    private readonly IAuthorsDbContext _db;
    public CreateBookCommandHandler(IAuthorsDbContext db)
    {
        _db = db;
    }
    
    public async Task<CreateBookCommandResult> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _db.Authors
            .Where(a => a.Id == request.AuthorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingAuthor is null)
        {
            return new CreateBookCommandResult($"Author with ID {request.AuthorId} not found.", false);
        }

        var existingBook = await _db.Books
            .AnyAsync(b => b.Name == request.Name && b.PublicAt == request.PublicAt && b.Genre == request.Genre
                           && b.AuthorId == request.AuthorId, cancellationToken);

        if (existingBook)
        {
            return new CreateBookCommandResult(
                $"Book '{request.Name}' by Author: {existingAuthor.LastName} {existingAuthor.FirstName} already exists.",
                false);
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

        return new CreateBookCommandResult(
            $"The book '{request.Name}' by Author: {existingAuthor.LastName} {existingAuthor.FirstName} has been successfully created!",
            true);
    }
}