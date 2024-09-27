namespace WebTech.Application.Configurations;

public class CookiesConfiguration
{
    public static readonly string ConfigurationKey = "Cookies";
    
    public string RefreshTokenCookiesKey { get; set; }
    
    public string FingerprintCookiesKey { get; set; }
}