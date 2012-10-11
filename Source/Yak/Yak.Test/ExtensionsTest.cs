using System;
using System.Collections;
using System.Collections.Generic;
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

        #region Enumerable

        [TestMethod]
        public void ArrayIsNullOrEmpty()
        {
            object[] nullArray = null;
            Assert.IsTrue(nullArray.IsNullOrEmpty());

            object[] emptyArray = { };
            Assert.IsTrue(emptyArray.IsNullOrEmpty());

            object[] fullArray = { 1, "I'm a string", 2.04f, DateTime.Now };
            Assert.IsFalse(fullArray.IsNullOrEmpty());
        }

        [TestMethod]
        public void ListIsNullOrEmpty()
        {
            List<object> nullList = null;
            Assert.IsTrue(nullList.IsNullOrEmpty());

            List<object> emptyList = new List<object>{ };
            Assert.IsTrue(emptyList.IsNullOrEmpty());

            List<object> fullList = new List<object>() { 1, "I'm a string", 2.04f, DateTime.Now };
            Assert.IsFalse(fullList.IsNullOrEmpty());
        }

        [TestMethod]
        public void EnumerableIsNullOrEmpty()
        {
            IEnumerable<object> nullEnumerable = null;
            Assert.IsTrue(nullEnumerable.IsNullOrEmpty());

            IEnumerable emptyEnumerable = new ArrayList(){ };
            Assert.IsTrue(emptyEnumerable.IsNullOrEmpty());

            IEnumerable fullEnumerable = new ArrayList() { 1, "I'm a string", 2.04f, DateTime.Now };
            Assert.IsFalse(fullEnumerable.IsNullOrEmpty());
        }

        #endregion

        #region String

        [TestMethod]
        public void StringIsNullOrEmpty()
        {
            string nullStr = null;
            Assert.IsTrue(nullStr.IsNullOrEmpty());

            string emptyStr = string.Empty;
            Assert.IsTrue(emptyStr.IsNullOrEmpty());

            string notNullOrEmptyStr = "not null or empty";
            Assert.IsFalse(notNullOrEmptyStr.IsNullOrEmpty());
        }

        [TestMethod]
        public void StringIsNullOrWhiteSpace()
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

        #endregion

        #region Throw

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

        #endregion
    }
}