using System;
using System.Collections.Generic;

namespace DStruct.Heaps
{
    /// <summary>Represents a <see cref="BinaryHeap{T}"/> in which the value of each node is greater than the value of all its children.</summary>
    /// <typeparam name="T">The type of the values stored in the tree. It must implement the <see cref="IComparable{T}"/> interface.</typeparam>
    public class MaxHeap<T> : BinaryHeap<T> 
        where T : IComparable<T>
    {
        /// <summary>Initializes a new instance of <see cref="MaxHeap{T}"/> that is empty and has the default initial capacity.</summary>
        public MaxHeap() {}

        /// <summary>Initializes a new instance of <see cref="MaxHeap{T}"/> that contains every element from the input collection
        /// and has enough capacity to accomodate the number of elements copied.</summary>
        /// <param name="collection">The collection of elements to add to the new <see cref="MaxHeap{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public MaxHeap(IEnumerable<T> collection) : base(collection) {}

        /// <summary>Initializes a new instance of <see cref="MaxHeap{T}"/> that is empty and has the specified initial capacity.</summary>
        /// <param name="capacity">The number of elements that the new <see cref="MaxHeap{T}"/> can initially store.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is not in a valid range.</exception>
        public MaxHeap(int capacity) : base(capacity) {}

        /// <summary>Initializes a new instance of <see cref="MaxHeap{T}"/> that is empty and uses the specified <see cref="IComparer{T}"/>.</summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> that will be used for making comparisons.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> is <c>null</c>.</exception>
        public MaxHeap(IComparer<T> comparer) : base(comparer) {}

        /// <summary>Inserts a new element into the <see cref="MaxHeap{T}"/>. <code>Complexity: O(LogN)</code></summary>
        /// <param name="value">The element to be inserted into the <see cref="MaxHeap{T}"/>.</param>
        public override void Push(T value)
        {
            // Add the new node at the end of the heap
            _heap.Add(value);
            int i = Count - 1;

            // Swap it with its parent until it's either in root position or its parent has a bigger value
            while (i != 0 && Compare(_heap[i], _heap[Parent(i)]) > 0)
            {
                Swap(i, Parent(i));
                i = Parent(i);
            }
        }

        /// <summary>Ensures that the tree starting at the given <paramref name="root"/> respects the <see cref="MaxHeap{T}"/> properties.</summary>
        /// <param name="root">The root of the tree to transform into a <see cref="MaxHeap{T}"/>.</param>
        protected override void Heapify(int root)
        {
            int biggest = root;

            while (true)
            {
                int left  = Left(root);
                int right = Right(root);

                if (left < Count && Compare(_heap[left], _heap[biggest]) > 0)
                {
                    biggest = left;
                }

                if (right < Count && Compare(_heap[right], _heap[biggest]) > 0)
                {
                    biggest = right;
                }

                if (biggest == root) break;

                Swap(root, biggest);
                root = biggest;
            }
        }
    }
}
