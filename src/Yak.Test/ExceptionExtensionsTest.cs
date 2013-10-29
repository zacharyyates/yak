using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yak.Test {
    [TestClass]
    public class ExceptionExtensionsTest {
        [TestMethod]
        public void ThrowIfNull() {
            object nullObj = null;
            try {
                nullObj.ThrowIfNull();
                Assert.Fail();
            } catch (ArgumentNullException) {
                // catch the exception to make the test pass
            }

            try {
                nullObj.ThrowIfNull("nullObj");
                Assert.Fail();
            } catch (ArgumentNullException ane) {
                Assert.AreEqual(ane.ParamName, "nullObj");
            }
        }

        [TestMethod]
        public void ThrowIfNullOrEmpty() {
            string nullStr = null;
            try {
                nullStr.ThrowIfNullOrEmpty();
                Assert.Fail();
            } catch (ArgumentException) {
                // catch the exception to make the test pass
            }

            try {
                nullStr.ThrowIfNullOrEmpty("nullStr");
                Assert.Fail();
            } catch (ArgumentException ae) {
                Assert.AreEqual("nullStr", ae.ParamName);
            }
        }

        [TestMethod]
        public void ThrowIfNullOrWhiteSpace() {
            string whitespaceStr = "   \t\n";
            try {
                whitespaceStr.ThrowIfNullOrWhiteSpace();
                Assert.Fail();
            } catch (ArgumentException) {
                // catch the exception to make the test pass
            }

            try {
                whitespaceStr.ThrowIfNullOrWhiteSpace("whitespaceStr");
                Assert.Fail();
            } catch (ArgumentException ae) {
                Assert.AreEqual("whitespaceStr", ae.ParamName);
            }
        }
    }
}