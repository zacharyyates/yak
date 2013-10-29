using System;
using System.IO.Compression;
using System.Web;

namespace Yak.Web {
    public static class WebExtensions {
        #region Http Compression
        // adapted from: http://www.codeproject.com/Articles/38067/Compress-Response-and-HTML-WhiteSpace-Remover

        static HttpWorkerRequest GetWorkerRequest(HttpContext context) {
            var provider = (IServiceProvider)context;
            var worker = (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));
            return worker;
        }

        public static void SetEncondingOptimized(HttpContext context, string contentEncoding) {
            var worker = GetWorkerRequest(context);
            worker.SendKnownResponseHeader(HttpWorkerRequest.HeaderContentEncoding, contentEncoding);
        }

        public static void SetVaryOptimized(HttpContext context, string contentVary) {
            var worker = GetWorkerRequest(context);
            worker.SendKnownResponseHeader(HttpWorkerRequest.HeaderVary, contentVary);
        }

        public static bool IsGZipSupported() {

            var acceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

            return !acceptEncoding.IsNullOrEmpty() &&
                acceptEncoding.Contains("gzip") ||
                acceptEncoding.Contains("deflate");
        }

        public static void GZipEncodePage() {
            var response = HttpContext.Current.Response;

            if (IsGZipSupported()) {
                var acceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
                if (acceptEncoding.Contains("deflate")) {
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                    SetEncondingOptimized(HttpContext.Current, "deflate");
                } else {
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                    SetEncondingOptimized(HttpContext.Current, "gzip");
                }
            }

            // Allow proxy servers to cache encoded and unencoded versions separately
            SetVaryOptimized(HttpContext.Current, "Content-Encoding");
        }

        #endregion
    }
}