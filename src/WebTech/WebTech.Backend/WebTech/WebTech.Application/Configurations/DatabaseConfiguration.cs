namespace WebTech.Application.Configurations;

public class DatabaseConfiguration
{
    public static readonly string ConfigurationKey = "Database";

    public string ConnectionStringPattern { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }    
}