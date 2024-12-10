using System.ComponentModel.DataAnnotations;

namespace WebTech.Application.Common.ValidationAttributes;

public class BirthDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateTime birthDate)
        {
            return new ValidationResult("Invalid birth date.");
        }

        return birthDate > DateTime.UtcNow
            ? new ValidationResult("Birth date cannot be in the future.")
            : ValidationResult.Success;
    }
}