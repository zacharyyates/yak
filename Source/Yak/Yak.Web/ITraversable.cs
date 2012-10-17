using System;

namespace Yak.Web
{
    public interface ITraversable
    {
        INQuery Eq(int index);
        INQuery HasClass(string @class);
        INQuery Filter(int index);
        INQuery Filter(Action<bool> predicate);
        INQuery Is(string selector);
        INQuery Map(); // todo: read jq implementation for delegate signature
        INQuery Not(string selector);
        INQuery Slice(int start, int end);

        INQuery Add(string selector);
        INQuery Children(string selector);
        INQuery Contents();
        INQuery Find(string selector);
        INQuery Next(string selector);
        INQuery NextAll(string selector);
        INQuery Parent();
        INQuery Parent(string selector);
        INQuery Parents(string selector);
        INQuery Prev(string selector);
        INQuery PrevAll(string selector);
        INQuery Siblings(string selector);

        INQuery First();
        INQuery Last();
        INQuery Each(Action<int, INQuery> action);
    }
}