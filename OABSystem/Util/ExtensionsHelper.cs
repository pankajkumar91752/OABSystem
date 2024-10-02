using System.Text.RegularExpressions;

namespace OABSystem.Util
{
    public static class ExtensionsHelper
    {
        public static string ToSeparatedString(this string input)
        {
            return Regex.Replace(input, @"([a-z])([A-Z])", "$1 $2").Replace(".", " ");
        }
    }
}