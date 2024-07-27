using Authors.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authors.Data.EntityTypeConfigurations;

public class AuthorTypeConfigurations : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);
        
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.FirstName).HasColumnName("first_name");
        builder.Property(a => a.LastName).HasColumnName("last_name");
        builder.Property(a => a.DateOfBirth).HasColumnName("date_of_birth")
            .HasColumnType("date");
    }
}