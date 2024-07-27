using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.CQRS.Commands;


public record UpdateBookCommandResult(string Message, bool IsSuccess);
public class UpdateBookCommand : IRequest<UpdateBookCommandResult>
{
    public int BookId { get; set; }
    public string Name { get; set; }
    public int PublicAt { get; set; }
    public string Genre { get; set; }
    public int AuthorId { get; set; }
}

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdateBookCommandResult>
{
    private readonly IAuthorsDbContext _db;

    public UpdateBookCommandHandler(IAuthorsDbContext db)
    {
        _db = db;
    }

    public async Task<UpdateBookCommandResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {

        var existingBook = await _db.Books
            .Where(b => b.Id == request.BookId)
            .AsTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (existingBook is null)
        {
            return new UpdateBookCommandResult($"Book with ID {request.BookId} not found.", false);
        }

        var existingAuthor = await _db.Authors.AsTracking()
            .Where(a => a.Id == request.AuthorId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (existingAuthor is null)
        {
            return new UpdateBookCommandResult($"Author with ID {request.AuthorId} not found.", false);
        }

        existingBook.Name = request.Name;
        existingBook.PublicAt = request.PublicAt;
        existingBook.Genre = request.Genre;
        existingBook.AuthorId = existingAuthor.Id;
        
        _db.Books.Update(existingBook);

        await _db.SaveChangesAsync(cancellationToken);

        return new UpdateBookCommandResult($"The book \'{request.Name}\' has been successfully updated!", true);
    }
}