namespace Switcheroo.Extensions
{
    internal static class StringExtensions
    {
        public static string PrepareForDisplay(this string str, int maxLength, bool pad)
        {
            str = str ?? string.Empty;
            str = str.Length > maxLength
                ? str.Substring(0, maxLength)
                : pad ? str.PadRight(maxLength, ' ') : str;

            return str;
        }
    }
}
