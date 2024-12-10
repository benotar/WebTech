using System.Text.Json.Serialization;

namespace WebTech.Domain.Entities.Database;

public class Book : DatabaseEntity
{
    public string Title { get; set; }

    public string Genre { get; set; }

    public int PublicationYear { get; set; }

    public Guid AuthorId { get; set; }

    [JsonIgnore] public Author Author { get; set; }
}