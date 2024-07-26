using Microsoft.EntityFrameworkCore;
using Users.Data.EntityTypeConfigurations;
using Users.Entities.Database;
using Users.Interfaces;

namespace Users.Data;

public class UsersDbContext : DbContext, IUsersDbContext
{
    public DbSet<User> Users { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserTypeConfigurations());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}