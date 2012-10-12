using System;
using System.Html;
using System.IO;
using System.Text;
using System.Web;

// adapted from: http://madskristensen.net/post/A-whitespace-removal-HTTP-module-for-ASPNET-20.aspx

/// <summary>
/// Removes whitespace from the html response.
/// </summary>
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
        public HtmlCompactorStream(Stream sink)
        {
            _sink = sink;
        }

        Stream _sink;
        
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
            _sink.Flush();
        }

        public override long Length
        {
            get { return 0; }
        }

        long _position;
        public override long Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _sink.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _sink.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _sink.SetLength(value);
        }

        public override void Close()
        {
            _sink.Close();
        }

        #endregion

        public override void Write(byte[] buffer, int offset, int count)
        {
            var data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            var html = Encoding.UTF8.GetString(buffer);

            html = HtmlCompactor.Compact(html);

            var outdata = Encoding.UTF8.GetBytes(html);
            _sink.Write(outdata, 0, outdata.GetLength(0));
        }
    }
    #endregion
}