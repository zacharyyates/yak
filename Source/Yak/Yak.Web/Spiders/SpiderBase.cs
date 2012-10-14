using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            ShortSleep = 500;
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


        // todo: add cancellation token impl, task awaitable
        public virtual Task Run()
        {
            var run = Task.Factory.StartNew(() =>
            {
                using (Items = new BlockingCollection<string>())
                {
                    var tasks = new List<Task>();

                    var produce = Task.Factory.StartNew(() => ProduceTmpl());
                    tasks.Add(produce);

                    for (int i = 0; i < MaxConsumers; i++)
                    {
                        var consume = Task.Factory.StartNew(() =>
                        {
                            string address = null;
                            while (!Items.IsCompleted)
                            {
                                if (Items.TryTake(out address) && !address.IsNullOrWhiteSpace())
                                    Consume(address);
                                else
                                    Thread.Sleep(ShortSleep);
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

        public class DynamicEventArgs : EventArgs
        {
            public dynamic Data { get; set; }
        }

        public delegate void HtmlLoaded(Exception ex, NQuery.NQuery html);

        void ProduceTmpl()
        {
            Produce();

            if (!Items.IsNull())
                Items.CompleteAdding();
        }

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