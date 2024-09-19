using System.ComponentModel.DataAnnotations;
using WebTech.Application.Interfaces.Providers;

namespace WebTech.Application.Common.ValidationAttributes;

public class PublicationYearAttribute : ValidationAttribute
{
    private const int MinYear = 1900;
    private readonly int _currentYear;
    private readonly IDateTimeProvider _dateTimeProvider;

    public PublicationYearAttribute(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;

        _currentYear = _dateTimeProvider.UtcNow.Year;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not int year)
        {
            return new ValidationResult("The year must be specified.");
        }

        if (year < MinYear || year > _currentYear)
        {
            return new ValidationResult($"The year must be between {MinYear} and {_currentYear}.");
        }

        return ValidationResult.Success;
    }
}