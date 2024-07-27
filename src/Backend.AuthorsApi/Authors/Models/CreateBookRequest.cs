using System.ComponentModel.DataAnnotations;

namespace Authors.Models;

public class CreateBookRequest
{
    [Required(ErrorMessage = "Book name is required!")]
    [MaxLength(100, ErrorMessage = "Book name can't be longer than 100 characters!")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Book publication date is required!")]
    [DataType(DataType.Date)]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Invalid publication date format. Expected format is yyyy.")]
    public int PublicAt { get; set; }
    
    [Required(ErrorMessage = "Book genre is required!")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "AuthorId is required!")]
    public int AuthorId { get; set; }
}