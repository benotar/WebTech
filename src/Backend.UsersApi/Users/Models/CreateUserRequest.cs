using System.ComponentModel.DataAnnotations;

namespace Users.Models;

public class CreateUserRequest
{
    [MaxLength(50, ErrorMessage = "Username can't be longer than 50 characters!")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Password don`t match!")]
    public string ConfirmPassword { get; set; }
    
    
    [Required(ErrorMessage = "User first name is required")]
    [MaxLength(100, ErrorMessage = "FirstName can't be longer than 100 characters!")]
    public string FirstName { get; set; }
    
    
    [Required(ErrorMessage = "User last name is required")]
    [MaxLength(100, ErrorMessage = "LastName can't be longer than 100 characters!")]
    public string LastName { get; set; }
    
    
    [Required(ErrorMessage = "User birth date is required")]
    public DateTime BirthDate { get; set; }
    
    [Required(ErrorMessage = "User address is required")]
    public string Address { get; set; }
}