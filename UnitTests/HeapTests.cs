using System;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class HeapTests
    {
        [TestMethod]
        public void MaxHeapHappyPath()
        {
            MaxHeap<char, int> maxHeap = new MaxHeap<char, int>();
            maxHeap.Insert('b', 5);
            maxHeap.Insert('a', 3);
            maxHeap.Insert('d', 4);
            maxHeap.Insert('c', 1);
            maxHeap.Insert('e', 2);

            for (int i = 5; i >= 1; i--)
            {
                Assert.AreEqual(i, maxHeap.Extract());
            }
        }

        [TestMethod]
        public void MaxHeapWithOneItem()
        {
            MaxHeap<char, int> maxHeap = new MaxHeap<char, int>();
            maxHeap.Insert('a', 5);
            Assert.AreEqual(5, maxHeap.Extract());
            Assert.AreEqual(0, maxHeap.Extract());
        }

        [TestMethod]
        public void MaxHeapWithDuplicates()
        {
            MaxHeap<char, int> maxHeap = new MaxHeap<char, int>();
            maxHeap.Insert('a', 5);
            maxHeap.Insert('c', 3);
            maxHeap.Insert('b', 5);
            Assert.AreEqual(5, maxHeap.Extract());
            Assert.AreEqual(5, maxHeap.Extract());
            Assert.AreEqual(3, maxHeap.Extract());
        }

        [TestMethod]
        public void MaxHeapWithNegativeValues()
        {
            MaxHeap<char, int> maxHeap = new MaxHeap<char, int>();
            maxHeap.Insert('c', -5);
            maxHeap.Insert('b', 3);
            maxHeap.Insert('a', 5);
            maxHeap.Insert('d', -1);
            Assert.AreEqual(5, maxHeap.Extract());
            Assert.AreEqual(3, maxHeap.Extract());
            Assert.AreEqual(-1, maxHeap.Extract());
            Assert.AreEqual(-5, maxHeap.Extract());
        }

        [TestMethod]
        public void MaxHeapUpdateKey()
        {
            MinHeapWithNoDuplicates<char, int> minHeapNoDuplicates = new MinHeapWithNoDuplicates<char, int>();
            minHeapNoDuplicates.Insert('c', -5);
            minHeapNoDuplicates.Insert('b', 3);
            minHeapNoDuplicates.Insert('a', 5);
            minHeapNoDuplicates.Insert('d', -1);
            
            minHeapNoDuplicates.UpdatePriority('c', 6);
            Assert.AreEqual(6, minHeapNoDuplicates.Extract());


            minHeapNoDuplicates.UpdatePriority('b', 7);
            Assert.AreEqual(7, minHeapNoDuplicates.Extract());

            Assert.AreEqual(5, minHeapNoDuplicates.Extract());
            Assert.AreEqual(-1, minHeapNoDuplicates.Extract());
        }


        #region MinHeap Tests

        [TestMethod]
        public void MinHeapHappyPath()
        {
            MinHeap<char, int> minHeap = new MinHeap<char, int>();
            minHeap.Insert('a', 5);
            minHeap.Insert('b', 3);
            minHeap.Insert('d', 4);
            minHeap.Insert('e', 1);
            minHeap.Insert('c', 2);

            for (int i = 1; i <= 5; i++)
            {
                Assert.AreEqual(i, minHeap.Extract());
            }
        }

        [TestMethod]
        public void MinHeapWithOneItem()
        {
            MinHeap<char, int> minHeap = new MinHeap<char, int>();
            minHeap.Insert('a', 5);
            Assert.AreEqual(5, minHeap.Extract());
            Assert.AreEqual(0, minHeap.Extract());
        }

        [TestMethod]
        public void MinHeapWithNegativeValues()
        {
            MinHeap<char, int> minHeap = new MinHeap<char, int>();
            minHeap.Insert('c', -5);
            minHeap.Insert('d', 3);
            minHeap.Insert('a', 5);
            minHeap.Insert('b', -1);
            Assert.AreEqual(-5, minHeap.Extract());
            Assert.AreEqual(-1, minHeap.Extract());
            Assert.AreEqual(3, minHeap.Extract());
            Assert.AreEqual(5, minHeap.Extract());
        }

        [TestMethod]
        public void MinHeapUpdateKey2()
        {
            MinHeapWithNoDuplicates<char, int> minHeap = new MinHeapWithNoDuplicates<char, int>();
            minHeap.Insert('a', 11);
            minHeap.Insert('b', 19);
            minHeap.Insert('c', 13);
            minHeap.Insert('d', 20);
            minHeap.Insert('e', 21);
            minHeap.Insert('f', 55);
            minHeap.Insert('g', 15);

            minHeap.UpdatePriority('c', 63);
            Assert.AreEqual('a', minHeap.Extract());
            Assert.AreEqual('g', minHeap.Extract());
            Assert.AreEqual('b', minHeap.Extract());
            Assert.AreEqual('d', minHeap.Extract());
            Assert.AreEqual('e', minHeap.Extract());
            Assert.AreEqual('f', minHeap.Extract());
            Assert.AreEqual('c', minHeap.Extract());
        }

        #endregion
    }
}
