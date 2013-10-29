using System.Text;
using System.Text.RegularExpressions;
using Yak.Text.RegularExpressions;

namespace Yak.Html {
    public class WhiteSpaceFilter {
        static readonly Regex RemoveWhitespaceRegex = @"(?<=\s)[\s]+".Compile();

        public static string Filter(string html) {
            var output = new StringBuilder(RemoveWhitespaceRegex.Replace(html, string.Empty));
            output = output.Replace("\r", string.Empty);
            return output.ToString();
        }

        // todo: look at this for further system.web enhancement http://cestdumeleze.net/blog/2011/minifying-the-html-with-asp-net-mvc-and-razor/
    }
}