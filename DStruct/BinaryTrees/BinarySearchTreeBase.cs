using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DStruct.BinaryTrees
{
    public abstract class BinarySearchTreeBase<TValue> : IBinarySearchTree<TValue>
    {
        private readonly IComparer<TValue> _comparer = Comparer<TValue>.Default;

        /// <summary>
        /// Root used for trasverals methods
        /// </summary>
        private protected abstract IBinarySearchTreeNode<TValue> Root { get; }

        /// <summary>
        /// Gets the number of elements stored in the <see cref="IBinarySearchTree{TValue}"/>.
        /// </summary>
        public virtual int Count { get; protected set; }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to get from the <see cref="BinarySearchTreeBase{TValue}"/>.</param>
        /// <returns>The element at the specified index.</returns>
        public virtual TValue this[int index]
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

        /// <summary>Gets the minimum value element stored in the <see cref="BinarySearchTreeBase{TValue}"/>. <code>Complexity: O(LogN)</code></summary>
        /// <exception cref="InvalidOperationException"><see cref="BinarySearchTreeBase{TValue}"/> is empty.</exception>
        public virtual TValue Min
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

        /// <summary>Gets the maximum value element stored in the <see cref="BinarySearchTreeBase{TValue}" />. <code>Complexity: O(LogN)</code></summary>
        /// <exception cref="InvalidOperationException"><see cref="BinarySearchTreeBase{TValue}"/> is empty.</exception>
        public virtual TValue Max
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

        /// <summary>Initializes a new instance of <see cref="BinarySearchTreeBase{TValue}"/> that is empty.</summary>
        public BinarySearchTreeBase()
        {
        }

        /// <summary>Initializes a new instance of <see cref="BinarySearchTreeBase{TValue}"/> that contains every item from the input collection.</summary>
        /// <param name="collection">The collection of elements to add to the <see cref="BinarySearchTreeBase{TValue}"/>.</param>
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
        /// Initializes a new instance of <see cref="BinarySearchTreeBase{TValue}"/> that is empty and uses the specified <see cref="IComparer{TValue}"/>.
        /// </summary>
        /// <param name="comparer"></param>
        public BinarySearchTreeBase(IComparer<TValue> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        /// <summary>
        /// Determines whether the <see cref="BinarySearchTreeBase{TValue}" /> contains a specific value.
        /// </summary>
        /// <param name="value">The element to locate in the <see cref="BinarySearchTreeBase{TValue}"/>.</param>
        /// <returns><c>true</c> if the <see cref="BinarySearchTreeBase{TValue}"/> contains <paramref name="value"/>; <c>false</c> otherwise.</returns>
        public abstract bool Find(TValue value);

        /// <summary>
        /// Inserts an element into the <see cref="BinarySearchTreeBase{TValue}" /> and returns its index.
        /// </summary>
        /// <param name="value">The element to add to the <see cref="BinarySearchTreeBase{TValue}"/>.</param>
        /// <returns>The index at which the element was placed.</returns>
        public abstract int Insert(TValue value);

        /// <summary>
        /// Removes one occurrence of a specific element from the <see cref="BinarySearchTreeBase{TValue}" />.
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

            IBinarySearchTreeNode<TValue> currrentNode = Root;

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

        /// <summary>Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{TValue}" /> pre-order. <code>Complexity: O(N)</code></summary>
        /// <returns>List of breadth-first-search elements.</returns>
        public virtual IEnumerable<TValue> BreadthFirstSearch()
        {
            var nodeQueue = new Queue<IBinarySearchTreeNode<TValue>>();
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

        /// <summary>Returns the list of the elements stored in the <see cref="IBinarySearchTreeNode{TValue}" /> post-order. <code>Complexity: O(N)</code></summary>
        /// <returns>List of post-order elements.</returns>
        public virtual IEnumerable<TValue> PostOrderTraverse()
        {
            var nodeStack = new Stack<IBinarySearchTreeNode<TValue>>();

            IBinarySearchTreeNode<TValue> currrentNode = Root;

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
