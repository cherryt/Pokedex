using System.Text.RegularExpressions;

namespace Pokedex.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceSpecialCharactersWithSpace(this string word)
        {
            return Regex.Replace(word, @"[^0-9a-zA-Zé\._,& ]", " ");
        }
    }
}