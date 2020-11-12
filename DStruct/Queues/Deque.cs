using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DStruct.Queues
{
    /// <summary>Represents a double ended queue for which insertion and retrieval of elements close to either of the ends is very efficient.</summary>
    /// <typeparam name="T">The type of the values stored in the <see cref="Deque{T}"/>.</typeparam>
    public class Deque<T> : IEnumerable<T>
    {
        private const int DEFAULT_CAPACITY = 8;

        private T[] _deque;
        private int _offset;

        /// <summary>Gets the number of elements contained in the <see cref="Deque{T}"/>.</summary>
        public int Count { get; private set; }

        /// <summary>Gets the maximum number of elements the internal data structure can hold without resizing.</summary>
        public int Capacity
        {
            get => _deque.Length;
            private set
            {
                var newDeque = new T[value];
                CopyTo(newDeque);

                _deque  = newDeque;
                _offset = 0;
            }
        }

        /// <summary>Returns the first element at the rear of the <see cref="Deque{T}"/> without removing it.
        /// <code>Complexity: O(1)</code></summary>
        /// <exception cref="InvalidOperationException"><see cref="Deque{T}"/> is empty.</exception>
        public T Back
        {
            get
            {
                if (Count == 0)
                {
                    throw new InvalidOperationException("The Deque is empty.");
                }

                return GetItem(Count - 1);
            }
        }

        /// <summary>Returns the first element at the front of the <see cref="Deque{T}"/> without removing it.
        /// <code>Complexity: O(1)</code></summary>
        /// <exception cref="InvalidOperationException"><see cref="Deque{T}"/> is empty.</exception>
        public T Front
        {
            get
            {
                if (Count == 0)
                {
                    throw new InvalidOperationException("The Deque is empty.");
                }

                return GetItem(0);
            }
        }

        /// <summary>Gets or sets the element at the specified position of the <see cref="Deque{T}"/>. 
        /// <code>Complexity: O(1)</code>
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is out of the bounds of the <see cref="Deque{T}"/>.</exception>
        public T this[int index]
        {
            get
            {
                CheckIndexPrecondition(index);
                return GetItem(index);
            }
            set
            {
                CheckIndexPrecondition(index);
                SetItem(index, value);
            }
        }

        /// <summary>Initializes a new instance of <see cref="Deque{T}"/> that is empty and has the default initial capacity.</summary>
        public Deque() : this(DEFAULT_CAPACITY)
        {
        }

        /// <summary>Initializes a new instance of <see cref="Deque{T}"/> that is empty and has the specified initial capacity.</summary>
        /// <param name="capacity">The number of elements that the new <see cref="Deque{T}"/> can initially store.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is negative.</exception>
        public Deque(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be negative.");
            }

            _deque = new T[capacity];
        }

        /// <summary>Initializes a new instance of <see cref="Deque{T}"/> that contains every element from the input collection
        /// and has enough capacity to accomodate the number of elements copied.</summary>
        /// <param name="collection">The collection of elements to add to the new <see cref="Deque{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is null.</exception>
        public Deque(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var tmp = collection as T[] ?? collection.ToArray();

            if (tmp.Length > 0)
            {
                _deque = tmp;
                Count  = _deque.Length;
            }
            else
            {
                _deque = new T[DEFAULT_CAPACITY];
            }
        }

        /// <summary>Inserts a new element to the rear of the <see cref="Deque{T}"/>. 
        /// <code>Complexity: O(1)</code></summary>
        /// <param name="item">The element to be inserted into the <see cref="Deque{T}"/>.</param>
        public void PushBack(T item)
        {
            EnsureCapacity();
            SetItem(Count, item);
            Count++;
        }

        /// <summary>Inserts a new element to the front of the <see cref="Deque{T}"/>. 
        /// <code>Complexity: O(1)</code></summary>
        /// <param name="item">The element to be inserted into the <see cref="Deque{T}"/>.</param>
        public void PushFront(T item)
        {
            EnsureCapacity();
            DecrementOffset(1);
            _deque[_offset] = item;
            Count++;
        }

        /// <summary>Removes the element at the rear of the <see cref="Deque{T}"/> and returns its value. 
        /// <code>Complexity: O(1)</code></summary>
        /// <returns>The element at the rear of the <see cref="Deque{T}"/>.</returns>
        /// <exception cref="InvalidOperationException"><see cref="Deque{T}"/> is empty.</exception>
        public T PopBack()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The Deque is empty.");
            }

            Count--;
            return GetItem(Count);
        }

        /// <summary>Removes the element at the front of the <see cref="Deque{T}"/> and returns its value. 
        /// <code>Complexity: O(1)</code></summary>
        /// <returns>The element at the front of the <see cref="Deque{T}"/>.</returns>
        /// <exception cref="InvalidOperationException"><see cref="Deque{T}"/> is empty.</exception>
        public T PopFront()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The Deque is empty.");
            }

            Count--;
            T ret = GetItem(0);
            IncrementOffset(1);
            return ret;
        }

        /// <summary>Removes all elements from the <see cref="Deque{T}"/>. 
        /// <code>Complexity: O(1)</code></summary>
        public void Clear()
        {
            _offset = 0;
            Count   = 0;
        }

        /// <summary>Determines whether the <see cref="Deque{T}"/> contains a specific element.
        /// <code>Complexity: O(N)</code></summary>
        /// <param name="item">The element to locate in the <see cref="Deque{T}"/>.</param>
        /// <returns><c>true</c> if the <see cref="Deque{T}"/> contains <paramref name="item"/>; <c>false</c> otherwise.</returns>
        public bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < Count; i++)
            {
                if (comparer.Equals(GetItem(i), item))
                    return true;
            }

            return false;
        }

        /// <summary>Copies the elements of the <see cref="Deque{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index. <code>Complexity: O(N)</code></summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="Deque{T}"/>.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is out of the bounds of <paramref name="array"/>.</exception>
        /// <exception cref="ArgumentException">The number of elements in the source <see cref="Deque{T}"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 ||
                arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (arrayIndex + Count > array.Length)
            {
                throw new ArgumentException("Destination doesn't have enough available space.", nameof(array));
            }

            if (IsSplit)
            {
                int first = Capacity - _offset;
                Array.Copy(_deque, _offset, array, arrayIndex,         first);
                Array.Copy(_deque, 0,       array, arrayIndex + first, Count - first);
            }
            else
            {
                Array.Copy(_deque, _offset, array, arrayIndex, Count);
            }
        }

        /// <summary>Removes the first occurrence of a specific element from the <see cref="Deque{T}"/>. <code>Complexity: O(N)</code></summary>
        /// <param name="item">The element to remove from the <see cref="Deque{T}"/></param>
        /// <returns><c>true</c> if the element was successfully removed from the <see cref="Deque{T}"/>; <c>false</c> otherwise.</returns>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        /// <summary>Searches for the specified element and returns the zero-based index of the first occurrence within the entire <see cref="Deque{T}"/>. 
        /// <code>Complexity: O(N)</code></summary>
        /// <param name="item">The object to locate in the <see cref="Deque{T}"/>.</param>
        /// <returns>The zero-based index of the first occurrence of item within the entire <see cref="Deque{T}"/>, if found; otherwise, –1.</returns>
        public int IndexOf(T item)
        {
            var comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < Count; i++)
            {
                if (comparer.Equals(GetItem(i), item))
                    return i;
            }

            return -1;
        }

        /// <summary>Inserts an element into the <see cref="Deque{T}"/> at the specified index. <code>Complexity: O(N)</code></summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The element to insert into the <see cref="Deque{T}"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is out of the bounds of the <see cref="Deque{T}"/>.</exception>
        public void Insert(int index, T item)
        {
            CheckIndexPrecondition(index);
            EnsureCapacity();

            if (index < (Count + 1) / 2)
            {
                DecrementOffset(1);

                for (int i = 0; i <= index; i++)
                {
                    _deque[ConvertIndex(i)] = _deque[ConvertIndex(i + 1)];
                }
            }
            else
            {
                for (int i = Count; i > index; i--)
                {
                    _deque[ConvertIndex(i)] = _deque[ConvertIndex(i - 1)];
                }
            }

            SetItem(index, item);
            Count++;
        }

        /// <summary>Removes the element at the specified index of the <see cref="Deque{T}"/>. <code>Complexity: O(N)</code></summary>
        /// <param name="index">The zero-based index of the element to remove from the <see cref="Deque{T}"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is out of the bounds of the <see cref="Deque{T}"/>.</exception>
        public void RemoveAt(int index)
        {
            CheckIndexPrecondition(index);

            Count--;

            if (index < (Count + 1) / 2)
            {
                for (int i = index; i > 0; i--)
                {
                    _deque[ConvertIndex(i)] = _deque[ConvertIndex(i - 1)];
                }

                IncrementOffset(1);
            }
            else
            {
                for (int i = index; i < Count; i++)
                {
                    _deque[ConvertIndex(i)] = _deque[ConvertIndex(i + 1)];
                }
            }
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="Deque{T}"/>.</summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="Deque{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return GetItem(i);
            }
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="Deque{T}"/>.</summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the <see cref="Deque{T}"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Helpers

        private bool IsFull => Count == Capacity;
        private bool IsSplit => _offset + Count > Capacity;

        private T GetItem(int index)
        {
            return _deque[ConvertIndex(index)];
        }

        private void SetItem(int index, T value)
        {
            _deque[ConvertIndex(index)] = value;
        }

        private int ConvertIndex(int index)
        {
            return (_offset + index) % Capacity;
        }

        private void CheckIndexPrecondition(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is outside the bounds of the Deque.");
            }
        }

        private void IncrementOffset(int value)
        {
            _offset = (_offset + value) % Capacity;
        }

        private void DecrementOffset(int value)
        {
            _offset -= value;
            if (_offset < 0)
                _offset += Capacity;
        }

        private void EnsureCapacity()
        {
            if (IsFull)
            {
                Capacity = Capacity == 0 ? 1 : Capacity * 2;
            }
        }

        #endregion
    }
}
