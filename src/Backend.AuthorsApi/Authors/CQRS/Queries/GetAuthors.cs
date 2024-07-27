using Authors.Entities.Database;
using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.CQRS.Queries;

public record GetAuthorsQueryResult(ICollection<Author> Authors, string Message, bool IsSuccess);
public record GetAuthorsQuery : IRequest<GetAuthorsQueryResult>;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, GetAuthorsQueryResult>
{
    private readonly IAuthorsDbContext _db;
    public GetAuthorsQueryHandler(IAuthorsDbContext db)
    {
        _db = db;
    }


    public async Task<GetAuthorsQueryResult> Handle(
        GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _db.Authors
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return existingAuthor is null
            ? new GetAuthorsQueryResult(null, "An error occurred while retrieving the list of authors!", false)
            : new GetAuthorsQueryResult(existingAuthor, "Success", true);
    }
}