using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DStruct.BinaryTrees
{
    public abstract class BinarySearchTreeBase<TNode, TValue> : IBinarySearchTree<TValue>
        where TNode : IBinarySearchTreeNode<TValue>
    {
        private readonly IComparer<TValue> _comparer = Comparer<TValue>.Default;
        protected TNode _root;

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to get from the <see cref="BinarySearchTreeBase{TNode, TValue}"/>.</param>
        /// <returns>The element at the specified index.</returns>
        public abstract TValue this[int index] { get; }

        /// <summary>
        /// Gets the number of elements stored in the <see cref="IBinarySearchTree{TValue}"/>.
        /// </summary>
        public virtual int Count { get; protected set; }

        /// <summary>
        /// Gets the maximum value element stored in the <see cref="BinarySearchTreeBase{TNode, TValue}" />.
        /// </summary>
        public abstract TValue Min { get; }

        /// <summary>
        /// Gets the minimum value element stored in the <see cref="BinarySearchTreeBase{TNode, TValue}"/>.
        /// </summary>
        public abstract TValue Max { get; }

        /// <summary>Initializes a new instance of <see cref="BinarySearchTreeBase{TNode, TValue}"/> that is empty.</summary>
        public BinarySearchTreeBase()
        {
        }

        /// <summary>Initializes a new instance of <see cref="BinarySearchTreeBase{TNode, TValue}"/> that contains every item from the input collection.</summary>
        /// <param name="collection">The collection of elements to add to the <see cref="BinarySearchTreeBase{TNode, TValue}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public BinarySearchTreeBase(IEnumerable<TValue> collection)
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
        /// Initializes a new instance of <see cref="BinarySearchTreeBase{TNode, TValue}"/> that is empty and uses the specified <see cref="IComparer{TValue}"/>.
        /// </summary>
        /// <param name="comparer"></param>
        public BinarySearchTreeBase(IComparer<TValue> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        /// <summary>
        /// Determines whether the <see cref="BinarySearchTreeBase{TNode, TValue}" /> contains a specific value.
        /// </summary>
        /// <param name="value">The element to locate in the <see cref="BinarySearchTreeBase{TNode, TValue}"/>.</param>
        /// <returns><c>true</c> if the <see cref="BinarySearchTreeBase{TNode, TValue}"/> contains <paramref name="value"/>; <c>false</c> otherwise.</returns>
        public abstract bool Find(TValue value);

        /// <summary>
        /// Inserts an element into the <see cref="BinarySearchTreeBase{TNode, TValue}" /> and returns its index.
        /// </summary>
        /// <param name="value">The element to add to the <see cref="BinarySearchTreeBase{TNode, TValue}"/>.</param>
        /// <returns>The index at which the element was placed.</returns>
        public abstract int Insert(TValue value);

        /// <summary>
        /// Removes one occurrence of a specific element from the <see cref="BinarySearchTreeBase{TNode, TValue}" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool Remove(TValue value);

        public virtual IEnumerator<TValue> GetEnumerator() => InOrderTraverse().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{TValue}" /> in-order. <code>Complexity: O(N)</code></summary>
        /// <returns>List of in-order elements.</returns>
        public virtual IEnumerable<TValue> InOrderTraverse()
        {
            var nodeStack = new Stack<IBinarySearchTreeNode<TValue>>();

            IBinarySearchTreeNode<TValue> currrentNode = _root;

            while (currrentNode != null || nodeStack.Count > 0)
            {
                while (currrentNode != null)
                {
                    nodeStack.Push(currrentNode);
                    currrentNode = currrentNode.Left;
                }

                currrentNode = nodeStack.Pop();

                yield return currrentNode.Value;

                currrentNode = currrentNode.Right;
            }
        }

        /// <summary>Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{TValue}" /> pre-order. <code>Complexity: O(N)</code></summary>
        /// <returns>List of pre-order elements.</returns>
        public virtual IEnumerable<TValue> PreOrderTraverse()
        {
            var nodeQueue = new LinkedList<IBinarySearchTreeNode<TValue>>();
            nodeQueue.AddLast(_root);

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

        /// <summary>Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{TValue}" /> pre-order. <code>Complexity: O(N)</code></summary>
        /// <returns>List of breadth-first-search elements.</returns>
        public virtual IEnumerable<TValue> BreadthFirstSearch()
        {
            var nodeQueue = new Queue<IBinarySearchTreeNode<TValue>>();
            nodeQueue.Enqueue(_root);

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

        /// <summary>Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{TValue}" /> post-order. <code>Complexity: O(N)</code></summary>
        /// <returns>List of post-order elements.</returns>
        public virtual IEnumerable<TValue> PostOrderTraverse()
        {
            var nodeStack = new Stack<IBinarySearchTreeNode<TValue>>();

            IBinarySearchTreeNode<TValue> currrentNode = _root;

            while (currrentNode != null || nodeStack.Count > 0)
            {
                while (currrentNode != null)
                {
                    nodeStack.Push(currrentNode);
                    currrentNode = currrentNode.Right;
                }

                currrentNode = nodeStack.Pop();

                yield return currrentNode.Value;

                currrentNode = currrentNode.Left;
            }
        }

        protected virtual int Compare(TValue x, TValue y)
        {
            return _comparer.Compare(x, y);
        }
    }
}
