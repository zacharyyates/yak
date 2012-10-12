﻿using System.Text;
using System.Text.RegularExpressions;

namespace System.Html
{
    public class HtmlCompactor
    {
        static Regex m_RemoveWhitespace = new Regex(@"(?<=\s)[\s]+", RegexOptions.Compiled);

        public static string Compact(string html)
        {
            var output = new StringBuilder(m_RemoveWhitespace.Replace(html, string.Empty));
            output = output.Replace("\r", string.Empty);
            return output.ToString();
        }
    }
}