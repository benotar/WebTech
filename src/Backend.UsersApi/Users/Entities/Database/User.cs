namespace Users.Entities.Database;

public class User
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public byte[] PasswordSalt { get; set; }
    
    public byte[] PasswordHash { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string Address { get; set; }
}