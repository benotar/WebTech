using Microsoft.EntityFrameworkCore;
using WebTech.Domain.Entities.Database;

namespace WebTech.Application.Interfaces.Persistence;

public interface IWebTechDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Author> Authors { get; set; }
    DbSet<Book> Books { get; set; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}