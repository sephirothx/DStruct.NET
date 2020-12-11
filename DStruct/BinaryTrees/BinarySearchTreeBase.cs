using System;
using System.Collections.Generic;

// ReSharper disable VirtualMemberCallInConstructor

namespace DStruct.BinaryTrees
{
    /// <summary>
    /// Represents a Binary Tree in which the elements are stored in a way that allows operations to use the principle of Binary Search.
    /// </summary>
    /// <typeparam name="T">The type of the values stored in the tree.</typeparam>
    public abstract class BinarySearchTreeBase<T> : IBinarySearchTree<T>
    {
        private readonly IComparer<T> _comparer = Comparer<T>.Default;

        /// <summary>
        /// Root used for traversals methods
        /// </summary>
        private protected abstract IBinarySearchTreeNode<T> Root { get; }

        /// <summary>
        /// Gets the number of elements stored in the <see cref="IBinarySearchTree{T}"/>.
        /// </summary>
        public virtual int Count { get; protected set; }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to get from the <see cref="BinarySearchTreeBase{T}"/>.</param>
        /// <returns>The element at the specified index.</returns>
        public virtual T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Index is outside the bounds of the Binary Search Tree.");
                }

                var curr = Root;
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
                        curr = curr.Right;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the minimum value element stored in the <see cref="BinarySearchTreeBase{T}"/>.
        /// <code>Complexity: O(LogN)</code>
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="BinarySearchTreeBase{T}"/> is empty.</exception>
        public virtual T Min
        {
            get
            {
                if (Root == null)
                {
                    throw new InvalidOperationException("The Red-Black Tree is empty.");
                }

                var curr = Root;
                while (curr.Left != null)
                {
                    curr = curr.Left;
                }

                return curr.Value;
            }
        }

        /// <summary>
        /// Gets the maximum value element stored in the <see cref="BinarySearchTreeBase{T}" />.
        /// <code>Complexity: O(LogN)</code>
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="BinarySearchTreeBase{T}"/> is empty.</exception>
        public virtual T Max
        {
            get
            {
                if (Root == null)
                {
                    throw new InvalidOperationException("The Red-Black Tree is empty.");
                }

                var curr = Root;
                while (curr.Right != null)
                {
                    curr = curr.Right;
                }

                return curr.Value;
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BinarySearchTreeBase{T}"/> that is empty.
        /// </summary>
        protected BinarySearchTreeBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BinarySearchTreeBase{T}"/> that contains every item from the input collection.
        /// </summary>
        /// <param name="collection">The collection of elements to add to the <see cref="BinarySearchTreeBase{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        protected BinarySearchTreeBase(IEnumerable<T> collection)
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

        /// <summary>
        /// Initializes a new instance of <see cref="BinarySearchTreeBase{T}"/> that is empty and uses the specified <see cref="IComparer{T}"/>.
        /// </summary>
        /// <param name="comparer"></param>
        protected BinarySearchTreeBase(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        /// <summary>
        /// Determines whether the <see cref="BinarySearchTreeBase{T}" /> contains a specific value.
        /// </summary>
        /// <param name="value">The element to locate in the <see cref="BinarySearchTreeBase{T}"/>.</param>
        /// <returns><c>true</c> if the <see cref="BinarySearchTreeBase{T}"/> contains <paramref name="value"/>; <c>false</c> otherwise.</returns>
        public abstract bool Find(T value);

        /// <summary>
        /// Inserts an element into the <see cref="BinarySearchTreeBase{T}" /> and returns its index.
        /// </summary>
        /// <param name="value">The element to add to the <see cref="BinarySearchTreeBase{T}"/>.</param>
        /// <returns>The index at which the element was placed.</returns>
        public abstract int Insert(T value);

        /// <summary>
        /// Removes one occurrence of a specific element from the <see cref="BinarySearchTreeBase{T}" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool Remove(T value);

        /// <summary>
        /// Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{T}" /> in-order.
        /// <code>Complexity: O(N)</code>
        /// </summary>
        /// <returns>List of in-order elements.</returns>
        public virtual IEnumerable<T> InOrderTraverse()
        {
            var nodeStack   = new Stack<IBinarySearchTreeNode<T>>();
            var currentNode = Root;

            while (currentNode != null || nodeStack.Count > 0)
            {
                while (currentNode != null)
                {
                    nodeStack.Push(currentNode);
                    currentNode = currentNode.Left;
                }

                currentNode = nodeStack.Pop();

                yield return currentNode.Value;

                currentNode = currentNode.Right;
            }
        }

        /// <summary>
        /// Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{T}" /> pre-order.
        /// <code>Complexity: O(N)</code>
        /// </summary>
        /// <returns>List of pre-order elements.</returns>
        public virtual IEnumerable<T> PreOrderTraverse()
        {
            var nodeQueue = new LinkedList<IBinarySearchTreeNode<T>>();
            nodeQueue.AddLast(Root);

            while (nodeQueue.Count > 0)
            {
                var currentNode = nodeQueue.First.Value;
                nodeQueue.RemoveFirst();

                if (currentNode != null)
                {
                    yield return currentNode.Value;

                    nodeQueue.AddFirst(currentNode.Left);
                    nodeQueue.AddLast(currentNode.Right);
                }
            }
        }

        /// <summary>
        /// Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{T}" /> post-order.
        /// <code>Complexity: O(N)</code>
        /// </summary>
        /// <returns>List of post-order elements.</returns>
        public virtual IEnumerable<T> PostOrderTraverse()
        {
            var nodeStack   = new Stack<IBinarySearchTreeNode<T>>();
            var currentNode = Root;

            while (currentNode != null || nodeStack.Count > 0)
            {
                while (currentNode != null)
                {
                    nodeStack.Push(currentNode);
                    currentNode = currentNode.Right;
                }

                currentNode = nodeStack.Pop();

                yield return currentNode.Value;

                currentNode = currentNode.Left;
            }
        }

        /// <summary>
        /// Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{T}" /> ordered by level.
        /// <code>Complexity: O(N)</code>
        /// </summary>
        /// <returns>List of breadth-first-search elements.</returns>
        public virtual IEnumerable<T> BreadthFirstSearch()
        {
            var nodeQueue = new Queue<IBinarySearchTreeNode<T>>();
            nodeQueue.Enqueue(Root);

            while (nodeQueue.Count > 0)
            {
                var currentNode = nodeQueue.Dequeue();

                if (currentNode != null)
                {
                    yield return currentNode.Value;

                    nodeQueue.Enqueue(currentNode.Left);
                    nodeQueue.Enqueue(currentNode.Right);
                }
            }
        }

        private protected virtual int Compare(T x, T y)
        {
            return _comparer.Compare(x, y);
        }
    }
}
