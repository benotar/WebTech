using Authors.Entities.Database;
using Authors.Interfaces;
using Authors.Models;
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
        var authors = await _db.Authors
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return authors is null
            ? new GetAuthorsQueryResult(null, "An error occurred while retrieving the list of authors!", false)
            : new GetAuthorsQueryResult(authors, "Success", true);
    }
}