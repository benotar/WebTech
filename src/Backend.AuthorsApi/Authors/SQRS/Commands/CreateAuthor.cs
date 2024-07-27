using Authors.Interfaces;
using MediatR;

namespace Authors.SQRS.Commands;

public class CreateAuthorCommand : IRequest<bool>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, bool>
{
    private readonly IAuthorsDbContext _db;

    public CreateAuthorCommandHandler(IAuthorsDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}