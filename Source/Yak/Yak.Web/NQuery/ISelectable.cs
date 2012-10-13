
namespace Yak.Web.NQuery
{
    public interface ISelectable
    {
        INQuery Select(string selector);
    }
}