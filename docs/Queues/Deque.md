# Deque

Implements: `IEnumerable<T>`

Represents a double ended queue for which insertion and retrieval of elements close to either of the ends is very efficient.

<br>

## Constructors

`Deque()` Initializes a new instance of **Deque<T>** that is empty and has the default initial capacity.

`Deque(int capacity)` Initializes a new instance of **Deque<T>** that is empty and has the specified initial capacity.

`Deque(IEnumerable<T> collection)` Initializes a new instance of **Deque<T>** that contains every element from the input collection and has enough capacity to accomodate the number of elements copied.

<br>

## Properties

`int Count` Gets the number of elements contained in the **Deque<T>**.

`int Capacity` Gets the maximum number of elements the internal data structure can hold without resizing.

`T Back` Returns the first element at the rear of the **Deque<T>** without removing it. Complexity: O(1)

`T Front` Returns the first element at the front of the **Deque<T>** without removing it. Complexity: O(1)

`T this[int index]` Gets or sets the element at the specified position of the **Deque<T>**. Complexity: O(1)

<br>

## Methods

`void PushBack(T item)` Inserts a new element to the rear of the **Deque<T>**. Complexity: O(1)

`void PushFront(T item)` Inserts a new element to the front of the **Deque<T>**. Complexity: O(1)

`T PopBack()` Removes the element at the rear of the **Deque<T>** and returns its value. Complexity: O(1)

`T PopFront()` Removes the element at the front of the **Deque<T>** and returns its value. Complexity: O(1)

`void Clear()` Removes all elements from the **Deque<T>**. Complexity: O(1)

`bool Contains(T item)` Determines whether the **Deque<T>** contains a specific element. Complexity: O(N)

`void CopyTo(T[] array, int arrayIndex)` Copies the elements of the **Deque<T>** to an **Array**, starting at a particular **Array** index. Complexity: O(N)

`bool Remove(T item)` Removes the first occurrence of a specific element from the **Deque<T>**. Complexity: O(N)

`int IndexOf(T item)` Searches for the specified element and returns the zero-based index of the first occurrence within the entire **Deque<T>**. Complexity: O(N)

`void Insert(int index, T item)` Inserts an element into the **Deque<T>** at the specified index. Complexity: O(N)

`void RemoveAt(int index)` Removes the element at the specified index of the **Deque<T>**. Complexity: O(N)
