using Microsoft.EntityFrameworkCore;
using WebTech.Domain.Entities.Database;

namespace WebTech.Application.Interfaces.Persistence;

public interface IWebTechDbContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}