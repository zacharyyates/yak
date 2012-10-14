using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yak.Web.NQuery;

namespace Yak.Web.Spiders.Test
{
    [TestClass]
    public class SpiderTest
    {
        [TestMethod]
        public void DownloadHtml()
        {
            var spider = new SpiderMock();
            var task = spider.TestDownloadHtml("http://www.google.com/", Encoding.UTF8, (ex, html) =>
            {
                var result = html["img#hplogo"].Html();
                Trace.WriteLine(result);
            });
            task.Wait();
        }
    }

    class SpiderMock : SpiderBase
    {
        public Task TestDownloadHtml(string address, Encoding encoding, HtmlLoaded callback)
        {
            return this.DownloadHtml(address, encoding, callback);
        }

        protected override void Produce()
        {
            throw new NotImplementedException();
        }

        protected override void Consume(string address)
        {
            throw new NotImplementedException();
        }
    }
}