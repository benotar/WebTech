namespace WebTech.Application.Extensions;

public static class StringExtensions
{
    public static string ToValidPropertyName(this string str)
    {
        return string.IsNullOrEmpty(str)
            ? str
            : char.ToUpper(str[0]) + str[1..];
    }
    public static string ToValidEntityIdPropertyName(this string str, bool isEntityForeignKey = false)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return isEntityForeignKey
            ? str.ToValidPropertyName()
            : str.Replace("entity", "", StringComparison.OrdinalIgnoreCase).Trim();
    }
}