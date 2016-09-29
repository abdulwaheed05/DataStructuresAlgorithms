using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UtilityTests
    {
        [TestMethod]
        public void AssertExtensionIEnumberableSameLength()
        {
            int[] arr1 = { 1, 2 };
            int[] arr2 = { 1, 2 };

            Assert.IsTrue(AssertExtensions.AreEqual(arr1, arr2));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssertExtensionIEnumberableDifferentLength()
        {
            int[] arr1 = { 1, 2 };
            int[] arr2 = { 1};

            AssertExtensions.AreEqual(arr1, arr2);
        }

        [TestMethod]
        public void AssertExtensionIEnumberableDifferentTypes()
        {
            List<int> list = new List<int> () { 1, 2 };
            int[] arr = { 1, 2 };

            Assert.IsTrue(AssertExtensions.AreEqual(arr, list));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssertExtensionIEnumberableDifferentItems()
        {
            List<int> list = new List<int>() { 1, 3 };
            int[] arr = { 1, 2 };

            Assert.IsTrue(AssertExtensions.AreEqual(arr, list));
        }

    }
}
