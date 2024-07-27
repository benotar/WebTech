using Authors.Entities.Database;
using Authors.Interfaces;
using Authors.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authors.SQRS.Queries;

public class GetAuthorsBooksQuery : IRequest<List<Author>>
{
    
}

public class GetAuthorsBooksQueryHandler : IRequestHandler<GetAuthorsBooksQuery, List<Author>>
{
    private readonly IAuthorsDbContext _db;

    public GetAuthorsBooksQueryHandler(IAuthorsDbContext db)
    {
        _db = db;
    }


    public async Task<List<Author>> Handle(GetAuthorsBooksQuery request, CancellationToken cancellationToken)
    {
        var authorsWithBooks = await _db.Authors
            .Include(a => a.Books)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (authorsWithBooks is null)
        {
            throw new Exception("Unable to get author's books");
        }

        // var result = new List<GetAuthorsResponse>();
        //
        // foreach (var author in authorsWithBooks)
        // {
        //     var responseAuthor = new GetAuthorsResponse
        //     {
        //         Id = author.Id,
        //         FirstName = author.FirstName,
        //         LastName = author.LastName,
        //         DateOfBirth = author.DateOfBirth
        //     };
        //
        //     foreach (var book in author.Books)
        //     {
        //         responseAuthor.Books.Add(book);
        //     }
        //     result.Add(responseAuthor);
        // }

        //return result;

        return authorsWithBooks;
    }
}