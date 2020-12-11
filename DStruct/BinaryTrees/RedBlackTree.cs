using System;
using System.Collections.Generic;

// ReSharper disable RedundantAssignment

namespace DStruct.BinaryTrees
{
    /// <summary>
    /// Represents a node-based, self-balancing <see cref="IBinarySearchTree{T}"/> enhanced to implement an efficient indexer.
    /// </summary>
    /// <typeparam name="T">The type of the values stored in the <see cref="RedBlackTree{T}"/>.</typeparam>
    public class RedBlackTree<T> : BinarySearchTreeBase<T>
    {
        private RedBlackTreeNode<T> _root;

        private protected override IBinarySearchTreeNode<T> Root => _root;

        /// <summary>
        /// Initializes a new instance of <see cref="RedBlackTree{T}"/> that is empty.
        /// </summary>
        public RedBlackTree()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RedBlackTree{T}"/> that contains every item from the input collection.
        /// </summary>
        /// <param name="collection">The collection of elements to add to the <see cref="RedBlackTree{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public RedBlackTree(IEnumerable<T> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RedBlackTree{T}"/> that is empty and uses the specified <see cref="IComparer{T}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> that will be used for making comparisons.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> is <c>null</c>.</exception>
        public RedBlackTree(IComparer<T> comparer)
            : base(comparer)
        {
        }

        /// <summary>
        /// Inserts an element into the <see cref="RedBlackTree{T}" /> and returns its index.
        /// <code>Complexity: O(LogN)</code>
        /// </summary>
        /// <param name="value">The element to add to the <see cref="RedBlackTree{T}"/>.</param>
        /// <returns>The index at which the element was placed.</returns>
        public override int Insert(T value)
        {
            RedBlackTreeNode<T> parent = null;
            RedBlackTreeNode<T> child  = null;

            bool adjust   = false;
            int  position = 0;

            void InsertHelper(ref RedBlackTreeNode<T> node)
            {
                if (node == null)
                {
                    node  = new RedBlackTreeNode<T>(value, parent);
                    child = node;
                    Count++;
                    return;
                }

                parent = node;

                if (node.HasTwoRedChildren)
                {
                    node.Recolor();
                    node.Left.Recolor();
                    node.Right.Recolor();
                }

                if (Compare(value, node.Value) < 0)
                {
                    node.LeftChildren++;
                    InsertHelper(ref node.Left);
                }
                else
                {
                    position += node.LeftChildren + 1;
                    InsertHelper(ref node.Right);
                }

                if (adjust)
                {
                    if (node.HasTwoRedChildren)
                    {
                        node.RecolorNodeAndChildren();
                    }
                    else
                    {
                        node = node.PerformRotation();
                    }

                    adjust = false;
                }

                if (node.IsRed && child.IsRed)
                {
                    if (node.Parent.HasTwoRedChildren)
                    {
                        node.Parent.RecolorNodeAndChildren();
                    }
                    else
                    {
                        adjust = true;
                    }
                }

                child = node;
            }

            InsertHelper(ref _root);
            _root.ColorBlack();

            return position;
        }

        /// <summary>
        /// Determines whether the <see cref="RedBlackTree{T}" /> contains a specific value.
        /// <code>Complexity: O(LogN)</code>
        /// </summary>
        /// <param name="value">The element to locate in the <see cref="RedBlackTree{T}"/>.</param>
        /// <returns><c>true</c> if the <see cref="RedBlackTree{T}"/> contains <paramref name="value"/>; <c>false</c> otherwise.</returns>
        public override bool Find(T value)
        {
            if (_root == null)
            {
                return false;
            }

            var curr = _root;
            while (curr != null)
            {
                if (Compare(value, curr.Value) == 0)
                {
                    return true;
                }

                curr = Compare(value, curr.Value) < 0
                           ? curr.Left
                           : curr.Right;
            }

            return false;
        }

        /// <summary>
        /// Removes one occurrence of a specific element from the <see cref="RedBlackTree{T}" />.
        /// <code>Complexity: O(LogN)</code>
        /// </summary>
        /// <param name="value">The element to remove from the <see cref="RedBlackTree{T}"/>.</param>
        /// <returns><c>true</c> if the element was successfully removed from the <see cref="RedBlackTree{T}"/>; <c>false</c> otherwise.</returns>
        public override bool Remove(T value)
        {
            bool ret = false;

            void RemoveHelper(RedBlackTreeNode<T> node)
            {
                if (node == null)
                {
                    ret = false;
                    return;
                }

                int  comparison  = Compare(value, node.Value);
                bool decremented = false;

                if (comparison < 0)
                {
                    node.LeftChildren--;
                    decremented = true;
                    RemoveHelper(node.Left);
                }
                else if (comparison > 0)
                {
                    RemoveHelper(node.Right);
                }
                else
                {
                    ret = true;

                    if (node.Left  == null ||
                        node.Right == null)
                    {
                        node.Delete(ref _root);
                        return;
                    }

                    node.Value = RedBlackTreeNode<T>.RemoveInOrderSuccessor(node.Right, ref _root);
                }

                if (!ret && decremented)
                {
                    node.LeftChildren++;
                }
            }

            RemoveHelper(_root);
            _root?.ColorBlack();

            if (ret) Count--;
            return ret;
        }
    }
}
