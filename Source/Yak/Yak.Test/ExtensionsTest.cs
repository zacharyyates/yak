using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yak.Test
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void IsNull()
        {
            object nullObj = null;
            Assert.IsTrue(nullObj.IsNull());

            object notNullObj = new object();
            Assert.IsFalse(notNullObj.IsNull());
        }

        [TestMethod]
        public void IsNullOrEmpty()
        {
            string nullStr = null;
            Assert.IsTrue(nullStr.IsNullOrEmpty());

            string emptyStr = string.Empty;
            Assert.IsTrue(emptyStr.IsNullOrEmpty());

            string notNullOrEmptyStr = "not null or empty";
            Assert.IsFalse(notNullOrEmptyStr.IsNullOrEmpty());
        }

        [TestMethod]
        public void IsNullOrWhiteSpace()
        {
            string nullStr = null;
            Assert.IsTrue(nullStr.IsNullOrWhiteSpace());

            string emptyStr = string.Empty;
            Assert.IsTrue(emptyStr.IsNullOrWhiteSpace());

            string whitespaceStr = "\n\t     ";
            Assert.IsTrue(whitespaceStr.IsNullOrWhiteSpace());

            string notNullOrWhiteSpaceStr = "not null or whitespace";
            Assert.IsFalse(notNullOrWhiteSpaceStr.IsNullOrWhiteSpace());
        }

        [TestMethod]
        public void ThrowIfNull()
        {
            object nullObj = null;
            try
            {
                nullObj.ThrowIfNull();
                Assert.Fail();
            }
            catch (ArgumentNullException) 
            { 
                // catch the exception to make the test pass
            }

            try
            {
                nullObj.ThrowIfNull("nullObj");
                Assert.Fail();
            }
            catch (ArgumentNullException ane)
            {
                Assert.AreEqual(ane.ParamName, "nullObj");
            }
        }

        [TestMethod]
        public void ThrowIfNullOrEmpty()
        {
            string nullStr = null;
            try
            {
                nullStr.ThrowIfNullOrEmpty();
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                // catch the exception to make the test pass
            }

            try
            {
                nullStr.ThrowIfNullOrEmpty("nullStr");
                Assert.Fail();
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual("nullStr", ae.ParamName);
            }
        }

        [TestMethod]
        public void ThrowIfNullOrWhiteSpace()
        {
            string whitespaceStr = "   \t\n";
            try
            {
                whitespaceStr.ThrowIfNullOrWhiteSpace();
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                // catch the exception to make the test pass
            }

            try
            {
                whitespaceStr.ThrowIfNullOrWhiteSpace("whitespaceStr");
                Assert.Fail();
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual("whitespaceStr", ae.ParamName);
            }
        }
    }
}