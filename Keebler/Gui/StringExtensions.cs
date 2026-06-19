using System.Globalization;
using System.Text.RegularExpressions;

namespace Keebler.Gui;

public static class StringExtensions
{
    public static string MakePretty(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return string.Empty;

        str = str.Trim().Replace('_', ' ');

        // Insert spaces between letters and digits: "NUMPAD0" -> "NUMPAD 0"
        str = Regex.Replace(str, @"(?<=[A-Za-z])(?=\d)|(?<=\d)(?=[A-Za-z])", " ");

        // Lowercase first, then title-case words for a friendlier UI label
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        var words = str.ToLowerInvariant()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return string.Join(" ", words.Select(textInfo.ToTitleCase));
    }
}