using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yak.Test {
    [TestClass]
    public class StringExtensionsTest {
        [TestMethod]
        public void StringIsNullOrEmpty() {
            string nullStr = null;
            Assert.IsTrue(nullStr.IsNullOrEmpty());

            string emptyStr = string.Empty;
            Assert.IsTrue(emptyStr.IsNullOrEmpty());

            string notNullOrEmptyStr = "not null or empty";
            Assert.IsFalse(notNullOrEmptyStr.IsNullOrEmpty());
        }

        [TestMethod]
        public void StringIsNullOrWhiteSpace() {
            string nullStr = null;
            Assert.IsTrue(nullStr.IsNullOrWhiteSpace());

            string emptyStr = string.Empty;
            Assert.IsTrue(emptyStr.IsNullOrWhiteSpace());

            string whitespaceStr = "\n\t     ";
            Assert.IsTrue(whitespaceStr.IsNullOrWhiteSpace());

            string notNullOrWhiteSpaceStr = "not null or whitespace";
            Assert.IsFalse(notNullOrWhiteSpaceStr.IsNullOrWhiteSpace());
        }
    }
}