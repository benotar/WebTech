using Authors.Entities.Database;
using Authors.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.CQRS.Queries;

public record GetBooksQueryResult(ICollection<Book> Authors, string Message, bool IsSuccess);
public record GetBooksQuery : IRequest<GetBooksQueryResult>;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, GetBooksQueryResult>
{
    private readonly IAuthorsDbContext _db;

    public GetBooksQueryHandler(IAuthorsDbContext db)
    {
        _db = db;
    }

    public async Task<GetBooksQueryResult> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _db.Books
            .Include(b => b.Author)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return books is null
            ? new GetBooksQueryResult(null, "An error occurred while retrieving the list of books!", false)
            : new GetBooksQueryResult(books, "Success", true);
    }
}
