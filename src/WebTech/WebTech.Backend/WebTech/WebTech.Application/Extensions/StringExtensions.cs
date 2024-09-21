namespace WebTech.Application.Extensions;

public static class StringExtensions
{
    public static string ToValidUserNamePropertyName(this string str)
    {
        return string.IsNullOrEmpty(str)
            ? str
            : char.ToUpper(str[0]) + str[1..];
    }
    public static string ToValidEntityIdPropertyName(this string str)
    {
        return string.IsNullOrEmpty(str)
            ? str
            : str.Replace("entity", "").Trim();
    }
}