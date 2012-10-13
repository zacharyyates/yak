using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yak.Web.Test
{
    [TestClass]
    public class NQueryTest
    {
        public TestContext Context { get; set; }

        NQuery.NQuery _nq = null;

        [TestInitialize, TestMethod]
        public void LoadHtml()
        {
            var html = File.ReadAllText(@".\Files\jquery.html");
            _nq = NQuery.NQuery.LoadHtml(html);
        }

        [TestMethod, TestCategory("NQuery")]
        public void Html()
        {
            var output = _nq["*"].Html();
            Assert.IsNotNull(output);
        }

        [TestMethod, TestCategory("NQuery")]
        public void Attr()
        {
            var results = _nq["a"];
            var href = results.Attr("href");
            Console.WriteLine("results: " + href);
            Assert.AreEqual("http://jquery.com", href);
        }

        [TestMethod, TestCategory("NQuery")]
        public void Text()
        {
            var results = _nq["a.jq-runCode"];
            var text = results.Text();
            Console.WriteLine("results: " + text);
            Assert.AreEqual("Run Code", text);
        }
    }
}