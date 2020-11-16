using System;
using DStruct.Queues;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Queues
{
    [TestClass]
    public class PriorityQueueTests
    {
        [TestMethod]
        public void AscendingTest()
        {
            var pq = new PriorityQueue<int>(PriorityQueueOrder.Ascending);

            var list = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            foreach (int i in list)
            {
                pq.Push(i, i);
            }

            var node = pq.Peek();
            Assert.AreEqual(9, node.Priority);
            Assert.AreEqual(9, node.Value);
            Assert.AreEqual(10, pq.Count);

            Array.Reverse(list);
            foreach (int i in list)
            {
                node = pq.Pop();
                Assert.AreEqual(i, node.Priority);
                Assert.AreEqual(i, node.Value);
                Assert.AreEqual(i, pq.Count);
            }
        }

        [TestMethod]
        public void DescendingTest()
        {
            var pq = new PriorityQueue<int>(PriorityQueueOrder.Descending);

            var list = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            foreach (int i in list)
            {
                pq.Push(i, i);
            }

            var node = pq.Peek();
            Assert.AreEqual(0,  node.Priority);
            Assert.AreEqual(0,  node.Value);
            Assert.AreEqual(10, pq.Count);

            foreach (int i in list)
            {
                node = pq.Pop();
                Assert.AreEqual(i, node.Priority);
                Assert.AreEqual(i, node.Value);
                Assert.AreEqual(9 - i, pq.Count);
            }
        }
    }
}
