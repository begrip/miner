using System;
using System.Collections.Generic;
using System.Linq;

namespace Buzzilio.Begrip.Utilities.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether source string contains target string.
        /// </summary>
        public static bool StringContains(this string source, string target, StringComparison comparisonType)
        {
            return source.IndexOf(target, comparisonType) >= 0;
        }

        /// <summary>
        /// Return a list of string tokens based on a specified delimiter.
        /// </summary>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<string> GetTokensList(this string input, char delimiter)
        {
            List<string> tokens;
            if (input != null)
            {
                tokens = new List<string>(input.Split(delimiter));
            }
            else
            {
                tokens = new List<string>();
            }
            // Clear empty strings.
            tokens = tokens.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            return tokens;
        }
    }
}
