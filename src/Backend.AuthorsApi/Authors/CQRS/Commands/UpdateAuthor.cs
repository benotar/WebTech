using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.CQRS.Commands;

public record UpdateAuthorCommandResult(string Message, bool IsSuccess);
public class UpdateAuthorCommand : IRequest<UpdateAuthorCommandResult>
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdateAuthorCommandResult>
{
    private readonly IAuthorsDbContext _db;

    public UpdateAuthorCommandHandler(IAuthorsDbContext db)
    {
        _db = db;
    }

    public async Task<UpdateAuthorCommandResult> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _db.Authors.AsTracking()
            .Where(a => a.Id == request.AuthorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingAuthor is null)
        {
            return new UpdateAuthorCommandResult($"Author with ID {request.AuthorId} not found.", false);
        }

        existingAuthor.FirstName = request.FirstName;
        existingAuthor.LastName = request.LastName;
        existingAuthor.DateOfBirth = request.DateOfBirth;

        _db.Authors.Update(existingAuthor);

        await _db.SaveChangesAsync(cancellationToken);

        return new UpdateAuthorCommandResult($"The author \'{request.FirstName} {request.LastName}\' has been successfully updated!", true);
    }
}