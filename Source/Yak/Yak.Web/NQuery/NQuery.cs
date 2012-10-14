using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace Yak.Web.NQuery
{
    public class NQuery : INQuery
    {
        protected HtmlDocument Document { get; set; }
        protected List<HtmlNode> Nodes { get; set; }

        // disable public constructor
        protected NQuery(HtmlDocument document)
        {
            document.ThrowIfNull("document");

            Document = document;
            Nodes = Document.DocumentNode.DescendantsAndSelf().ToList();
        }
        protected NQuery(HtmlDocument document, params HtmlNode[] nodes)
        {
            document.ThrowIfNull("document");
            nodes.ThrowIfNullOrEmpty("nodes");

            Document = document;
            Nodes = new List<HtmlNode>(nodes);
        }
        
        // delegate to HtmlDocument.Load methods
        public static NQuery Load(HtmlDocument document)
        {
            return new NQuery(document);
        }
        public static NQuery Load(Stream stream)
        {
            stream.ThrowIfNull("stream");

            var doc = new HtmlDocument();
            doc.Load(stream);

            return new NQuery(doc);
        }
        public static NQuery LoadHtml(string html)
        {
            html.ThrowIfNullOrWhiteSpace("html");

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return new NQuery(doc);
        }

        void ThrowIfDocumentIsNull()
        {
            if (Document.IsNull() || Document.DocumentNode.IsNull())
                throw new InvalidOperationException("Document is not loaded.");
        }

        HtmlNode m_First
        {
            get { return Nodes.FirstOrDefault(); }
        }

        #region ISelectable Implementation

        public INQuery Select(string selector)
        {
            ThrowIfDocumentIsNull();
            selector.ThrowIfNullOrEmpty("selector");

            Nodes = Document.DocumentNode.QuerySelectorAll(selector).ToList();
            return this;
        }

        public INQuery this[string selector]
        {
            get { return this.Select(selector); }
        }

        #endregion

        #region IHtmlElement Implementation

        public string Html()
        {
            ThrowIfDocumentIsNull();

            if (!m_First.IsNull())
                return m_First.OuterHtml;
            
            return null;
        }
        public INQuery Html(string value)
        {
            throw new NotImplementedException();
        }

        public string Attr(string name)
        {
            name.ThrowIfNullOrEmpty("name");
            ThrowIfDocumentIsNull();

            if (!m_First.IsNull())
            {
                var attrib = m_First.Attributes[name];
                if (!attrib.IsNull())
                    return attrib.Value;
            }
           
            return null;
        }
        public INQuery Attr<T>(string name, T value)
        {
            throw new NotImplementedException();
        }        

        public string Text()
        {
            ThrowIfDocumentIsNull();

            if (!m_First.IsNull())
                return m_First.InnerText;

            return null;
        }

        public INQuery Text(string value)
        {
            throw new NotImplementedException();
        }

        public T Val<T>()
        {
            throw new NotImplementedException();
        }

        public INQuery Val(params string[] values)
        {
            throw new NotImplementedException();
        }

        public INQuery AddClass(string @class)
        {
            throw new NotImplementedException();
        }

        public INQuery RemoveClass(string @class)
        {
            throw new NotImplementedException();
        }

        public INQuery ToggleClass(string @class)
        {
            throw new NotImplementedException();
        }

        public string Css(string name)
        {
            throw new NotImplementedException();
        }

        public INQuery Css(string name, string value)
        {
            throw new NotImplementedException();
        }

        public int Height()
        {
            throw new NotImplementedException();
        }

        public INQuery Height(int value)
        {
            throw new NotImplementedException();
        }

        public int Width()
        {
            throw new NotImplementedException();
        }

        public INQuery Width(int value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IManipulable Implementation

        public INQuery Append(string content)
        {
            throw new NotImplementedException();
        }

        public INQuery AppendTo(string content)
        {
            throw new NotImplementedException();
        }

        public INQuery Prepend(string content)
        {
            throw new NotImplementedException();
        }

        public INQuery PrependTo(string content)
        {
            throw new NotImplementedException();
        }

        public INQuery After(string content)
        {
            throw new NotImplementedException();
        }

        public INQuery Before(string content)
        {
            throw new NotImplementedException();
        }

        public INQuery InsertAfter(string content)
        {
            throw new NotImplementedException();
        }

        public INQuery InsertBefore(string content)
        {
            throw new NotImplementedException();
        }

        public INQuery Wrap(string html)
        {
            throw new NotImplementedException();
        }

        public INQuery WrapAll(string html)
        {
            throw new NotImplementedException();
        }

        public INQuery WrapInner(string html)
        {
            throw new NotImplementedException();
        }

        public INQuery ReplaceWith(string content)
        {
            throw new NotImplementedException();
        }

        public INQuery ReplaceAll(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Empty()
        {
            throw new NotImplementedException();
        }

        public INQuery Remove(string selector)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITraversable Implementation

        public INQuery Eq(int index)
        {
            throw new NotImplementedException();
        }

        public INQuery HasClass(string @class)
        {
            throw new NotImplementedException();
        }

        public INQuery Filter(int index)
        {
            throw new NotImplementedException();
        }

        public INQuery Filter(Action<bool> predicate)
        {
            throw new NotImplementedException();
        }

        public INQuery Is(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Map()
        {
            throw new NotImplementedException();
        }

        public INQuery Not(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Slice(int start, int end)
        {
            throw new NotImplementedException();
        }

        public INQuery Add(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Children(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Contents()
        {
            throw new NotImplementedException();
        }

        public INQuery Find(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Next(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery NextAll(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Parent(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Parents(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Prev(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery PrevAll(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery Siblings(string selector)
        {
            throw new NotImplementedException();
        }

        public INQuery First()
        {
            return new NQuery(Document, Nodes.FirstOrDefault());
        }

        public INQuery Last()
        {
            return new NQuery(Document, Nodes.LastOrDefault());
        }

        public INQuery Each(Action<int, INQuery> action)
        {
            for (int i = 0; i < Nodes.Count(); i++)
                action(i, new NQuery(Document, Nodes[i]));

            return this;
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<INQuery> GetEnumerator()
        {
            return IterateImpl().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return IterateImpl().GetEnumerator();
        }

        IEnumerable<INQuery> IterateImpl()
        {
            foreach (var node in Nodes)
                yield return new NQuery(Document, node);
        }

        #endregion
    }
}