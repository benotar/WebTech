using Authors.Entities.Database;
using Authors.Interfaces;
using Authors.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.SQRS.Queries;

public class GetAuthorsBooksQuery : IRequest<(ICollection<Author> authors, (string message, bool isSuccess))>
{
}

public class GetAuthorsBooksQueryHandler : IRequestHandler<GetAuthorsBooksQuery, (ICollection<Author> authors, (string
    message, bool isSuccess))>
{
    private readonly IAuthorsDbContext _db;

    public GetAuthorsBooksQueryHandler(IAuthorsDbContext db)
    {
        _db = db;
    }


    public async Task<(ICollection<Author> authors, (string message, bool isSuccess))> Handle(
        GetAuthorsBooksQuery request, CancellationToken cancellationToken)
    {
        var authorsWithBooks = await _db.Authors
            // .Include(a => a.Books)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return authorsWithBooks is null
            ? (null, ("An error occurred while retrieving the list of authors!", false))
            : (authorsWithBooks, ("Success", true));
    }
}