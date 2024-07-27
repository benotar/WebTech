using System.ComponentModel.DataAnnotations;

namespace Authors.Models;

public class CreateBookRequest
{
    [Required(ErrorMessage = "Book name is required!")]
    [MaxLength(100, ErrorMessage = "Book name can't be longer than 100 characters!")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Author birth date is required!")]
    [DataType(DataType.Date)]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid birth date format. Expected format is yyyy-MM-dd.")]
    public string? PublicAt { get; set; }
    
    public string Genre { get; set; }
    
    public int AuthorId { get; set; }
}