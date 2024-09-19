using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebTech.Domain.Entities.Database;

public class Author : DatabaseEntity
{
    [Required]
    [MaxLength(48)]
    [MinLength(3)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(48)]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required] public DateTime BirthDate { get; set; }

    [JsonIgnore] public ICollection<Book> Books = new List<Book>();
}