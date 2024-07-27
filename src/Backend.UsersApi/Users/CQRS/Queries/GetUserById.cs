using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Entities.Database;
using Users.Interfaces;

namespace Users.CQRS.Queries;

public class GetUserByIdQuery : IRequest<User>
{
    public int Id;
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUsersDbContext _db;

    public GetUserByIdQueryHandler(IUsersDbContext db)
    {
        _db = db;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Users.Where(u => u.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
    }
}