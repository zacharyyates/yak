
namespace Yak.Web.NQuery
{
    public interface IManipulable
    {
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
        INQuery Remove(string selector); // todo: lookup jquery doc
    }
}