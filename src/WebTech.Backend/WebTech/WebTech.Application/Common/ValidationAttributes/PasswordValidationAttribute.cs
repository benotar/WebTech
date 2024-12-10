﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebTech.Application.Common.ValidationAttributes;

public partial class PasswordValidationAttribute() : ValidationAttribute(DefaultErrorMessage)
{
    private const string DefaultErrorMessage =
        "Password must contain at least one uppercase letter, one lowercase letter, one number and a minimum of 8 characters.";

    private static readonly Regex PasswordRegex = MyRegex();

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || !PasswordRegex.IsMatch(value.ToString()))
        {
            return new ValidationResult(ErrorMessage ?? DefaultErrorMessage);
        }

        return ValidationResult.Success;
    }

    [GeneratedRegex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}