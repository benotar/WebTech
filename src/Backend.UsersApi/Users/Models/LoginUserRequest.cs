using System.ComponentModel.DataAnnotations;

namespace Users.Models;

public class LoginUserRequest
{
    [MaxLength(50, ErrorMessage = "Username can't be longer than 50 characters!")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}