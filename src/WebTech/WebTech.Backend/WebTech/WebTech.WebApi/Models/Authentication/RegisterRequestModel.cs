using System.ComponentModel.DataAnnotations;
using WebTech.Application.Common.ValidationAttributes;

namespace WebTech.WebApi.Models.Authentication;

public class RegisterRequestModel
{
    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(48)]
    [MinLength(3)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(48)]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required] public DateTime BirthDate { get; set; }

    [Required] [MaxLength(128)] public string Address { get; set; }

    [Required] [PasswordValidation] public string Password { get; set; }
}