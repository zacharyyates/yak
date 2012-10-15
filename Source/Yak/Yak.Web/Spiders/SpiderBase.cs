using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Html;
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
        public bool UseCache { get; set; }
        public string CacheDirectory { get; set; }

        public int ShortSleep { get; set; }
        public int MaxConsumers { get; set; }

        public ParallelOptions ParallelOptions
        {
            get { return new ParallelOptions { MaxDegreeOfParallelism = MaxConsumers }; }
        }

        protected BlockingCollection<string> Work { get; set; }

        public SpiderBase()
        {
            MaxConsumers = 25;
            ShortSleep = 500;
        }

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
        protected Task DownloadHtml(string address, HtmlLoaded callback)
        {
            return DownloadHtml(address, Encoding.UTF8, callback);
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

        // todo: add cancellation token impl, task awaitable
        public virtual Task Run()
        {
            var run = Task.Factory.StartNew(() =>
            {
                using (Work = new BlockingCollection<string>())
                {
                    var tasks = new List<Task>();

                    tasks.Add(Task.Factory.StartNew(() => ProduceTmpl()));

                    for (int i = 0; i < MaxConsumers; i++)
                    {
                        tasks.Add(Task.Factory.StartNew(() => ConsumeTmpl()));
                    }

                    Task.WaitAll(tasks.ToArray());

                    return tasks;
                }
            }).ContinueWith((antecedent) =>
            {
                // clean up
                foreach (var task in antecedent.Result)
                {
                    task.Dispose();
                }  
            });
            return run;
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

        void ProduceTmpl()
        {
            Produce();

            if (!Work.IsNull())
                Work.CompleteAdding();
        }

        /// <summary>
        /// When implemented in a derived class, produces the addresses for the content to be parsed.
        /// </summary>
        protected abstract void Produce();

        void ConsumeTmpl()
        {
            string address = null;
            while (!Work.IsCompleted)
            {
                if (Work.TryTake(out address) && !address.IsNullOrWhiteSpace())
                    Consume(address);
                else
                    Thread.Sleep(ShortSleep);
            }
        }

        /// <summary>
        /// When implemented in a derived class, retrieves and parses the content at address.
        /// </summary>
        protected abstract void Consume(string address);
    }
}