using System.ComponentModel.DataAnnotations;

namespace Authors.Models;

public class CreateAuthorRequest
{
    [Required(ErrorMessage = "Author first name is required!")]
    [MaxLength(100, ErrorMessage = "Author first name can't be longer than 100 characters!")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Author last name is required!")]
    [MaxLength(100, ErrorMessage = "Author last name can't be longer than 100 characters!")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Author birth date is required!")]
    [DataType(DataType.Date)]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid birth date format. Expected format is yyyy-MM-dd.")]
    public string? DateOfBirth { get; set; }
}