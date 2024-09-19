using System.ComponentModel.DataAnnotations;

namespace WebTech.Domain.Entities.Database;

public abstract class DatabaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}