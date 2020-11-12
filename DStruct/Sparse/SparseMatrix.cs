using System;
using System.Collections;
using System.Collections.Generic;

namespace DStruct.Sparse
{
    struct Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>Provides a memory efficient implementation of a matrix in which most of the elements have the default value.</summary>
    /// <typeparam name="T">The type of the elements in the <see cref="SparseMatrix{T}"/>.</typeparam>
    public class SparseMatrix<T> : IEnumerable<T>
    {
        private readonly IDictionary<Coordinate, T> _dictionary = new Dictionary<Coordinate, T>();
        private readonly T _default;

        /// <summary>Gets the number of elements with value other than <c>default</c>.</summary>
        public int Count => _dictionary.Count;

        /// <summary>Gets the minimum X-index of the <see cref="SparseMatrix{T}"/>.</summary>
        public int MinX { get; private set; }

        /// <summary>Gets the minimum Y-index of the <see cref="SparseMatrix{T}"/>.</summary>
        public int MinY { get; private set; }

        /// <summary>Gets the max X-index of the <see cref="SparseMatrix{T}"/>.</summary>
        public int MaxX { get; private set; }

        /// <summary>Gets the max Y-index of the <see cref="SparseMatrix{T}"/>.</summary>
        public int MaxY { get; private set; }

        /// <summary>Initializes a new instance of <see cref="SparseMatrix{T}"/>.</summary>
        public SparseMatrix()
        {
        }

        /// <summary>Initializes a new instance of <see cref="SparseMatrix{T}"/> with the specified default element value.</summary>
        public SparseMatrix(T defaultValue)
        {
            _default = defaultValue;
        }

        /// <summary>Gets or sets the value of the element at the specified coordinate of the <see cref="SparseMatrix{T}"/>.
        /// <code>Complexity: O(1)</code></summary>
        /// <param name="index0"></param>
        /// <param name="index1"></param>
        public T this[int index0, int index1]
        {
            get => _dictionary.TryGetValue(new Coordinate(index0, index1), out T value)
                       ? value
                       : _default;
            set
            {
                var key = new Coordinate(index0, index1);

                if (_dictionary.ContainsKey(key))
                {
                    _dictionary[key] = value;
                }
                else
                {
                    _dictionary.Add(key, value);
                    UpdateBoundaries(index0, index1);
                }
            }
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="SparseMatrix{T}"/>.</summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="SparseMatrix{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void UpdateBoundaries(int x, int y)
        {
            MinX = Math.Min(MinX, x);
            MinY = Math.Min(MinY, y);
            MaxX = Math.Max(MaxX, x);
            MaxY = Math.Max(MaxY, y);
        }
    }
}
