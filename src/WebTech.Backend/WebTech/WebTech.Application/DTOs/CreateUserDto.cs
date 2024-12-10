namespace WebTech.Application.DTOs;

public class CreateUserDto
{
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string Address { get; set; }
}