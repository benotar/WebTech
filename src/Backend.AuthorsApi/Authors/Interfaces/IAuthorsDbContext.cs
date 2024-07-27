using Authors.Entities.Database;
using Microsoft.EntityFrameworkCore;

namespace Authors.Interfaces;

public interface IAuthorsDbContext
{
    DbSet<Author> Authors { get; set; }
    DbSet<Book> Books { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}