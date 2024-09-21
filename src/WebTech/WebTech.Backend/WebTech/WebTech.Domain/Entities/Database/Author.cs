using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebTech.Domain.Entities.Database;

public class Author : DatabaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    [JsonIgnore] public ICollection<Book> Books = new List<Book>();
}