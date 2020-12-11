﻿using System;
using System.Collections.Generic;

// ReSharper disable RedundantAssignment

namespace DStruct.BinaryTrees
{
    /// <summary>Represents a node-based, non self-balancing <see cref="IBinarySearchTree{T}" /> enhanced to implement an efficient indexer.</summary>
    /// <typeparam name="T">The type of the values stored in the <see cref="BinarySearchTree{T}"/>.</typeparam>
    public class BinarySearchTree<T> : BinarySearchTreeBase<T>
    {
        private BSTNode<T> _root;

        /// <summary>Initializes a new instance of <see cref="BinarySearchTree{T}"/> that is empty.</summary>
        public BinarySearchTree()
        {
        }

        /// <summary>Initializes a new instance of <see cref="BinarySearchTree{T}"/> that contains every item from the input collection.</summary>
        /// <param name="collection">The collection of elements to add to the <see cref="BinarySearchTree{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public BinarySearchTree(IEnumerable<T> collection)
            : base(collection)
        {
        }

        /// <summary>Initializes a new instance of <see cref="BinarySearchTree{T}"/> that is empty and uses the specified <see cref="IComparer{T}"/>.</summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> that will be used for making comparisons.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> is <c>null</c>.</exception>
        public BinarySearchTree(IComparer<T> comparer)
            : base(comparer)
        {
        }

        private protected override IBinarySearchTreeNode<T> Root => _root;

        /// <summary>Inserts an element into the <see cref="BinarySearchTree{T}" /> and returns its index. <code>Complexity: avg O(LogN), worst O(N)</code></summary>
        /// <param name="value">The element to add to the <see cref="BinarySearchTree{T}"/>.</param>
        /// <returns>The index at which the element was placed.</returns>
        public override int Insert(T value)
        {
            int position = 0;
            
            ref var curr = ref _root;

            while (curr != null)
            {
                if (Compare(value, curr.Value) < 0)
                {
                    curr.LeftChildren++;
                    curr = ref curr.Left;
                }
                else
                {
                    position += curr.LeftChildren + 1;
                    curr = ref curr.Right;
                }
            }

            curr = new BSTNode<T>(value);
            Count++;

            return position;
        }

        /// <summary>Determines whether the <see cref="BinarySearchTree{T}" /> contains a specific value. <code>Complexity: avg O(LogN), worst O(N)</code></summary>
        /// <param name="value">The element to locate in the <see cref="BinarySearchTree{T}"/>.</param>
        /// <returns><c>true</c> if the <see cref="BinarySearchTree{T}"/> contains <paramref name="value"/>; <c>false</c> otherwise.</returns>
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

        /// <summary>Removes one occurrence of a specific element from the <see cref="BinarySearchTree{T}"/>. <code>Complexity: avg O(LogN), worst O(N)</code></summary>
        /// <param name="value">The element to remove from the <see cref="BinarySearchTree{T}"/>.</param>
        /// <returns><c>true</c> if the element was successfully removed from the <see cref="BinarySearchTree{T}"/>; <c>false</c> otherwise.</returns>
        public override bool Remove(T value)
        {
            bool ret = true;

            BSTNode<T> RemoveHelper(BSTNode<T> node)
            {
                if (node == null)
                {
                    ret = false;
                    return null;
                }

                int  comparison  = Compare(value, node.Value);
                bool decremented = false;

                if (comparison < 0)
                {
                    node.LeftChildren--;
                    decremented = true;
                    node.Left   = RemoveHelper(node.Left);
                }
                else if (comparison > 0)
                {
                    node.Right = RemoveHelper(node.Right);
                }
                else
                {
                    if (node.Left == null)
                    {
                        return node.Right;
                    }

                    if (node.Right == null)
                    {
                        return node.Left;
                    }

                    node.Value = BSTNode<T>.RemoveInOrderSuccessor(ref node.Right);
                }

                if (!ret && decremented)
                {
                    node.LeftChildren++;
                }

                return node;
            }

            _root = RemoveHelper(_root);

            if (ret) Count--;
            return ret;
        }
    }
}
