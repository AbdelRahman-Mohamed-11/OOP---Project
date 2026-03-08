namespace LibraryManagementProject;

public static class StringExtensions
{
    public static string NormalizeId(this string id)
        => id?.Trim().ToUpperInvariant()!;

    public static bool IsValidEmail(this string value)
    {
        if(string.IsNullOrEmpty(value)) return false;

        bool hasAt = false;

        bool hasDot = false;

        for(int i = 0; i < value.Length; i++)
        {
            if (value[i] == '@') hasAt = true;

            if (value[i] == '.') hasDot = true;
        }

        return hasAt && hasDot;
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
