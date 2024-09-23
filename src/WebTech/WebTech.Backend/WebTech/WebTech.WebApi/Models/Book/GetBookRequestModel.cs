using System.ComponentModel.DataAnnotations;

namespace WebTech.WebApi.Models.Book;

public class GetBookRequestModel
{
    [Required]
    public Guid BookId {get; set;}
    
    [Required]
    [MaxLength(88)]
    [MinLength(3)]
    public string AuthorFirstName {get; set;} 
    
    [Required]
    [MaxLength(88)]
    [MinLength(3)]
    public string AuthorLastName {get; set;}
}