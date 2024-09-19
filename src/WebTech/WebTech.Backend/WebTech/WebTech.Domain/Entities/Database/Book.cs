using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebTech.Domain.Entities.Database;

public class Book : DatabaseEntity
{
    [Required]
    [MaxLength(128)]
    [MinLength(3)]
    public string Title { get; set; }

    [Required]
    [MaxLength(48)]
    [MinLength(3)]
    public string Genre { get; set; }

    [Required] public int PublicationYear { get; set; }

    public Guid AuthorId { get; set; }

    [JsonIgnore] public Author Author { get; set; }
}