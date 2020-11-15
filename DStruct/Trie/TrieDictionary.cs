using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DStruct.Trie
{
    /// <summary>Represents a dictionary in which the key/value coupling is implemented via a Trie.</summary>
    /// <typeparam name="T">The type of values stored in the <see cref="TrieDictionary{T}"/>. It must implement the <see cref="IEquatable{T}"/> interface.</typeparam>
    public class TrieDictionary<T> : IDictionary<string, T>, IReadOnlyDictionary<string, T>
        where T : IEquatable<T>
    {
        private readonly TrieNode<T> _root = new TrieNode<T>();

        /// <summary>Gets the number of elements in the <see cref="TrieDictionary{T}"/>.</summary>
        public int Count { get; private set; }

        /// <summary>Gets a value indicating whether the <see cref="TrieDictionary{T}" /> is read-only.</summary>
        /// <returns><c>true</c> if the <see cref="TrieDictionary{T}" /> is read-only; <c>false</c> otherwise.</returns>
        public bool IsReadOnly { get; } = false;

        /// <summary>Gets an <see cref="ICollection" /> containing the keys of the <see cref="TrieDictionary{T}" />.</summary>
        public ICollection<string> Keys => _root.GetAllKeys().ToArray();

        /// <summary>Gets an <see cref="IEnumerable{T}"/> that contains the keys in the read-only dictionary.</summary>
        IEnumerable<string> IReadOnlyDictionary<string, T>.Keys => Keys;

        /// <summary>Gets an <see cref="ICollection{T}" /> containing the values of the <see cref="TrieDictionary{T}" />.</summary>
        public ICollection<T> Values => _root.GetAllValues().ToArray();

        /// <summary>Gets an <see cref="IEnumerable{T}"/> that contains the values in the read-only dictionary.</summary>
        IEnumerable<T> IReadOnlyDictionary<string, T>.Values => Values;

        /// <summary>Initializes a new instance of <see cref="TrieDictionary{T}"/> that is empty.</summary>
        public TrieDictionary()
        {
        }

        /// <summary>Initializes a new instance of <see cref="TrieDictionary{T}"/> that contains every key/value pair from the input collection.</summary>
        /// <param name="collection">The collection of key/value pairs to add to the <see cref="TrieDictionary{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public TrieDictionary(IEnumerable<KeyValuePair<string, T>> collection)
        {
            AddAll(collection);
        }

        /// <summary>Adds a key/value pair to the <see cref="TrieDictionary{T}" />. <code>Complexity: O(L)</code></summary>
        /// <param name="item">The key/value pair to add to the <see cref="TrieDictionary{T}" />.</param>
        public void Add(KeyValuePair<string, T> item)
        {
            if (_root.Add(item.Key, item.Value))
            {
                Count++;
            }
        }

        /// <summary>Adds a collection of key/value pairs to the <see cref="TrieDictionary{T}"/>. <code>Complexity: O(N*L)</code></summary>
        /// <param name="collection">The collection of key/value pairs to add to the <see cref="TrieDictionary{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public void AddAll(IEnumerable<KeyValuePair<string, T>> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var pair in collection)
            {
                Add(pair);
            }
        }

        /// <summary>Removes all elements from the <see cref="TrieDictionary{T}"/>.</summary>
        public void Clear()
        {
            _root.Clear();
            Count = 0;
        }

        /// <summary>Determines whether the <see cref="TrieDictionary{T}"/> contains a specific key/value pair. <code>Complexity: O(L)</code></summary>
        /// <param name="item">The key/value pair to locate in the <see cref="TrieDictionary{T}"/>.</param>
        /// <returns><c>true</c> if the <see cref="TrieDictionary{T}"/> contains <paramref name="item"/>; <c>false</c> otherwise.</returns>
        public bool Contains(KeyValuePair<string, T> item)
        {
            return _root.ContainsPair(item.Key, item.Value);
        }

        /// <summary>Determines whether the <see cref="TrieDictionary{T}"/> contains a specific collection of key/value pairs. <code>Complexity: O(N*L)</code></summary>
        /// <param name="collection">The collection of key/value pairs to locate in the <see cref="TrieDictionary{T}"/>.</param>
        /// <returns><c>true</c> if the <see cref="TrieDictionary{T}"/> contains all the key/value pairs of <paramref name="collection"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public bool ContainsAll(IEnumerable<KeyValuePair<string, T>> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.All(Contains);
        }

        /// <summary>Copies the key/value pairs of the <see cref="TrieDictionary{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index. <code>Complexity: O(N*L)</code></summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the key/value pairs copied from <see cref="TrieDictionary{T}"/>.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is out of the bounds of <paramref name="array"/>.</exception>
        /// <exception cref="ArgumentException">The number of key/value pairs in the source <see cref="TrieDictionary{T}"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
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

            var pairs = _root.GetAllPairs().ToArray();
            Array.Copy(pairs, 0, array, arrayIndex, Count);
        }

        /// <summary>Removes a specific key/value pair from the <see cref="TrieDictionary{T}"/>. <code>Complexity: O(L)</code></summary>
        /// <param name="item">The key/value pair to remove from the <see cref="TrieDictionary{T}"/>.</param>
        /// <returns><c>true</c> if the key/value pair was successfully removed from the <see cref="TrieDictionary{T}"/>; <c>false</c> otherwise.</returns>
        public bool Remove(KeyValuePair<string, T> item)
        {
            var ret = _root.Remove(item.Key, item.Value);

            if (ret)
            {
                Count--;
            }

            return ret;
        }

        /// <summary>Determines whether the <see cref="TrieDictionary{T}"/> contains a specific key. <code>Complexity: O(L)</code></summary>
        /// <param name="key">The key to locate in the <see cref="TrieDictionary{T}"/>.</param>
        /// <returns><c>true</c> if the <see cref="TrieDictionary{T}"/> contains <paramref name="key"/>; <c>false</c> otherwise.</returns>
        public bool ContainsKey(string key)
        {
            return _root.Contains(key);
        }

        /// <summary>Adds a key/value pair to the <see cref="TrieDictionary{T}" />. <code>Complexity: O(L)</code></summary>
        /// <param name="key">The string to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public void Add(string key, T value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (_root.Add(key, value))
            {
                Count++;
            }
        }

        /// <summary>Gets the key/value pairs stored in the <see cref="TrieDictionary{T}"/> that are associated to keys that begin with the chosen prefix. <code>Complexity: O(N*L)</code></summary>
        /// <param name="prefix">The prefix of the keys to retrieve.</param>
        /// <returns><see cref="Array"/> containing all the key/value pairs associated to keys that begin with <paramref name="prefix"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="prefix"/> is <c>null</c>.</exception>
        public KeyValuePair<string, T>[] GetWithPrefix(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return _root.GetPairsWithPrefix(prefix).ToArray();
        }

        /// <summary>Removes a specific key from the <see cref="TrieDictionary{T}"/>. <code>Complexity: O(L)</code></summary>
        /// <param name="key">The key to remove from the <see cref="TrieDictionary{T}"/>.</param>
        /// <returns><c>true</c> if the key was successfully removed from the <see cref="TrieDictionary{T}"/>; <c>false</c> otherwise.</returns>
        public bool Remove(string key)
        {
            var ret = _root.Remove(key);

            if (ret)
            {
                Count--;
            }

            return ret;
        }

        /// <summary>Gets the value associated with the specified key. <code>Complexity: O(L)</code></summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.</param>
        /// <returns><c>true</c> if the <see cref="TrieDictionary{T}"/> contains an element with the specified key; <c>false</c> otherwise.</returns>
        public bool TryGetValue(string key, out T value)
        {
            return _root.TryGetValue(key, out value);
        }

        /// <summary>Gets or sets the element with the specified key.</summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns>The value associated to the specified key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        /// <exception cref="KeyNotFoundException">The <see cref="TrieDictionary{T}"/> does not contain <paramref name="key"/></exception>
        public T this[string key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (!_root.TryGetValue(key, out var result))
                {
                    throw new KeyNotFoundException($"The key \"{key}\" is not present in the TrieDictionary.");
                }

                return result;
            }

            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (_root.Add(key, value))
                {
                    Count++;
                }
            }
        }

        /// <summary>Returns an enumerator that iterates through the key/value pairs of the <see cref="TrieDictionary{T}"/>.</summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="TrieDictionary{T}"/>.</returns>
        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return _root.GetAllPairs().GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="TrieDictionary{T}"/>.</summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the <see cref="TrieDictionary{T}"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
