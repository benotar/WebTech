using System.ComponentModel.DataAnnotations;
using WebTech.Application.Common.ValidationAttributes;

namespace WebTech.WebApi.Models.Authors;

public class CreateAuthorRequestModel
{
    [Required]
    [MaxLength(88)]
    [MinLength(3)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(88)]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required]
    [BirthDate]
    public DateTime BirthDate { get; set; }
}