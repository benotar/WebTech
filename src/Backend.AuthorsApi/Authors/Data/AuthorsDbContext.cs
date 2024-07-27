using Authors.Data.EntityTypeConfigurations;
using Authors.Entities.Database;
using Authors.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authors.Data;

public class AuthorsDbContext : DbContext, IAuthorsDbContext
{
    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    public AuthorsDbContext(DbContextOptions<AuthorsDbContext> options)
        : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorTypeConfigurations());
        modelBuilder.ApplyConfiguration(new BookTypeConfigurations());
        
        base.OnModelCreating(modelBuilder);
    }
}