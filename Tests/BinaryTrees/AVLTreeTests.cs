using DStruct.BinaryTrees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.BinaryTrees
{
    [TestClass]
    public class AVLTreeTests
    {
        [TestMethod]
        public void InsertTest()
        {
            var list = new[] {6, 4, 2, 1, 3, 7, 9, 8, 10};
            var avl  = new AVLTree<int>();

            foreach (int i in list)
            {
                avl.Insert(i);
            }

            Assert.AreEqual(4,  avl.Insert(5));
            Assert.AreEqual(10, avl.Count);
            Assert.AreEqual(8,  avl[7]);
        }

        [TestMethod]
        public void FindTest()
        {
            var list = new[] {6, 4, 2, 1, 3, 7, 9, 8, 10};
            var avl  = new AVLTree<int>(list);

            Assert.IsTrue(avl.Find(3));
            Assert.IsFalse(avl.Find(0));
        }

        [TestMethod]
        public void RemoveTest()
        {
            var list = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var avl  = new AVLTree<int>(list);

            Assert.IsTrue(avl.Remove(8));
            Assert.IsFalse(avl.Remove(8));
            Assert.IsFalse(avl.Remove(0));
            Assert.IsFalse(avl.Remove(-10));

            Assert.AreEqual(9, avl.Count);
        }
    }
}
