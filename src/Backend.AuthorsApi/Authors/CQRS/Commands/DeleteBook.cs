using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.CQRS.Commands;

public record DeleteBookCommandResult(string Message, bool IsSuccess);
public class DeleteBookCommand : IRequest<DeleteBookCommandResult>
{
    public int BookId { get; set; }
}

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, DeleteBookCommandResult>
{
    private readonly IAuthorsDbContext _db;

    public DeleteBookCommandHandler(IAuthorsDbContext db)
    {
        _db = db;
    }

    public async Task<DeleteBookCommandResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var existingBook = await _db.Books
            .Where(b => b.Id == request.BookId)
                .Include(b => b.Author)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingBook is null)
        {
            return new DeleteBookCommandResult($"Book with ID {request.BookId} not found.", false);
        }

        _db.Books.Remove(existingBook);

        await _db.SaveChangesAsync(cancellationToken);

        return new DeleteBookCommandResult(
            $"The book '{existingBook.Name}' has been successfully deleted!",
            true);
    }
}