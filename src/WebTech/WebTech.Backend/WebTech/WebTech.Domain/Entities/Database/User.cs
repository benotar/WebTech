namespace WebTech.Domain.Entities.Database;

public class User : DatabaseEntity
{
    public string UserName { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string Address { get; set; }

    public byte[] PasswordSalt { get; set; }

    public byte[] PasswordHash { get; set; }
}