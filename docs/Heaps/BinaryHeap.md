# BinaryHeap

*This is an abstract class*

Represents a tree in which the value of each node is greater ([**MaxHeap<T>**](MaxHeap.md)) or smaller ([**MinHeap<T>**](MinHeap.md)) than the value of all its children.

<br>

## Properties

`int Count` Gets the number of elements contained in the **BinaryHeap<T>**.

`bool IsEmpty` Determines whether the **BinaryHeap<T>** is empty.

<br>

## Methods

`void Push(T value)` Inserts a new element into the **BinaryHeap<T>**. Complexity: O(LogN)

`T Pop()` Removes the element at the root of the **BinaryHeap<T>** and returns its value, maintaining the **BinaryHeap<T>** properties. Complexity: O(LogN)

`T Peek()` Returns the value of the root of the **BinaryHeap<T>** without removing it. Complexity: O(1)
