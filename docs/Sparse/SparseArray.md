# SparseArray

Implements: `IEnumerable<T>`

Provides a memory efficient implementation of an array in which most of the elements have the default value.

<br>

## Constructors

`SparseArray()` Initializes a new instance of **SparseArray<T>** with no specified length.

`SparseArray(int length)` Initializes a new instance of **SparseArray<T>** with the fixed specified length.

`SparseArray(T defaultValue)` Initializes a new instance of **SparseArray<T>** with the specified default element value.

`SparseArray(int length, T defaultValue)` Initializes a new instance of **SparseArray<T>** with the fixed specified length and default element value.

<br>

## Properties

`int Count` Gets the number of elements with value other than the default one.

`int Length` Gets the total length of the **SparseArray<T>**.

`ICollection<int> Indices` Gets an **ICollection<int>** containing the indices of the elements with value other than the default one.

`ICollection<T> Values` Gets an **ICollection<T>** containing the elements with value other than the default one.

`T this[int index]` Gets or sets the value of the element at the specified index of the **SparseArray<T>**. Complexity: O(1)
