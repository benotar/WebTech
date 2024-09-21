namespace WebTech.Application.Configurations;

public class RefreshTokenSessionConfiguration
{
    public static readonly string ConfigurationKey = "RefreshTokenSession";
    
    public int ExpirationDays { get; set; }
}