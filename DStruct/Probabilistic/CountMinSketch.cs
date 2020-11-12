using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DStruct.Probabilistic
{
    /// <summary>Represents a probabilistic data structure that serves as a memory
    /// efficient frequency table for objects in a stream of data.</summary>
    public class CountMinSketch
    {
        private readonly IHashFunction _hash = new MurMurHash3();

        private readonly int _width;
        private readonly int _depth;

        private int[,] _count;

        /// <summary>Initializes a new instance of <see cref="CountMinSketch"/> in which the error of a query result is
        /// within a factor of <paramref name="epsilon"/> with probability 1 - <paramref name="delta"/>.</summary>
        /// <param name="epsilon">The error factor.</param>
        /// <param name="delta">The error probability.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="epsilon"/> or <paramref name="delta"/> is out of range.</exception>
        public CountMinSketch(double epsilon, double delta)
        {
            if (epsilon <= 0 || epsilon >= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(epsilon), epsilon, "Epsilon must be within this range: (0, 1).");
            }

            if (delta <= 0 || delta >= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(delta), delta, "Delta must be within this range: (0, 1).");
            }

            _width = (int)Math.Ceiling((Math.E) / epsilon);
            _depth = (int)Math.Ceiling(Math.Log(1 / delta));

            _count = new int[_depth, _width];
        }

        /// <summary>Initializes a new instance of <see cref="CountMinSketch"/> with the specified width and depth.</summary>
        /// <param name="width">The hash range.</param>
        /// <param name="depth">The number of hash functions.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="depth"/> is negative.</exception>
        public CountMinSketch(int width, int depth)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), width, "Width cannot be negative.");
            }

            if (depth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depth), depth, "Depth cannot be negative.");
            }

            _width = width;
            _depth = depth;

            _count = new int[_depth, _width];
        }

        /// <summary>Increments the count of the given object by one. <code>Complexity: O(1)</code></summary>
        /// <param name="obj">The object to increment count for.</param>
        /// <exception cref="ArgumentNullException"><paramref name="obj"/> is <c>null</c>.</exception>
        public void Add(object obj) => Add(obj, 1);

        /// <summary>Increments the count of the given object by a specified amount. <code>Complexity: O(1)</code></summary>
        /// <param name="obj">The object to increment count for.</param>
        /// <param name="count">The amount of the increment.</param>
        /// <exception cref="ArgumentNullException"><paramref name="obj"/> is <c>null</c>.</exception>
        public void Add(object obj, int count)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var hashes = GetHashes(obj, _width, _depth);

            for (int i = 0; i < _depth; i++)
            {
                _count[i, hashes[i]] += count;
            }
        }

        /// <summary>Estimates the count of an object in the <see cref="CountMinSketch"/>.
        /// The returned number is always equal to or greater than the actual count of the object.
        /// <code>Complexity: O(1)</code></summary>
        /// <param name="obj">The object to estimate count for.</param>
        /// <returns>An upward estimate of the count for <paramref name="obj"/>.</returns>
        public int Estimate(object obj)
        {
            var hashes = GetHashes(obj, _width, _depth);
            var min    = int.MaxValue;

            for (int i = 0; i < _depth; i++)
            {
                min = Math.Min(min, _count[i, hashes[i]]);
            }

            return min;
        }

        /// <summary>Removes all objects from the <see cref="CountMinSketch"/>.</summary>
        public void Clear()
        {
            _count = new int[_depth, _width];
        }

        #region Helpers

        // https://en.wikipedia.org/wiki/Double_hashing
        private int[] GetHashes(object obj, int maxValue, int count)
        {
            int hash1 = obj.GetHashCode();
            int hash2 = GetHash(obj);

            var array = new int[count];

            for (int i = 0; i < count; i++)
            {
                unchecked
                {
                    array[i] = Math.Abs(hash1 + hash2 * (i + 1)) % maxValue;
                }
            }

            return array;
        }

        private int GetHash(object obj)
        {
            int output;
            using (var stream = new MemoryStream(ObjectToStream(obj)))
            {
                output = _hash.Hash(stream);
            }

            return output;
        }

        private static byte[] ObjectToStream(object obj)
        {
            if (obj == null)
                return null;

            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        #endregion
    }
}
