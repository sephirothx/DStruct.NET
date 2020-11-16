using DStruct.BinaryTrees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        [TestMethod]
        public void InsertTest()
        {
            var list = new[] {6, 4, 2, 5, 1, 3, 7, 9, 8, 10};
            var bst  = new BinarySearchTree<int>();

            foreach (int i in list)
            {
                bst.Insert(i);
            }

            Assert.AreEqual(10, bst.Count);
        }

        [TestMethod]
        public void FindTest()
        {
            var list = new[] {6, 4, 2, 5, 1, 3, 7, 9, 8, 10};
            var bst  = new BinarySearchTree<int>(list);

            Assert.IsTrue(bst.Find(3));
            Assert.IsFalse(bst.Find(0));
        }

        [TestMethod]
        public void RemoveTest()
        {
            var list = new[] {20, 10, 5, 15, 30, 25, 35, 32, 33};
            var bst  = new BinarySearchTree<int>(list);

            Assert.IsFalse(bst.Remove(7));
            Assert.IsTrue(bst.Remove(10));
            Assert.IsTrue(bst.Remove(30));
            Assert.AreEqual(33, bst[5]);
        }
    }
}
