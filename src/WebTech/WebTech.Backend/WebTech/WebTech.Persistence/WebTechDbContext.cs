using Microsoft.EntityFrameworkCore;
using WebTech.Application.Interfaces.Persistence;
using WebTech.Domain.Entities.Database;

namespace WebTech.Persistence;

public class WebTechDbContext(DbContextOptions<WebTechDbContext> options)
    : DbContext(options), IWebTechDbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}