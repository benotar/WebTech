using System.ComponentModel.DataAnnotations;
using WebTech.Application.Common.ValidationAttributes;

namespace WebTech.WebApi.Models.Book;

public class CreateOrUpdateBookRequestModel
{
    [Required]
    [MaxLength(88)]
    [MinLength(3)]
    public string Title { get; set; }

    [Required]
    [MaxLength(88)]
    [MinLength(3)]
    public string Genre { get; set; }

    [Required]
    [PublicationYear]
    public int PublicationYear { get; set; }

    [Required]
    [MaxLength(88)]
    [MinLength(3)]
    public string AuthorFirstName { get; set; }
    
    [Required]
    [MaxLength(88)]
    [MinLength(3)]
    public string AuthorLastName { get; set; }
}