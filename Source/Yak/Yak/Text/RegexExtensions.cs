using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static IList<Match> Matches(this string pattern, string input)
        {
            return MatchesImpl(pattern, input).ToList();
        }

        static IEnumerable<Match> MatchesImpl(string pattern, string input)
        {
            pattern.ThrowIfNullOrWhiteSpace("pattern");
            input.ThrowIfNullOrWhiteSpace("input");

            var regex = new Regex(pattern);
            var matches = regex.Matches(input);

            foreach (Match match in matches)
                yield return match;
        }

        public static IList<Capture> Captures(this string pattern, string input)
        {
            return CapturesImpl(pattern, input).ToList();
        }

        static IEnumerable<Capture> CapturesImpl(string pattern, string input)
        {
            var matches = MatchesImpl(pattern, input);

            foreach (Match match in matches)
                foreach (Capture capture in match.Captures)
                    yield return capture;
        }

        public static string Capture(this string pattern, string input, int matchIndex, int groupIndex, int captureIndex)
        {
            pattern.ThrowIfNullOrWhiteSpace("pattern");
            input.ThrowIfNullOrWhiteSpace("input");

            var regex = new Regex(pattern);
            var matches = regex.Matches(input);

            if (matchIndex >= matches.Count)
                throw new IndexOutOfRangeException("matchIndex: {0}, expected < {1}".FormatWith(matchIndex, matches.Count));

            var groups = matches[matchIndex].Groups;
            if (groupIndex >= groups.Count)
                throw new IndexOutOfRangeException("groupIndex: {0}, expected < {1}".FormatWith(groupIndex, groups.Count));

            var captures = groups[groupIndex].Captures;
            if (captureIndex >= captures.Count)
                throw new IndexOutOfRangeException("captureIndex: {0}, expected < {1}".FormatWith(captureIndex, captures.Count));

            return captures[captureIndex].Value;
        }
    }
}