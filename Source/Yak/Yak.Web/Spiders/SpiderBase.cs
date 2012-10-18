using System;
using System.Collections.Concurrent;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yak.Web.Spiders
{
    public abstract class SpiderBase<T>
    {
        protected ConcurrentQueue<T> Work { get; private set; }

        protected Encoding DefaultEncoding { get; set; }
        public int MaxParallel { get; set; }
        public int ShortSleep { get; set; }

        object m_Lock = new object();

        protected bool CompletedProducing
        {
            get { lock (m_Lock) { return m_CompletedProducing; } }
            set { lock (m_Lock) { m_CompletedProducing = value; } }
        }
        bool m_CompletedProducing = false;

        public SpiderBase()
        {
            DefaultEncoding = Encoding.UTF8;
            Work = new ConcurrentQueue<T>();
            MaxParallel = 1;
            ShortSleep = 500;
        }

        protected string DownloadHtml(string url, Encoding encoding)
        {
            using (var client = CreateWebClient(encoding))
                return client.DownloadString(url);
        }
        protected string DownloadHtml(string url)
        {
            return DownloadHtml(url, DefaultEncoding);
        }
        protected INQuery Load(string url, Encoding encoding)
        {
            return NQuery.LoadHtml(DownloadHtml(url, encoding));
        }
        protected INQuery Load(string url)
        {
            return Load(url, DefaultEncoding);
        }

        protected WebClient CreateWebClient(Encoding encoding)
        {
            encoding.ThrowIfNull("encoding");

            var client = new WebClient();
            // disable automatic proxy detection
            // http://stackoverflow.com/questions/754333/why-is-this-webrequest-code-slow
            client.Proxy = null;
            client.Encoding = encoding;
            client.Headers["User-Agent"] = @"Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            return client;
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

        public event EventHandler<ExceptionEventArgs> ItemFail
        {
            add { m_ItemFail += value; }
            remove { m_ItemFail -= value; }
        }
        EventHandler<ExceptionEventArgs> m_ItemFail;

        protected void OnItemFail(ExceptionEventArgs e)
        {
            if (m_ItemFail != null)
                m_ItemFail(this, e);
        }

        public virtual void Run()
        {
            var produce = Task.Factory.StartNew(() =>
            {
                Produce();
                CompletedProducing = true;
            });

            while (!CompletedProducing)
            {
                Parallel.ForEach(Work, new ParallelOptions { MaxDegreeOfParallelism = MaxParallel }, (item) =>
                {
                    Consume(item);
                });
                Thread.Sleep(ShortSleep);
            }
            produce.Wait();
        }

        /// <summary>
        /// When implemented in a derived class, produces the addresses for the content to be parsed.
        /// </summary>
        protected abstract void Produce();

        /// <summary>
        /// When implemented in a derived class, retrieves and parses the content at address.
        /// </summary>
        protected abstract void Consume(T item);
    }
}