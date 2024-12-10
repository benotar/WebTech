using System.ComponentModel.DataAnnotations;

namespace WebTech.WebApi.Models.Authentication;

public class LoginRequestModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Fingerprint { get; set; }
}