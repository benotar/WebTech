using System.ComponentModel.DataAnnotations;
using WebTech.Application.Interfaces.Providers;

namespace WebTech.Application.Common.ValidationAttributes;

public class PublicationYearAttribute : ValidationAttribute
{
    private const int MinYear = 1900;
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var currentYear = DateTime.UtcNow.Year;
        
        if (value is not int year)
        {
            return new ValidationResult("The year must be specified.");
        }

        if (year < MinYear || year > currentYear)
        {
            return new ValidationResult($"The year must be between {MinYear} and {currentYear}.");
        }

        return ValidationResult.Success;
    }
}