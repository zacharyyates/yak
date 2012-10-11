using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yak.Test
{
    [TestClass]
    public class NumericExtensionsTest
    {
        [TestMethod]
        public void OrderOfMagnitudeTest()
        {
            double aMillion = 1000000;
            Assert.AreEqual(6, aMillion.OrderOfMagnitude());

            long shouldBe7 = 10000000;
            Assert.AreEqual(7, shouldBe7.OrderOfMagnitude());
            
            int aBillion = 1000000000;
            Assert.AreEqual(9, aBillion.OrderOfMagnitude());

            short shouldBe2 = 100;
            Assert.AreEqual(2, shouldBe2.OrderOfMagnitude());

            float shouldBe14 = 100000000000000;
            Assert.AreEqual(14, shouldBe14.OrderOfMagnitude());

            decimal shouldBe10 = 17452888857;
            Assert.AreEqual(10, shouldBe10.OrderOfMagnitude());
        }

        [TestMethod]
        public void LongScaleTest()
        {
            long tenBillion = 10000000000;
            Assert.AreEqual(LongScale.Milliard, tenBillion.LongScale());

            // todo: add more test coverage here
        }

        [TestMethod]
        public void ShortScaleTest()
        {
            long tenBillion = 10000000000;
            Assert.AreEqual(ShortScale.Billion, tenBillion.ShortScale());

            // todo: add more test coverage here
        }

        [TestMethod]
        public void SymbolTest()
        {
            long tenBillion = 10000000000;
            Assert.AreEqual("G", tenBillion.Symbol());

            // todo: add more test coverage here
        }

        [TestMethod]
        public void ToStringTest()
        {
            long tenBillion = 10000000000;
            Assert.AreEqual("10G", tenBillion.ToString(2));

            long tenPointTwoOneFiveBillion = 10215000000;
            Assert.AreEqual("10.2G", tenPointTwoOneFiveBillion.ToString(4));

            // todo: add more test coverage here
            int oneHundredTwentyFiveThousand = 125000;
            Assert.AreEqual("125k", oneHundredTwentyFiveThousand.ToString(3));
        }
    }
}