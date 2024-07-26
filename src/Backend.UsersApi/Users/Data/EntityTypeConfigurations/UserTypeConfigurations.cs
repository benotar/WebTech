using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Entities.Database;

namespace Users.Data.EntityTypeConfigurations;

public class UserTypeConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.Username).HasColumnName("user_name");
        builder.Property(u => u.FirstName).HasColumnName("first_name");
        builder.Property(u => u.LastName).HasColumnName("last_name");
        builder.Property(u => u.PasswordSalt).HasColumnName("password_salt");
        builder.Property(u => u.PasswordHash).HasColumnName("password_hash");
        builder.Property(u => u.DateOfBirth).HasColumnName("date_of_birth")
            .HasColumnType("date");
    }
}