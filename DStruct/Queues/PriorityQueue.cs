using System;
using System.Collections;
using System.Collections.Generic;
using DStruct.Heaps;

namespace DStruct.Queues
{
    /// <summary>Represents a node of the <see cref="PriorityQueue{T}"/>.</summary>
    /// <typeparam name="T">The type of the elements in the <see cref="PriorityQueue{T}"/>.</typeparam>
    public readonly struct PriorityQueueNode<T> : IComparable<PriorityQueueNode<T>>
    {
        /// <summary>Gets the value of the <see cref="PriorityQueueNode{T}"/>.</summary>
        public T Value { get; }

        /// <summary>Gets the priority of the <see cref="PriorityQueueNode{T}"/>.</summary>
        public int Priority { get; }

        internal PriorityQueueNode(T value, int priority)
        {
            Value    = value;
            Priority = priority;
        }

        /// <summary>Compares the current instance with another <see cref="PriorityQueueNode{T}"/> and returns an integer that indicates
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the other.</summary>
        /// <param name="other">A <see cref="PriorityQueueNode{T}"/> to compare with this instance.</param>
        public int CompareTo(PriorityQueueNode<T> other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }

    /// <summary>Specifies the ordering criterion used for the implementation of the <see cref="PriorityQueue{T}"/>.</summary>
    public enum PriorityQueueOrder
    {
        /// <summary>Higher priority first</summary>
        Ascending,

        /// <summary>Lower priority first</summary>
        Descending
    }

    /// <summary>Represents a Queue in which the elements are ordered using a specified priority.</summary>
    /// <typeparam name="T">The type of the elements in the <see cref="PriorityQueue{T}"/>.</typeparam>
    public class PriorityQueue<T> : IEnumerable<T>
    {
        private readonly BinaryHeap<PriorityQueueNode<T>> _heap;

        /// <summary>Gets the number of elements contained in the <see cref="PriorityQueue{T}"/>.</summary>
        public int Count => _heap.Count;

        /// <summary>Determines whether the <see cref="PriorityQueue{T}"/> is empty.</summary>
        public bool IsEmpty => _heap.IsEmpty;

        /// <summary>Initializes a new instance of <see cref="PriorityQueue{T}"/> which is
        /// empty and ordered by ascending priority value (higher priority first).</summary>
        public PriorityQueue() : this(PriorityQueueOrder.Ascending)
        {
        }

        /// <summary>Initializes a new instance of <see cref="PriorityQueue{T}"/> which is
        /// empty and ordered by ascending or descending priority value.</summary>
        /// <param name="order">Specifies whether the priority queue is ordered by ascending or descending priority value.</param>
        public PriorityQueue(PriorityQueueOrder order)
        {
            switch (order)
            {
            case PriorityQueueOrder.Ascending:
                _heap = new MaxHeap<PriorityQueueNode<T>>();
                break;
            case PriorityQueueOrder.Descending:
                _heap = new MinHeap<PriorityQueueNode<T>>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }
        }

        /// <summary>Initializes a new instance of <see cref="PriorityQueue{T}"/> which is ordered by
        /// ascending or descending priority value and has the specified initial capacity.</summary>
        /// <param name="capacity">The number of elements that the new <see cref="PriorityQueue{T}"/> can initially store.</param>
        /// <param name="order">Specifies whether the priority queue is ordered by ascending or descending priority value.</param>
        public PriorityQueue(int capacity, PriorityQueueOrder order = PriorityQueueOrder.Ascending)
        {
            switch (order)
            {
            case PriorityQueueOrder.Ascending:
                _heap = new MaxHeap<PriorityQueueNode<T>>(capacity);
                break;
            case PriorityQueueOrder.Descending:
                _heap = new MinHeap<PriorityQueueNode<T>>(capacity);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }
        }

        /// <summary>Inserts a new item into the <see cref="PriorityQueue{T}"/>, in a position
        /// relative to the given <paramref name="priority"/>. <code>Complexity: O(LogN)</code></summary>
        /// <param name="value">The element to be inserted into the <see cref="PriorityQueue{T}"/>.</param>
        /// <param name="priority">The priority of the element that is being inserted into the <see cref="PriorityQueue{T}"/>.</param>
        public void Push(T value, int priority)
        {
            _heap.Push(new PriorityQueueNode<T>(value, priority));
        }

        /// <summary>Removes the element with the highest (or lowest) priority from the <see cref="PriorityQueue{T}"/> and returns its value.
        /// <code>Complexity: O(LogN)</code></summary>
        /// <returns>The element with the highest (or lowest) priority.</returns>
        /// <exception cref="InvalidOperationException"><see cref="PriorityQueue{T}"/> is empty.</exception>
        public PriorityQueueNode<T> Pop()
        {
            return _heap.Pop();
        }

        /// <summary>Returns the <see cref="PriorityQueueNode{T}"/> with the highest (or lowest) priority in the <see cref="PriorityQueue{T}"/>
        /// wihout removing it. <code>Complexity: O(1)</code></summary>
        /// <returns>The element with the highest (or lowest) priority.</returns>
        /// <exception cref="InvalidOperationException"><see cref="PriorityQueue{T}"/> is empty.</exception>
        public PriorityQueueNode<T> Peek()
        {
            return _heap.Peek();
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="PriorityQueue{T}"/>.</summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="PriorityQueue{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            while (!_heap.IsEmpty)
            {
                yield return _heap.Pop().Value;
            }
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="PriorityQueue{T}"/>.</summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the <see cref="PriorityQueue{T}"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
