using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Yak.Web.Spiders
{
    public abstract class AsyncSpiderBase : SpiderBase
    {
        public int ShortSleep { get; set; }
        public int MaxConsumers { get; set; }

        public ParallelOptions ParallelOptions
        {
            get { return new ParallelOptions { MaxDegreeOfParallelism = MaxConsumers }; }
        }

        protected BlockingCollection<string> Work { get; set; }

        public AsyncSpiderBase()
        {
            MaxConsumers = 25;
            ShortSleep = 500;
        }

        // todo: add cancellation token impl, task awaitable
        public virtual Task RunAsync()
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

        void ProduceTmpl()
        {
            Produce();

            if (!Work.IsNull())
                Work.CompleteAdding();
        }

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
    }
}