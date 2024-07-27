using System.ComponentModel.DataAnnotations;

namespace Authors.Models;

public class CreateRequest
{
    [Required(ErrorMessage = "Author first name is required!")]
    [MaxLength(100, ErrorMessage = "Author first name can't be longer than 100 characters!")]
    public string AuthorFirstName { get; set; }
    
    [Required(ErrorMessage = "Author last name is required!")]
    [MaxLength(100, ErrorMessage = "Author last name can't be longer than 100 characters!")]
    public string AuthorLastName { get; set; }
    
    [Required(ErrorMessage = "Author birth date is required!")]
    [DataType(DataType.Date)]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid birth date format. Expected format is yyyy-MM-dd.")]
    public string? AuthorDateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Book name is required!")]
    [MaxLength(100, ErrorMessage = "Book name can't be longer than 100 characters!")]
    public string BookName { get; set; }
    
    [Required(ErrorMessage = "Book publication date is required!")]
    [DataType(DataType.Date)]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Invalid birth date format. Expected format is yyyy.")]
    public int BookPublicAt { get; set; }
    
    [Required(ErrorMessage = "Book genre is required!")]
    public string BookGenre { get; set; }
}