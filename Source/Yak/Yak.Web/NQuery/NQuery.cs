using System.Collections;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace System.Web.JQuery
{
    public class NQuery : IEnumerable<NQuery>
    {
        HtmlDocument m_Document;
        HtmlNode m_FirstNode;
        HtmlNodeCollection m_Nodes;

        public NQuery LoadHtml(string html)
        {
            m_Document = new HtmlDocument();
            m_Document.LoadHtml(html);
            m_FirstNode = m_Document.DocumentNode;
            
            return this;
        }

        public NQuery this[string selector]
        {
            get { throw new NotImplementedException(); }
        }

        void ThrowIfDocumentIsNull()
        {
            if (m_Document.IsNull() || m_Document.DocumentNode.IsNull())
                throw new InvalidOperationException("Document is not loaded.");
        }

        public string Html()
        {
            ThrowIfDocumentIsNull();

            if (!m_FirstNode.IsNull())
                return m_FirstNode.InnerHtml;
            else
                return null;
        }

        public string Attr(string name)
        {
            name.ThrowIfNullOrEmpty("name");
            ThrowIfDocumentIsNull();

            if (!m_FirstNode.IsNull())
            {
                var attrib = m_FirstNode.Attributes[name];
                if (!attrib.IsNull())
                    return attrib.Value;
            }

            return null;
        }

        #region IEnumerable Implementation

        public IEnumerator<NQuery> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}