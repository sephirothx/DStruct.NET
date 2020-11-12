using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DStruct.Sparse
{
    /// <summary>Provides a memory efficient implementation of an array in which most of the elements have the default value.</summary>
    /// <typeparam name="T">The type of the elements in the <see cref="SparseArray{T}"/>.</typeparam>
    public class SparseArray<T> : IEnumerable<T>
    {
        private readonly IDictionary<int, T> _dictionary = new Dictionary<int, T>();

        private readonly bool _hasFixedLength;
        private readonly T    _default;

        /// <summary>Gets the number of elements with value other than <c>default</c>.</summary>
        public int Count => _dictionary.Count;

        /// <summary>Gets the total length of the <see cref="SparseArray{T}"/>.</summary>
        public int Length { get; private set; }

        /// <summary>Gets an <see cref="ICollection{T}"/> containing the indices of the elements with value other than <c>default</c>.</summary>
        public ICollection<int> Indices => _dictionary.Keys;

        /// <summary>Gets an <see cref="ICollection{T}"/> containing the elements with value other than <c>default</c>.</summary>
        public ICollection<T> Values => _dictionary.Values;

        /// <summary>Initializes a new instance of <see cref="SparseArray{T}"/> with no specified length.</summary>
        public SparseArray()
        {
        }

        /// <summary>Initializes a new instance of <see cref="SparseArray{T}"/> with the fixed specified length.</summary>
        /// <param name="length">The fixed length of the <see cref="SparseArray{T}"/>.</param>
        public SparseArray(int length)
        {
            Length          = length;
            _hasFixedLength = true;
        }

        /// <summary>Initializes a new instance of <see cref="SparseArray{T}"/> with the specified default element value.</summary>
        /// <param name="defaultValue">The default value of the elements in the <see cref="SparseArray{T}"/>.</param>
        public SparseArray(T defaultValue)
        {
            _default = defaultValue;
        }

        /// <summary>Initializes a new instance of <see cref="SparseArray{T}"/> with the fixed specified length and default element value.</summary>
        /// <param name="length">The fixed length of the <see cref="SparseArray{T}"/>.</param>
        /// <param name="defaultValue">The default value of the elements in the <see cref="SparseArray{T}"/>.</param>
        public SparseArray(int length, T defaultValue)
        {
            Length          = length;
            _hasFixedLength = true;
            _default        = defaultValue;
        }

        /// <summary>Gets or sets the value of the element at the specified index of the <see cref="SparseArray{T}"/>. <code>Complexity: O(1)</code></summary>
        /// <param name="index">The index of the value to get or set.</param>
        public T this[int index]
        {
            get
            {
                if (index < 0 ||
                    _hasFixedLength && index >= Length)
                {
                    throw new IndexOutOfRangeException("Index is outside the bounds of the SparseArray.");
                }

                if (!_hasFixedLength)
                {
                    Length = Math.Max(Length, index + 1);
                }

                return _dictionary.TryGetValue(index, out T value) ? value : _default;
            }
            set
            {
                if (index < 0 ||
                    _hasFixedLength && index >= Length)
                {
                    throw new IndexOutOfRangeException("Index is outside the bounds of the SparseArray.");
                }

                if (_dictionary.ContainsKey(index))
                {
                    _dictionary[index] = value;
                }
                else
                {
                    _dictionary.Add(index, value);
                }

                if (!_hasFixedLength)
                {
                    Length = Math.Max(Length, index + 1);
                }
            }
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="SparseArray{T}"/>.</summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="SparseArray{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return _dictionary.TryGetValue(i, out T value) ? value : _default;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="KeyValuePair{TKey,TValue}"/>
        /// containing the index and value of the elements of the <see cref="SparseArray{T}"/>.</summary>
        public IEnumerable<KeyValuePair<int, T>> GetKeyValuePairs()
        {
            return _dictionary.OrderBy(item => item.Key);
        }
    }
}
