using System.Text.Json.Serialization;

namespace Authors.Entities.Database;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    //[JsonIgnore]
    public ICollection<Book> Books = new List<Book>();
}