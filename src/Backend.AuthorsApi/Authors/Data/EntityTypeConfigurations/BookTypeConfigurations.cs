using Authors.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authors.Data.EntityTypeConfigurations;

public class BookTypeConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);
        
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.Name).HasColumnName("name");
        builder.Property(a => a.PublicAt).HasColumnName("public_at")
            .HasColumnType("date");
        builder.Property(a => a.Genre).HasColumnName("genre");
    }
}