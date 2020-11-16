using System;
using System.Collections.Generic;

#pragma warning disable 1591

namespace DStruct.Heaps
{
    /// <summary>Represents a tree in which the value of each node is greater (<see cref="MaxHeap{T}"/>)
    /// or smaller (<see cref="MinHeap{T}"/>) than the value of all its children.</summary>
    /// <typeparam name="T">The type of the values stored in the heap.</typeparam>
    public abstract class BinaryHeap<T>
    {
        protected readonly IList<T>     _heap = new List<T>();
        protected readonly IComparer<T> _comparer = Comparer<T>.Default;

        /// <summary>Gets the number of elements contained in the <see cref="BinaryHeap{T}"/>.</summary>
        public int Count => _heap.Count;

        /// <summary>Determines whether the <see cref="BinaryHeap{T}"/> is empty.</summary>
        public bool IsEmpty => Count == 0;

        protected BinaryHeap()
        {
        }

        protected BinaryHeap(IEnumerable<T> collection)
        {
            _heap = new List<T>(collection);

            Heapify();
        }

        protected BinaryHeap(int capacity)
        {
            _heap = new List<T>(capacity);
        }

        protected BinaryHeap(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        /// <summary>Inserts a new element into the <see cref="BinaryHeap{T}"/>. <code>Complexity: O(LogN)</code></summary>
        /// <param name="value">The element to be inserted into the <see cref="BinaryHeap{T}"/>.</param>
        public abstract void Push(T value);

        protected abstract void Heapify(int root);

        /// <summary>Removes the element at the root of the <see cref="BinaryHeap{T}"/> and returns its value,
        /// maintaining the <see cref="BinaryHeap{T}"/> properties. <code>Complexity: O(LogN)</code></summary>
        /// <returns>The element at the root of the <see cref="BinaryHeap{T}"/>.</returns>
        /// <exception cref="InvalidOperationException"><see cref="BinaryHeap{T}"/> is empty.</exception>
        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            T ret = _heap[0];

            if (Count == 1)
            {
                _heap.RemoveAt(0);
                return ret;
            }

            Swap(0, Count - 1);
            _heap.RemoveAt(Count - 1);
            Heapify(0);

            return ret;
        }

        /// <summary>Returns the value of the root of the <see cref="BinaryHeap{T}"/> without removing it. <code>Complexity: O(1)</code></summary>
        /// <returns>The element at the root of the <see cref="BinaryHeap{T}"/>.</returns>
        /// <exception cref="InvalidOperationException"><see cref="BinaryHeap{T}"/> is empty.</exception>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            return _heap[0];
        }

        protected static int Parent(int i) => (i - 1) / 2;
        protected static int Right(int i) => (2 * i) + 2;
        protected static int Left(int i) => (2 * i) + 1;

        protected int Compare(T x, T y)
        {
            return _comparer.Compare(x, y);
        }

        protected void Swap(int i1, int i2)
        {
            T tmp = _heap[i1];
            _heap[i1] = _heap[i2];
            _heap[i2] = tmp;
        }

        protected void Heapify()
        {
            for (int i = Parent(Count - 1); i >= 0; i--)
            {
                Heapify(i);
            }
        }
    }
}
