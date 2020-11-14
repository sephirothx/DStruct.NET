# PriorityQueue

Implements: `IEnumerable<T>`

Represents a Queue in which the elements are ordered using a specified priority.

<br>

## Constructors

`PriorityQueue()` Initializes a new instance of **PriorityQueue<T>** which is empty and ordered by ascending priority value (higher priority first).

`PriorityQueue(PriorityQueueOrder order)` Initializes a new instance of **PriorityQueue<T>** which is empty and ordered by ascending or descending priority value.

`PriorityQueue(int capacity, PriorityQueueOrder order)` Initializes a new instance of **PriorityQueue<T>** which is ordered by ascending or descending priority value and has the specified initial capacity.

<br>

## Properties

`int Count` Gets the number of elements contained in the **PriorityQueue<T>**.

`bool IsEmpty` Determines whether the **PriorityQueue<T>** is empty.

<br>

## Methods

`void Push(T value, int priority)` Inserts a new item into the **PriorityQueue<T>**, in a position relative to the given priority. Complexity: O(LogN)

`PriorityQueueNode<T> Pop()` Removes the element with the highest (or lowest) priority from the **PriorityQueue<T>** and returns its value. Complexity: O(LogN)

`PriorityQueueNode<T> Peek()` Returns the **PriorityQueueNode<T>** with the highest (or lowest) priority in the **PriorityQueue<T>** wihout removing it. Complexity: O(1)
