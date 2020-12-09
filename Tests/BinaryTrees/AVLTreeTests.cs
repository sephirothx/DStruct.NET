using DStruct.BinaryTrees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

        [TestMethod]
        public void EnumerateInOrderTraverse()
        {
            var list = new[] { 6, 4, 2, 1, 3, 7, 9, 8, 10 };
            var avl = new AVLTree<int>();

            foreach (int i in list)
            {
                avl.Insert(i);
            }

            var orderedSequence = Enumerable.Range(1, 4).Concat(Enumerable.Range(6, 5)).ToList();

            var actualOrdered = avl.InOrderTraverse().ToList();

            CollectionAssert.AreEqual(orderedSequence, actualOrdered);
        }

        [TestMethod]
        public void EnumeratePostOrderTraverse()
        {
            var list = new[] { 6, 4, 2, 1, 3, 7, 9, 8, 10 };
            var avl = new AVLTree<int>();

            foreach (int i in list)
            {
                avl.Insert(i);
            }

            var orderedSequence = Enumerable.Range(1, 4).Concat(Enumerable.Range(6, 5)).Reverse().ToList();

            var actualOrdered = avl.PostOrderTraverse().ToList();

            CollectionAssert.AreEqual(orderedSequence, actualOrdered);
        }

        [TestMethod]
        public void EnumeratePreOrderTraverse()
        {
            var list = new[] { 2, 3, 5, 6, 9, 10 };
            var avl = new AVLTree<int>();

            foreach (int i in list)
            {
                avl.Insert(i);
            }

            var orderedSequence = new[] { 6, 3, 2, 9, 5, 10 }.ToList();

            var actualOrdered = avl.PreOrderTraverse().ToList();

            CollectionAssert.AreEqual(orderedSequence, actualOrdered);
        }

        [TestMethod]
        public void EnumerateBreadthFirstSearch()
        {
            var list = new[] { 2, 3, 5, 6, 9, 10 };
            var avl = new AVLTree<int>();

            foreach (int i in list)
            {
                avl.Insert(i);
            }

            var orderedSequence = new[] { 6, 3, 9, 2, 5, 10 }.ToList();

            var actualOrdered = avl.BreadthFirstSearch().ToList();

            CollectionAssert.AreEqual(orderedSequence, actualOrdered);
        }
    }
}
