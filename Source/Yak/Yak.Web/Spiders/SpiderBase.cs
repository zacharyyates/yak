using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yak.IO;

namespace Yak.Web.Spiders
{
    public abstract class SpiderBase
    {
        Queue<string> m_Work = new Queue<string>();
        bool m_CompletedAdding = false;

        protected virtual bool TryAddWork(string url)
        {
            if (!m_CompletedAdding)
            {
                m_Work.Enqueue(url);
                return true;
            }
            return false;
        }
        protected virtual bool TryTakeWork(out string url)
        {
            if (m_Work.Any())
            {
                url = m_Work.Dequeue();
                return true;
            }
            url = null;
            return false;
        }

        public bool UseCache { get; set; }
        public string CacheDirectory { get; set; }      

        protected Task DownloadHtml(string url, Encoding encoding, HtmlLoaded callback)
        {
            return Task.Factory.StartNew(() =>
            {
                if (UseCache)
                {
                    using (var stream = Cache(url, encoding))
                    {
                        return NQuery.NQuery.Load(stream);
                    }
                }
                else
                {
                    using (var client = CreateWebClient(encoding))
                    using (var stream = client.OpenRead(url))
                    {
                        return NQuery.NQuery.Load(stream);
                    }
                }
            })
            .ContinueWith((antecedent) =>
            {
                NQuery.NQuery result = null;
                Exception ex = null;
                try
                {
                    result = antecedent.Result;
                }
                catch (AggregateException ae)
                {
                    ex = ae.InnerException;
                }
                finally
                {
                    callback(ex, result);
                }
            });
        }
        protected Task DownloadHtml(string url, HtmlLoaded callback)
        {
            return DownloadHtml(url, Encoding.UTF8, callback);
        }
        protected NQuery.NQuery DownloadHtml(string url, Encoding encoding)
        {
            NQuery.NQuery nq = null;
            Exception dlEx = null;
            DownloadHtml(url, encoding, (ex, html) =>
            {
                dlEx = ex;
                nq = html;
            }).Wait();

            if (!dlEx.IsNull())
                return nq;
            else
                throw dlEx;
        }

        bool IsCached(string path)
        {
            return File.Exists(path) && File.GetLastWriteTime(path).FromNow() > TimeSpan.FromDays(1);
        }

        Stream Cache(string url, Encoding encoding)
        {
            try
            {
                if (CacheDirectory.IsNullOrWhiteSpace() || !Directory.Exists(CacheDirectory))
                    CacheDirectory = @".\Cache\";

                DirectoryExtensions.EnsureDirectory(CacheDirectory);

                var fileName = new StringBuilder(url + ".gz")
                    .Replace("//", "/")
                    .Replace("/", "_")
                    .Replace(":", "")
                    .ToString();

                var path = Path.Combine(CacheDirectory, fileName);

                // if there's no cache or the cache is old, write a new file
                if (!IsCached(path))
                {
                    var ms = new MemoryStream();
                    using (var client = CreateWebClient(encoding))
                    using (var stream = client.OpenRead(url))
                    {
                        stream.CopyTo(ms);
                        using (var compressor = new GZipStream(File.OpenWrite(path), CompressionMode.Compress))
                            ms.CopyTo(compressor);
                    }
                    ms.Seek(0, SeekOrigin.Begin);
                    return ms;
                }
                else
                {
                    return new GZipStream(File.OpenRead(path), CompressionMode.Decompress);
                }
            }
            catch (Exception ex)
            {
                OnItemFailed(new ApplicationException("Downloading {0} failed.".FormatWith(url), ex));
            }
            return null;
        }

        protected WebClient CreateWebClient(Encoding encoding)
        {
            var client = new WebClient();
            // disable automatic proxy detection
            // http://stackoverflow.com/questions/754333/why-is-this-webrequest-code-slow
            client.Proxy = null;
            client.Encoding = encoding;
            return client;
        }

        public virtual void Run()
        {
            Produce();
            foreach (var url in m_Work)
            {
                Consume(url);
            }
        }

        public event EventHandler<DynamicEventArgs> ItemComplete
        {
            add { m_ItemComplete += value; }
            remove { m_ItemComplete -= value; }
        }
        EventHandler<DynamicEventArgs> m_ItemComplete;

        protected void OnItemComplete(dynamic e)
        {
            if (m_ItemComplete != null)
                m_ItemComplete(this, e);
        }

        public event EventHandler<ExceptionEventArgs> ItemFailed
        {
            add { m_ItemFailed += value; }
            remove { m_ItemFailed -= value; }
        }
        EventHandler<ExceptionEventArgs> m_ItemFailed;

        protected void OnItemFailed(ExceptionEventArgs e)
        {
            if (m_ItemFailed != null)
                m_ItemFailed(this, e);
        }

        public delegate void HtmlLoaded(Exception ex, NQuery.NQuery html);

        /// <summary>
        /// When implemented in a derived class, produces the addresses for the content to be parsed.
        /// </summary>
        protected abstract void Produce();

        /// <summary>
        /// When implemented in a derived class, retrieves and parses the content at address.
        /// </summary>
        protected abstract void Consume(string address);
    }
}