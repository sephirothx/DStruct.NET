using System;
using System.Collections.Generic;

namespace DStruct.Heaps
{
    /// <summary>Represents a <see cref="BinaryHeap{T}"/> in which the value of each node is smaller than the value of all its children.</summary>
    /// <typeparam name="T">The type of the values stored in the tree. It must implement the <see cref="IComparable{T}"/> interface.</typeparam>
    public class MinHeap<T> : BinaryHeap<T> 
        where T : IComparable<T>
    {
        /// <summary>Initializes a new instance of <see cref="MinHeap{T}"/> that is empty and has the default initial capacity.</summary>
        public MinHeap() {}

        /// <summary>Initializes a new instance of <see cref="MinHeap{T}"/> that contains every element from the input collection
        /// and has enough capacity to accomodate the number of elements copied.</summary>
        /// <param name="collection">The collection of elements to add to the new <see cref="MinHeap{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public MinHeap(IEnumerable<T> collection) : base(collection) {}

        /// <summary>Initializes a new instance of <see cref="MinHeap{T}"/> that is empty and has the specified initial capacity.</summary>
        /// <param name="capacity">The number of elements that the new <see cref="MinHeap{T}"/> can initially store.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is not in a valid range.</exception>
        public MinHeap(int capacity) : base(capacity) {}

        /// <summary>Initializes a new instance of <see cref="MinHeap{T}"/> that is empty and uses the specified <see cref="IComparer{T}"/>.</summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> that will be used for making comparisons.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> is <c>null</c>.</exception>
        public MinHeap(IComparer<T> comparer) : base(comparer) {}

        /// <summary>Inserts a new item into the <see cref="MinHeap{T}"/>. <code>Complexity: O(LogN)</code></summary>
        /// <param name="value">The element to be inserted into the <see cref="MinHeap{T}"/>.</param>
        public override void Push(T value)
        {
            // Add the new node at the end of the heap
            _heap.Add(value);
            int i = Count - 1;

            // Swap it with its parent until it's either in root position or its parent has a smaller value
            while (i != 0 && Compare(_heap[i], _heap[Parent(i)]) < 0)
            {
                Swap(i, Parent(i));
                i = Parent(i);
            }
        }

        /// <summary>Ensures that the tree starting at the given <paramref name="root"/> respects the <see cref="MinHeap{T}"/> properties.</summary>
        /// <param name="root">The root of the tree to transform into a <see cref="MinHeap{T}"/>.</param>
        protected override void Heapify(int root)
        {
            int smallest = root;

            while (true)
            {
                int left  = Left(root);
                int right = Right(root);

                if (left < Count && Compare(_heap[left], _heap[smallest]) < 0)
                {
                    smallest = left;
                }

                if (right < Count && Compare(_heap[right], _heap[smallest]) < 0)
                {
                    smallest = right;
                }

                if (smallest == root) break;

                Swap(root, smallest);
                root = smallest;
            }
        }
    }
}
