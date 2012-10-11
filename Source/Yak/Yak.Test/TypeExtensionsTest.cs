using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yak.Test
{
    [TestClass]
    public class TypeExtensionsTest
    {
        [TestMethod]
        public void IsImplementationOf()
        {
            var genericListType = typeof(List<>);
            var genericListInterfaceType = typeof(IList<>);
            Assert.IsTrue(genericListType.IsImplementationOf(genericListInterfaceType));

            var arrayListType = typeof(ArrayList);
            var listInterfaceType = typeof(IList);
            Assert.IsTrue(arrayListType.IsImplementationOf(listInterfaceType));

            var objectType = typeof(object);
            Assert.IsFalse(objectType.IsImplementationOf(genericListInterfaceType));
        }

        [TestMethod]
        public void IsNonGenericImplementationOf()
        {
            var arrayListType = typeof(ArrayList);
            var listInterfaceType = typeof(IList);
            Assert.IsTrue(arrayListType.IsNonGenericImplementationOf(listInterfaceType));

            var objectType = typeof(object);
            Assert.IsFalse(objectType.IsNonGenericImplementationOf(listInterfaceType));
        }

        [TestMethod]
        public void IsGenericImplementationOf()
        {
            var genericListType = typeof(List<>);
            var genericListInterfaceType = typeof(IList<>);
            Assert.IsTrue(genericListType.IsGenericImplementationOf(genericListInterfaceType));

            var objectType = typeof(object);
            Assert.IsFalse(objectType.IsGenericImplementationOf(genericListInterfaceType));
        }
    }
}