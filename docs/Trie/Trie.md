# Trie

Implements: `ICollection<string>` `IReadOnlyCollection<string>`

Represents a collection of strings that is very efficient at retrieving all the stored words with a common prefix.

<br>

## Constructors

`Trie()` Initializes a new instance of **Trie** that is empty.

`Trie(IEnumerable<string> collection)` Initializes a new instance of **Trie** that contains every item from the input collection.

<br>

## Properties

`int Count` Gets the number of elements contained in the **Trie**. Complexity: O(1)

<br>

## Methods

`void Add(string value)` Adds an element to the **Trie**. Complexity: O(L)

`void Clear()` Removes all elements from the **Trie**.

`void AddAll(IEnumerable<string> collection)` Adds the elements of the specified collection to the **Trie**. Complexity: O(N*L)

`void CopyTo(string[] array, int arrayIndex)` Copies the elements of the **Trie** to an **Array**, starting at a particular **Array** index. Complexity: O(N*L)

`bool Remove(string value)` Removes a specific element from the **Trie**. Complexity: O(L)

`void RemoveAll(IEnumerable<string> collection)` Removes the elements of the specified collection from the **Trie**. Complexity: O(N*L)

`bool Contains(string value)` Determines whether the **Trie** contains a specific value. Complexity: O(L)

`bool ContainsAll(IEnumerable<string> collection)` Determines whether the **Trie** contains a specific collection of values. Complexity: O(N*L)

`bool ContainsPrefix(string prefix)` Determines whether the **Trie** contains a specific prefix. Complexity: O(L)

`string[] GetWithPrefix(string prefix)` Gets the strings stored in the **Trie** that begin with the chosen prefix. Complexity: O(N*L)

`string[] ToArray()` Returns an **Array** containing all the elements stored in the **Trie**.
