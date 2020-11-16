using DStruct.Heaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Heaps
{
    [TestClass]
    public class HeapTests
    {
        [TestMethod]
        public void MaxHeapTest()
        {
            var list = new[] {5, 12, 3, 0, 67, 4, 9, 4};
            var heap = new MaxHeap<int>();

            foreach (int i in list)
            {
                heap.Push(i);
            }

            Assert.AreEqual(67, heap.Pop());
            Assert.AreEqual(12, heap.Pop());
            Assert.AreEqual(9,  heap.Pop());
            Assert.AreEqual(5,  heap.Pop());
            Assert.AreEqual(4,  heap.Pop());
            Assert.AreEqual(4,  heap.Pop());
            Assert.AreEqual(3,  heap.Pop());
            Assert.AreEqual(0,  heap.Pop());
        }

        [TestMethod]
        public void MinHeapTest()
        {
            var list = new[] {5, 12, 3, 0, 67, 4, 9, 4};
            var heap = new MinHeap<int>(list);

            Assert.AreEqual(0,  heap.Pop());
            Assert.AreEqual(3,  heap.Pop());
            Assert.AreEqual(4,  heap.Pop());
            Assert.AreEqual(4,  heap.Pop());
            Assert.AreEqual(5,  heap.Pop());
            Assert.AreEqual(9,  heap.Pop());
            Assert.AreEqual(12, heap.Pop());
            Assert.AreEqual(67, heap.Pop());
        }
    }
}
