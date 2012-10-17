using System.Collections.Generic;

namespace Yak.Web
{
    public interface INQuery : IHtmlElement, IManipulable, ITraversable, ISelectable, IEnumerable<INQuery>
    {
        INQuery this[string selector] { get; }
    }
}