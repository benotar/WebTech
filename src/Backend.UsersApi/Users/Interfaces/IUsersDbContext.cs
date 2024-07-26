using Microsoft.EntityFrameworkCore;
using Users.Entities.Database;

namespace Users.Interfaces;

public interface IUsersDbContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}