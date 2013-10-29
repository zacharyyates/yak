using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yak.Test {
    [TestClass]
    public class EnumerableExtensionsTest {
        [TestMethod]
        public void ArrayIsNullOrEmpty() {
            object[] nullArray = null;
            Assert.IsTrue(nullArray.IsNullOrEmpty());

            object[] emptyArray = { };
            Assert.IsTrue(emptyArray.IsNullOrEmpty());

            object[] fullArray = { 1, "I'm a string", 2.04f, DateTime.Now };
            Assert.IsFalse(fullArray.IsNullOrEmpty());
        }

        [TestMethod]
        public void ListIsNullOrEmpty() {
            List<object> nullList = null;
            Assert.IsTrue(nullList.IsNullOrEmpty());

            List<object> emptyList = new List<object> { };
            Assert.IsTrue(emptyList.IsNullOrEmpty());

            List<object> fullList = new List<object>() { 1, "I'm a string", 2.04f, DateTime.Now };
            Assert.IsFalse(fullList.IsNullOrEmpty());
        }

        [TestMethod]
        public void EnumerableIsNullOrEmpty() {
            IEnumerable<object> nullEnumerable = null;
            Assert.IsTrue(nullEnumerable.IsNullOrEmpty());

            IEnumerable emptyEnumerable = new ArrayList() { };
            Assert.IsTrue(emptyEnumerable.IsNullOrEmpty());

            IEnumerable fullEnumerable = new ArrayList() { 1, "I'm a string", 2.04f, DateTime.Now };
            Assert.IsFalse(fullEnumerable.IsNullOrEmpty());
        }
    }
}