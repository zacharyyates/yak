using System.Text;
using System.Text.RegularExpressions;

namespace System.Html
{
    public class WhiteSpaceFilter
    {
        static Regex m_RemoveWhitespace = new Regex(@"(?<=\s)[\s]+", RegexOptions.Compiled);

        public static string Filter(string html)
        {
            var output = new StringBuilder(m_RemoveWhitespace.Replace(html, string.Empty));
            output = output.Replace("\r", string.Empty);
            return output.ToString();
        }

        // todo: look at this for further system.web enhancement http://cestdumeleze.net/blog/2011/minifying-the-html-with-asp-net-mvc-and-razor/
    }
}