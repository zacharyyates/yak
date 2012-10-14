using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace System.Text
{
    public static class RegexExtensions
    {
        // todo: add tests for everything in this class
        public static Regex ToRegex(this string pattern)
        {
            pattern.ThrowIfNullOrWhiteSpace("pattern");

            return new Regex(pattern);
        }

        public static string Capture(this string pattern, string input, int index)
        {
            pattern.ThrowIfNullOrWhiteSpace("pattern");
            input.ThrowIfNullOrWhiteSpace("input");

            var regex = new Regex(pattern);
            var match = regex.Match(input);

            if (index >= match.Captures.Count)
                throw new ArgumentOutOfRangeException("index", index, "There are only {0} captures.".FormatWith(match.Captures.Count));
            
            return match.Captures[index].Value;
        }

        public static IEnumerable<string> AllCaptures(this string pattern, string input)
        {
            pattern.ThrowIfNullOrWhiteSpace("pattern");
            input.ThrowIfNullOrWhiteSpace("input");

            var regex = new Regex(pattern);
            var matches = regex.Matches(input);

            foreach (Match match in matches)
                foreach (Capture capture in match.Captures)
                    yield return capture.Value;
        }
    }
}