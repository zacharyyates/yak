
namespace Yak.Web.NQuery
{
    public interface IHtmlElement
    {
        /// <summary>
        /// Get the value of an attribute for the first element in the set of matched elements.
        /// </summary>
        /// <param name="name">The name of the attribute to get.</param>
        /// <returns></returns>
        string Attr(string name);
        /// <summary>
        /// Set one or more attributes for the set of matched elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name of the attribute to set.</param>
        /// <param name="value">A value to set for the attribute.</param>
        /// <returns></returns>
        INQuery Attr<T>(string name, T value);

        string Html();
        INQuery Html(string value);

        string Text();
        INQuery Text(string value);

        T Val<T>();
        /// <summary>
        /// Set the value of each element in the set of matched elements.
        /// </summary>
        /// <param name="values">A string of text or an array of strings corresponding to the value of each matched element to set as selected/checked.</param>
        /// <returns></returns>
        INQuery Val(params string[] values);

        INQuery AddClass(string @class);
        INQuery RemoveClass(string @class);
        INQuery ToggleClass(string @class);

        string Css(string name);
        INQuery Css(string name, string value);

        int Height();
        INQuery Height(int value);

        int Width();
        INQuery Width(int value);
    }
}