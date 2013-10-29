using System;
using System.IO;
using System.Web;
using Yak.Html;
using Yak.Text;

namespace Yak.Web {
    /// <summary>
    /// Removes whitespace from an html response.
    /// </summary>
    // adapted from: http://madskristensen.net/post/A-whitespace-removal-HTTP-module-for-ASPNET-20.aspx
    public class WhiteSpaceFilterModule : IHttpModule {
        #region IHttpModule Members

        // this is left empty because apparently unsubscribing from application events is pointless:
        // http://stackoverflow.com/questions/3424438/how-to-dispose-ihttpmodule-correctly
        void IHttpModule.Dispose() { }

        void IHttpModule.Init(HttpApplication application) {
            application.BeginRequest += BeginRequest;
        }

        #endregion

        static void BeginRequest(object sender, EventArgs e) {
            var app = sender as HttpApplication;
            if (app != null && app.Request.RawUrl.Contains(".html")) {
                app.Response.Filter = new WhiteSpaceFilterStream(app.Response.Filter);
            }
        }

        #region Stream filter

        class WhiteSpaceFilterStream : Stream {
            readonly Stream _sink;

            public WhiteSpaceFilterStream(Stream sink) {
                _sink = sink;
            }

            #region Delegated members

            public override bool CanRead {
                get { return true; }
            }

            public override bool CanSeek {
                get { return true; }
            }

            public override bool CanWrite {
                get { return true; }
            }

            public override void Flush() {
                _sink.Flush();
            }

            public override long Length {
                get { return 0; }
            }

            public override long Position { get; set; }

            public override int Read(byte[] buffer, int offset, int count) {
                return _sink.Read(buffer, offset, count);
            }

            public override long Seek(long offset, SeekOrigin origin) {
                return _sink.Seek(offset, origin);
            }

            public override void SetLength(long value) {
                _sink.SetLength(value);
            }

            public override void Close() {
                _sink.Close();
            }

            #endregion

            public override void Write(byte[] buffer, int offset, int count) {
                var encoding = buffer.DetectEncoding();

                var data = new byte[count];
                Buffer.BlockCopy(buffer, offset, data, 0, count);

                var html = encoding.GetString(buffer);
                html = WhiteSpaceFilter.Filter(html);

                var outdata = encoding.GetBytes(html);
                _sink.Write(outdata, 0, outdata.GetLength(0));
            }
        }
        #endregion
    }
}