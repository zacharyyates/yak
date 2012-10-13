using System.Collections.Generic;

namespace Yak.Web.NQuery
{
    public interface INQuery : IHtmlElement, IManipulable, ITraversable, ISelectable, IEnumerable<INQuery>
    {
    }
}