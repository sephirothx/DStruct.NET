using DStruct.Queues;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Queues
{
    [TestClass]
    public class DequeTests
    {
        [TestMethod]
        public void GetEnumeratorTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            int j = 0;
            foreach (int i in deque)
            {
                Assert.AreEqual(list[j++], i);
            }
        }

        [TestMethod]
        public void PushBackTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            deque.PushBack(9);
            Assert.AreEqual(10, deque.Count);
            Assert.AreEqual(18, deque.Capacity);
            Assert.AreEqual(9, deque[9]);
        }

        [TestMethod]
        public void PushFrontTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            deque.PushFront(9);
            Assert.AreEqual(10, deque.Count);
            Assert.AreEqual(18, deque.Capacity);
            Assert.AreEqual(9,  deque[0]);
        }

        [TestMethod]
        public void PopBackTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            Assert.AreEqual(8, deque.PopBack());
            Assert.AreEqual(8, deque.Count);
            Assert.AreEqual(9, deque.Capacity);
        }

        [TestMethod]
        public void PopFrontTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            Assert.AreEqual(0, deque.PopFront());
            Assert.AreEqual(8, deque.Count);
            Assert.AreEqual(9, deque.Capacity);
            Assert.AreEqual(1, deque[0]);
        }

        [TestMethod]
        public void PeekBackTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            Assert.AreEqual(8, deque.Back);
        }

        [TestMethod]
        public void PeekFrontTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            Assert.AreEqual(0, deque.Front);
        }

        [TestMethod]
        public void ClearTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            deque.Clear();

            Assert.AreEqual(0, deque.Count);
            Assert.AreEqual(9, deque.Capacity);
        }

        [TestMethod]
        public void ContainsTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            Assert.IsTrue(deque.Contains(7));
            Assert.IsFalse(deque.Contains(10));
        }

        [TestMethod]
        public void RemoveTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            Assert.IsTrue(deque.Remove(7));
            Assert.AreEqual(8, deque.Count);
            Assert.AreEqual(8, deque[7]);

            Assert.IsFalse(deque.Remove(7));
            Assert.AreEqual(8, deque.Count);
        }

        [TestMethod]
        public void IndexOfTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            Assert.AreEqual(5, deque.IndexOf(5));
        }

        [TestMethod]
        public void InsertTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            deque.Insert(5, 12);
            
            Assert.AreEqual(10, deque.Count);
            Assert.AreEqual(18, deque.Capacity);
            Assert.AreEqual(12, deque[5]);
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            var list  = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
            var deque = new Deque<int>(list);

            deque.RemoveAt(5);

            Assert.AreEqual(8, deque.Count);
            Assert.AreEqual(9, deque.Capacity);
            Assert.AreEqual(6, deque[5]);
        }
    }
}
