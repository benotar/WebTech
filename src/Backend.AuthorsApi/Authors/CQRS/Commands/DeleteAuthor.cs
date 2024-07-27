using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.CQRS.Commands;

public record DeleteAuthorCommandResult(string Message, bool IsSuccess);
public class DeleteAuthorCommand : IRequest<DeleteAuthorCommandResult>
{
    public int AuthorId { get; set; }
}

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, DeleteAuthorCommandResult>
{
    private readonly IAuthorsDbContext _db;

    public DeleteAuthorCommandHandler(IAuthorsDbContext db)
    {
        _db = db;
    }
    
    public async Task<DeleteAuthorCommandResult> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _db.Authors
            .Include(a => a.Books)
            .Where(a => a.Id == request.AuthorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingAuthor is null)
        {
            return new DeleteAuthorCommandResult($"Author with ID {request.AuthorId} not found.", false);
        }

        _db.Authors.Remove(existingAuthor);

        await _db.SaveChangesAsync(cancellationToken);

        return new DeleteAuthorCommandResult(
            $"The author \'{existingAuthor.FirstName} {existingAuthor.LastName}\' has been successfully deleted!",
            true);
    }
}
