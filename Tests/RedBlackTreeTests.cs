using DStruct.BinaryTrees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class RedBlackTreeTests
    {
        [TestMethod]
        public void InsertTest()
        {
            var list = new[] {1, 2, 3, 4, 6, 7, 8, 9, 10};
            var rbt  = new RedBlackTree<int>();

            foreach (int i in list)
            {
                rbt.Insert(i);
            }

            Assert.AreEqual(4,  rbt.Insert(5));
            Assert.AreEqual(10, rbt.Count);
            Assert.AreEqual(8,  rbt[7]);
        }

        [TestMethod]
        public void FindTest()
        {
            var list = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var rbt  = new RedBlackTree<int>(list);

            Assert.IsTrue(rbt.Find(3));
            Assert.IsFalse(rbt.Find(0));
        }

        [TestMethod]
        public void RemoveTest()
        {
            var list = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var rbt  = new RedBlackTree<int>(list);

            Assert.IsTrue(rbt.Remove(8));
            Assert.IsFalse(rbt.Remove(8));
            Assert.IsFalse(rbt.Remove(0));
            Assert.IsFalse(rbt.Remove(-10));
            Assert.IsTrue(rbt.Remove(4));

            Assert.AreEqual(8, rbt.Count);
        }
    }
}
