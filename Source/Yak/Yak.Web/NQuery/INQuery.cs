using System;
using System.Collections.Generic;

namespace Yak.Web.NQuery
{
    public interface INQuery : IEnumerable<INQuery>
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

        INQuery Append(string content); // todo: add overload for INQuery objects
        INQuery AppendTo(string content);
        INQuery Prepend(string content);
        INQuery PrependTo(string content);
        INQuery After(string content);
        INQuery Before(string content);
        INQuery InsertAfter(string content);
        INQuery InsertBefore(string content);
        INQuery Wrap(string html); // todo: add overload for INQuery objects
        INQuery WrapAll(string html);
        INQuery WrapInner(string html);
        INQuery ReplaceWith(string content);
        INQuery ReplaceAll(string selector);
        INQuery Empty();
        INQuery Remove(string expr); // todo: lookup jquery doc

        INQuery Eq(int index);
        INQuery HasClass(string @class);
        INQuery Filter(int index);
        INQuery Filter(Action<bool> predicate);
        INQuery Is(string expr);
        INQuery Map(); // todo: read jq implementation for delegate signature
        INQuery Not(string expr);
        INQuery Slice(int start, int end);
        
        INQuery Add(string expr);
        INQuery Children(string expr);
        INQuery Contents();
        INQuery Find(string expr);
        INQuery Next(string expr);
        INQuery NextAll(string expr);
        INQuery Parent(string expr);
        INQuery Parents(string expr);
        INQuery Prev(string expr);
        INQuery PrevAll(string expr);
        INQuery Siblings(string expr);
    }
}