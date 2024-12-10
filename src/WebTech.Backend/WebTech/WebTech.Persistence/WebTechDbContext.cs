using Microsoft.EntityFrameworkCore;
using WebTech.Application.Interfaces.Persistence;
using WebTech.Domain.Entities.Database;
using WebTech.Persistence.EntityTypeConfigurations;

namespace WebTech.Persistence;

public class WebTechDbContext(DbContextOptions<WebTechDbContext> options)
    : DbContext(options), IWebTechDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }
    
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}