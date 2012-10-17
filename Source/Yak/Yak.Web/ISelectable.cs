
namespace Yak.Web
{
    public interface ISelectable
    {
        INQuery Select(string selector);
    }
}