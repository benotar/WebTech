using System.ComponentModel.DataAnnotations;

namespace WebTech.WebApi.Models.Authentication;

public class LoginRequestModel
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string Fingerprint { get; set; }
}