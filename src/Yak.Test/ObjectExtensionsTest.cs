
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yak.Test {
    [TestClass]
    public class ObjectExtensionsTest {
        [TestMethod]
        public void IsNull() {
            object nullObj = null;
            Assert.IsTrue(nullObj.IsNull());

            object notNullObj = new object();
            Assert.IsFalse(notNullObj.IsNull());
        }
    }
}