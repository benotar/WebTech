using Authors.Entities.Database;

namespace Authors.Models;

public class GetAuthorsResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public List<Book> Books = new();
}