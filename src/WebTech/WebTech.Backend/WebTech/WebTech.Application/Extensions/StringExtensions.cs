namespace WebTech.Application.Extensions;

public static class StringExtensions
{
    public static string ToValidPropertyName(this string str)
    {
        return string.IsNullOrEmpty(str)
            ? str
            : char.ToUpper(str[0]) + str[1..];
    }
    public static string ToValidEntityIdPropertyName(this string str)
    {
        if (!str.Contains("Id") || string.IsNullOrEmpty(str))
        {
            throw new ArgumentException("The provided string must contain 'Id'.", nameof(str));
        }

        return "Id";
    }
}