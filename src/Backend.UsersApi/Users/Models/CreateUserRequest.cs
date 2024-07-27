using System.ComponentModel.DataAnnotations;

namespace Users.Models;

public class CreateUserRequest
{
    [Required(ErrorMessage = "Password is required!")]
    [MaxLength(50, ErrorMessage = "Username can't be longer than 50 characters!")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required!")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required!")]
    [Compare("Password", ErrorMessage = "Password don`t match!")]
    public string ConfirmPassword { get; set; }


    [Required(ErrorMessage = "User first name is required!")]
    [MaxLength(100, ErrorMessage = "FirstName can't be longer than 100 characters!")]
    public string FirstName { get; set; }


    [Required(ErrorMessage = "User last name is required!")]
    [MaxLength(100, ErrorMessage = "LastName can't be longer than 100 characters!")]
    public string LastName { get; set; }


    [Required(ErrorMessage = "User birth date is required!")]
    [DataType(DataType.Date)]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid birth date format. Expected format is yyyy-MM-dd.")]
    public string? BirthDate { get; set; }

    [Required(ErrorMessage = "User address is required!")]
    [MaxLength(100, ErrorMessage = "User address can't be longer than 100 characters!")]
    public string Address { get; set; }
}