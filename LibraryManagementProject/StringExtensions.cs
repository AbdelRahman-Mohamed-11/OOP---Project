using System.Text.RegularExpressions;

namespace LibraryManagementProject;

public static class StringExtensions
{
    public static string NormalizeId(this string id)
        => id?.Trim().ToUpperInvariant()!;

    public static bool IsValidEmail(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        // Email regex breakdown:
        // ^                 start of the string
        // [^@\s]+           local part (before @): one or more chars, not @ or whitespace
        // @                 required separator between local part and domain
        // [^@\s]+           domain name: one or more chars, not @ or whitespace
        // \.                literal dot before the top-level domain
        // [^@\s.]{2,}       TLD: at least 2 chars, no @, whitespace, or extra dots
        // $                 end of the string
        const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s.]{2,}$";

        return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
    }

    public static bool ContainDigit(this string value)
    {
        if(string.IsNullOrEmpty(value))
            return false;

        for(int i = 0; i < value.Length; i++)
        {
            if (char.IsDigit(value[i]))
                return true;
        }

        return false;
    }
}
