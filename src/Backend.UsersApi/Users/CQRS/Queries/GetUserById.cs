using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Entities.Database;
using Users.Interfaces;

namespace Users.CQRS.Queries;

public record GetUserByIdQueryResult(User? User, string Message, bool IsSuccess);

public class GetUserByIdQuery : IRequest<GetUserByIdQueryResult>
{
    public int Id;
}
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResult>
{
    private readonly IUsersDbContext _db;

    public GetUserByIdQueryHandler(IUsersDbContext db)
    {
        _db = db;
    }

    public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var existingUser = await _db.Users.Where(u => u.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        return existingUser is null
            ? new GetUserByIdQueryResult(null, $"User with ID {request.Id} not found!", false)
            : new GetUserByIdQueryResult(existingUser, $"User with ID {request.Id} successfully found!", true);
    }
}