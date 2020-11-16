﻿using System;
using System.Collections.Generic;

// ReSharper disable RedundantAssignment

namespace DStruct.BinaryTrees
{
    /// <summary>Represents a node-based, non self-balancing <see cref="IBinarySearchTree{T}" /> enhanced to implement an efficient indexer.</summary>
    /// <typeparam name="T">The type of the values stored in the <see cref="BinarySearchTree{T}"/>.</typeparam>
    public class BinarySearchTree<T> : IBinarySearchTree<T>
    {
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        private BSTNode<T> _root;

        /// <summary>Gets the number of elements stored in the <see cref="BinarySearchTree{T}" />. <code>Complexity: O(1)</code></summary>
        public int Count { get; private set; }

        /// <summary>Gets the minimum value element stored in the <see cref="BinarySearchTree{T}"/>. <code>Complexity: avg O(LogN), worst O(N)</code></summary>
        /// <exception cref="InvalidOperationException"><see cref="BinarySearchTree{T}"/> is empty.</exception>
        public T Min
        {
            get
            {
                if (_root == null)
                {
                    throw new InvalidOperationException("The Binary Search Tree is empty.");
                }

                var curr = _root;
                while (curr.Left != null)
                {
                    curr = curr.Left;
                }

                return curr.Value;
            }
        }

        /// <summary>Gets the maximum value element stored in the <see cref="BinarySearchTree{T}" />. <code>Complexity: avg O(LogN), worst O(N)</code></summary>
        /// <exception cref="InvalidOperationException"><see cref="BinarySearchTree{T}"/> is empty.</exception>
        public T Max
        {
            get
            {
                if (_root == null)
                {
                    throw new InvalidOperationException("The Binary Search Tree is empty.");
                }

                var curr = _root;
                while (curr.Right != null)
                {
                    curr = curr.Right;
                }

                return curr.Value;
            }
        }

        /// <summary>Gets the element at the specified index. <code>Complexity: avg O(LogN), worst O(N)</code></summary>
        /// <param name="index">The index of the element to get from the <see cref="BinarySearchTree{T}"/>.</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException"><paramref name="index"/> is out of the bounds of the <see cref="BinarySearchTree{T}"/>.</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Index is outside the bounds of the Binary Search Tree.");
                }

                var curr  = _root;
                int check = 0;

                while (true)
                {
                    if (check + curr.LeftChildren == index)
                    {
                        return curr.Value;
                    }

                    if (check + curr.LeftChildren > index)
                    {
                        curr = curr.Left;
                    }
                    else
                    {
                        check += curr.LeftChildren + 1;
                        curr  =  curr.Right;
                    }
                }
            }
        }

        /// <summary>Initializes a new instance of <see cref="BinarySearchTree{T}"/> that is empty.</summary>
        public BinarySearchTree()
        {
        }

        /// <summary>Initializes a new instance of <see cref="BinarySearchTree{T}"/> that contains every item from the input collection.</summary>
        /// <param name="collection">The collection of elements to add to the <see cref="BinarySearchTree{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public BinarySearchTree(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var value in collection)
            {
                Insert(value);
            }
        }

        /// <summary>Initializes a new instance of <see cref="BinarySearchTree{T}"/> that is empty and uses the specified <see cref="IComparer{T}"/>.</summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> that will be used for making comparisons.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> is <c>null</c>.</exception>
        public BinarySearchTree(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        /// <summary>Inserts an element into the <see cref="BinarySearchTree{T}" /> and returns its index. <code>Complexity: avg O(LogN), worst O(N)</code></summary>
        /// <param name="value">The element to add to the <see cref="BinarySearchTree{T}"/>.</param>
        /// <returns>The index at which the element was placed.</returns>
        public int Insert(T value)
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
        public bool Find(T value)
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
        public bool Remove(T value)
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

        /// <summary>Returns the list of the elements stored in the <see cref="BinarySearchTree{T}" /> in-order. <code>Complexity: O(N)</code></summary>
        /// <returns>List of in-order elements.</returns>
        public T[] InOrderTraverse()
        {
            var output = new T[Count];
            int i      = 0;

            void IOTHelper(BSTNode<T> node)
            {
                if (node == null)
                {
                    return;
                }

                IOTHelper(node.Left);
                output[i++] = node.Value;
                IOTHelper(node.Right);
            }

            IOTHelper(_root);

            return output;
        }

        /// <summary>Returns the list of the elements stored in the <see cref="BinarySearchTree{T}" /> pre-order. <code>Complexity: O(N)</code></summary>
        /// <returns>List of pre-order elements.</returns>
        public T[] PreOrderTraverse()
        {
            var output = new T[Count];
            int i      = 0;

            void POTHelper(BSTNode<T> node)
            {
                if (node == null)
                {
                    return;
                }

                output[i++] = node.Value;
                POTHelper(node.Left);
                POTHelper(node.Right);
            }

            POTHelper(_root);

            return output;
        }

        /// <summary>Returns the list of the elements stored in the <see cref="BinarySearchTree{T}" /> post-order. <code>Complexity: O(N)</code></summary>
        /// <returns>List of post-order elements.</returns>
        public T[] PostOrderTraverse()
        {
            var output = new T[Count];
            int i      = 0;

            void POTHelper(BSTNode<T> node)
            {
                if (node == null)
                {
                    return;
                }

                POTHelper(node.Left);
                POTHelper(node.Right);
                output[i++] = node.Value;
            }

            POTHelper(_root);

            return output;
        }

        private int Compare(T x, T y)
        {
            return _comparer.Compare(x, y);
        }
    }
}
