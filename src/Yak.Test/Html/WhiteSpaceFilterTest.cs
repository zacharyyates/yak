using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yak.Html;

namespace Yak.Test.Html {
    [TestClass]
    public class WhiteSpaceFilterTest {
        const string Html1 =
            @"<html lang='en'>
                <head>      </head>
                <body onload='go();'>
                    <div>       hi, hello how are you</div>
                </body>
            </html>";

        const string Html1Compacted =
            @"<html lang='en'><head> </head><body onload='go();'><div> hi, hello how are you</div></body></html>";

        [TestMethod]
        public void Filter() {
            var actual = WhiteSpaceFilter.Filter(Html1);
            Assert.AreEqual(Html1Compacted, actual);
        }

        [TestMethod]
        public void FilterPerf() {
            var iterations = TestSettings.PerformanceIterations;
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < iterations; i++) {
                Filter();
            }
            watch.Stop();

            Console.WriteLine("HtmlCompactor.Compact() for {0} iterations: {1}", iterations, watch.Elapsed);
        }
    }
}