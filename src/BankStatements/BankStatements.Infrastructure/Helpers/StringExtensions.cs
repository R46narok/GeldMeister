using System.Globalization;

namespace BankStatements.Infrastructure.Helpers;

public static class StringExtensions
{
    public static string ToTitleCaseTrimmed(this string str)
    {
        var culture = CultureInfo.CurrentCulture;
        var textInfo = culture.TextInfo;

        return textInfo.ToTitleCase(str).Replace(" ", "");
    }
}