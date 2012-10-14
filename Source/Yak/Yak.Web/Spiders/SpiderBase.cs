using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Yak.Web.NQuery;

namespace Yak.Web.Spiders
{
    public abstract class SpiderBase
    {
        public int ShortSleep { get; set; }
        public int MaxConsumers { get; set; }
        protected BlockingCollection<string> Items { get; set; }

        public SpiderBase()
        {
            MaxConsumers = 25;
            ShortSleep = 0;
        }

        protected Task DownloadHtml(string address, Encoding encoding, HtmlLoaded callback)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var client = CreateWebClient(encoding))
                using (var stream = client.OpenRead(address))
                {
                    return NQuery.NQuery.Load(stream);
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
            using (Items = new BlockingCollection<string>())
            {
                var tasks = new List<Task>();
                
                var produce = Task.Factory.StartNew(() => Produce());
                tasks.Add(produce);

                for (int i = 0; i < MaxConsumers; i++)
                {
                    var consume = Task.Factory.StartNew(() =>
                    {
                        var keepGoing = true;
                        string address = null;
                        while (keepGoing)
                        {
                            keepGoing = Items.TryTake(out address);
                            Consume(address);
                        }
                    });
                    tasks.Add(consume);
                }

                Task.WaitAll(tasks.ToArray());

                // clean up
                foreach (var task in tasks)
                {
                    task.Dispose();
                }
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

        public class DynamicEventArgs : EventArgs
        {
            public dynamic Data { get; set; }
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