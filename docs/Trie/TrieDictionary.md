# TrieDictionary

Implements: `IDictionary<string, T>` `IReadOnlyDictionary<string, T>`

Represents a dictionary in which the key/value coupling is implemented via a Trie.

<br>

## Constructors

`TrieDictionary()` Initializes a new instance of **TrieDictionary<T>** that is empty.

`TrieDictionary(IEnumerable<KeyValuePair<string, T>> collection)` Initializes a new instance of **TrieDictionary<T>** that contains every key/value pair from the input collection.

<br>

## Properties

`int Count` Gets the number of elements in the **TrieDictionary<T>**.

`ICollection<string> Keys` Gets an **ICollection** containing the keys of the **TrieDictionary<T>**.

`ICollection<T> Values` Gets an **ICollection<T>** containing the values of the **TrieDictionary<T>**.

`T this[string key]` Gets or sets the element with the specified key.

<br>

## Methods

`void Add(string key, T value)` Adds a key/value pair to the **TrieDictionary<T>**. Complexity: O(L)

`void Add(KeyValuePair<string, T> item)` Adds a key/value pair to the **TrieDictionary<T>**. Complexity: O(L)

`void AddAll(IEnumerable<KeyValuePair<string, T>> collection)` Adds a collection of key/value pairs to the **TrieDictionary<T>**. Complexity: O(N*L)

`void Clear()` Removes all elements from the **TrieDictionary<T>**.

`bool Contains(KeyValuePair<string, T> item)` Determines whether the **TrieDictionary<T>** contains a specific key/value pair. Complexity: O(L)

`bool ContainsAll(IEnumerable<KeyValuePair<string, T>> collection)` Determines whether the **TrieDictionary<T>** contains a specific collection of key/value pairs. Complexity: O(N*L)

`void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)` Copies the key/value pairs of the **TrieDictionary<T>** to an **Array**, starting at a particular **Array** index. Complexity: O(N*L)

`bool Remove(KeyValuePair<string, T> item)` Removes a specific key/value pair from the **TrieDictionary<T>**. Complexity: O(L)

`bool ContainsKey(string key)` Determines whether the **TrieDictionary<T>** contains a specific key. Complexity: O(L)

`KeyValuePair<string, T>[] GetWithPrefix(string prefix)` Gets the key/value pairs stored in the **TrieDictionary<T>** that are associated to keys that begin with the chosen prefix. Complexity: O(N*L)

`bool Remove(string key)` Removes a specific key from the **TrieDictionary<T>**. Complexity: O(L)

`bool TryGetValue(string key, out T value)` Gets the value associated with the specified key. Complexity: O(L)
