using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DStruct.Tries
{
    /// <summary>Represents a collection of strings that is very efficient at retrieving all the stored words with a common prefix.</summary>
    public class Trie : ICollection<string>, IReadOnlyCollection<string>
    {
        private readonly TrieNode _root = new TrieNode();

        /// <summary>Gets the number of elements contained in the <see cref="Trie" />. <code>Complexity: O(1)</code></summary>
        public int Count { get; private set; }

        /// <summary>Gets a value indicating whether the <see cref="Trie" /> is read-only.</summary>
        /// <returns><c>true</c> if the <see cref="Trie" /> is read-only; <c>false</c> otherwise.</returns>
        public bool IsReadOnly { get; } = false;

        /// <summary>Initializes a new instance of <see cref="Trie"/> that is empty.</summary>
        public Trie()
        {
        }

        /// <summary>Initializes a new instance of <see cref="Trie"/> that contains every item from the input collection.</summary>
        /// <param name="collection">The collection of elements to add to the <see cref="Trie"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public Trie(IEnumerable<string> collection)
        {
            AddAll(collection);
        }

        /// <summary>Adds an element to the <see cref="Trie" />. <code>Complexity: O(L)</code></summary>
        /// <param name="value">The element to add to the <see cref="Trie" />.</param>
        /// <exception cref="ArgumentNullException">Input <paramref name="value"/> is <c>null</c>.</exception>
        public void Add(string value)
        {
            if (_root.Add(value))
            {
                Count++;
            }
        }

        /// <summary>Removes all elements from the <see cref="Trie"/>.</summary>
        public void Clear()
        {
            _root.Clear();
            Count = 0;
        }

        /// <summary>Adds the elements of the specified collection to the <see cref="Trie"/>. <code>Complexity: O(N*L)</code></summary>
        /// <param name="collection">The collection of elements to add to the <see cref="Trie"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public void AddAll(IEnumerable<string> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (string s in collection)
            {
                if (_root.Add(s))
                {
                    Count++;
                }
            }
        }

        /// <summary>Copies the elements of the <see cref="Trie"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index. <code>Complexity: O(N*L)</code></summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="Trie"/>.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is out of the bounds of <paramref name="array"/>.</exception>
        /// <exception cref="ArgumentException">The number of elements in the source <see cref="Trie"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
        public void CopyTo(string[] array, int arrayIndex)
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

            Array.Copy(ToArray(), 0, array, arrayIndex, Count);
        }

        /// <summary>Removes a specific element from the <see cref="Trie"/>. <code>Complexity: O(L)</code></summary>
        /// <param name="value">The element to remove from the <see cref="Trie"/>.</param>
        /// <returns><c>true</c> if the element was successfully removed from the <see cref="Trie"/>; <c>false</c> otherwise.</returns>
        public bool Remove(string value)
        {
            bool ret = _root.Remove(value);

            if (ret)
            {
                Count--;
            }

            return ret;
        }

        /// <summary>Removes the elements of the specified collection from the <see cref="Trie"/>. <code>Complexity: O(N*L)</code></summary>
        /// <param name="collection">The collection of elements to remove from the <see cref="Trie"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public void RemoveAll(IEnumerable<string> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (string s in collection)
            {
                if (_root.Remove(s))
                {
                    Count--;
                }
            }
        }

        /// <summary>Determines whether the <see cref="Trie"/> contains a specific value. <code>Complexity: O(L)</code></summary>
        /// <param name="value">The element to locate in the <see cref="Trie"/>.</param>
        /// <returns><c>true</c> if the <see cref="Trie"/> contains <paramref name="value"/>; <c>false</c> otherwise.</returns>
        public bool Contains(string value)
        {
            return _root.Contains(value);
        }

        /// <summary>Determines whether the <see cref="Trie"/> contains a specific collection of values. <code>Complexity: O(N*L)</code></summary>
        /// <param name="collection">The collection of elements to locate in the <see cref="Trie"/>.</param>
        /// <returns><c>true</c> if the <see cref="Trie"/> contains all the elements of <paramref name="collection"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        public bool ContainsAll(IEnumerable<string> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.All(Contains);
        }

        /// <summary>Determines whether the <see cref="Trie"/> contains a specific prefix. <code>Complexity: O(L)</code></summary>
        /// <param name="prefix">The prefix to locate in the <see cref="Trie"/>.</param>
        /// <returns><c>true</c> if the <see cref="Trie"/> contains <paramref name="prefix"/>; <c>false</c> otherwise.</returns>
        public bool ContainsPrefix(string prefix)
        {
            return _root.Contains(prefix, true);
        }

        /// <summary>Gets the strings stored in the <see cref="Trie"/> that begin with the chosen prefix. <code>Complexity: O(N*L)</code></summary>
        /// <param name="prefix">The prefix of the strings to retrieve.</param>
        /// <returns><see cref="Array"/> containing all the strings that start with <paramref name="prefix"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="prefix"/> is <c>null</c>.</exception>
        public string[] GetWithPrefix(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return _root.GetWithPrefix(prefix).ToArray();
        }

        /// <summary>Returns an <see cref="Array"/> containing all the elements stored in the <see cref="Trie"/>.</summary>
        /// <returns>Array of the elements in the <see cref="Trie"/>.</returns>
        public string[] ToArray()
        {
            return _root.GetAllValues().ToArray();
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="Trie"/>.</summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="Trie"/>.</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return _root.GetAllValues().GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="Trie"/>.</summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the <see cref="Trie"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
