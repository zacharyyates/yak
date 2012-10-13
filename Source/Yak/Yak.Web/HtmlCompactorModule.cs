using System.Html;
using System.IO;
using System.Text;

namespace System.Web
{
    /// <summary>
    /// Removes whitespace from an html response.
    /// </summary>
    // adapted from: http://madskristensen.net/post/A-whitespace-removal-HTTP-module-for-ASPNET-20.aspx
    public class HtmlCompactorModule : IHttpModule
    {
        #region IHttpModule Members

        // this is left empty because apparently unsubscribing from application events is pointless:
        // http://stackoverflow.com/questions/3424438/how-to-dispose-ihttpmodule-correctly
        void IHttpModule.Dispose() { }

        void IHttpModule.Init(HttpApplication application)
        {
            application.BeginRequest += context_BeginRequest;
        }

        #endregion

        void context_BeginRequest(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;
            if (app.Request.RawUrl.Contains(".html"))
            {
                app.Response.Filter = new HtmlCompactorStream(app.Response.Filter);
            }
        }

        #region Stream filter

        class HtmlCompactorStream : Stream
        {
            Stream m_Sink;
            
            public HtmlCompactorStream(Stream sink)
            {
                m_Sink = sink;
            }

            #region Delegated members

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return true; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override void Flush()
            {
                m_Sink.Flush();
            }

            public override long Length
            {
                get { return 0; }
            }

            public override long Position { get; set; }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return m_Sink.Read(buffer, offset, count);
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return m_Sink.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                m_Sink.SetLength(value);
            }

            public override void Close()
            {
                m_Sink.Close();
            }

            #endregion

            public override void Write(byte[] buffer, int offset, int count)
            {
                var encoding = buffer.DetectEncoding();

                var data = new byte[count];
                Buffer.BlockCopy(buffer, offset, data, 0, count);

                var html = encoding.GetString(buffer);
                html = HtmlCompactor.Compact(html);

                var outdata = encoding.GetBytes(html);
                m_Sink.Write(outdata, 0, outdata.GetLength(0));
            }
        }
        #endregion
    }
}