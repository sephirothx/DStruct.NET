# BinarySearchTree

Implements: [`IBinarySearchTree<T>`](IBinarySearchTree.md)

Represents a node-based, non self-balancing **IBinarySearchTree<T>** enhanced to implement an efficient indexer.

<br>

## Constructors

`BinarySearchTree()` Initializes a new instance of **BinarySearchTree<T>** that is empty.

`BinarySearchTree(IEnumerable<T> collection)` Initializes a new instance of **BinarySearchTree<T>** that contains every item from the input collection.

`BinarySearchTree(IComparer<T> comparer)` Initializes a new instance of **BinarySearchTree<T>** that is empty and uses the specified **IComparer<T>**.

<br>

## Properties

`int Count` Gets the number of elements stored in the **BinarySearchTree<T>**. Complexity: O(1)

`T Min` Gets the minimum value element stored in the **BinarySearchTree<T>**. Complexity: avg O(LogN), worst O(N)

`T Max` Gets the maximum value element stored in the **BinarySearchTree<T>**. Complexity: avg O(LogN), worst O(N)

`T this[int index]` Gets the element at the specified index. Complexity: avg O(LogN), worst O(N)

<br>

## Methods

`int Insert(T value)` Inserts an element into the **BinarySearchTree<T>** and returns its index. Complexity: avg O(LogN), worst O(N)

`bool Find(T value)` Determines whether the **BinarySearchTree<T>** contains a specific value. Complexity: avg O(LogN), worst O(N)

`bool Remove(T value)` Removes one occurrence of a specific element from the **BinarySearchTree<T>**. Complexity: avg O(LogN), worst O(N)

`T[] InOrderTraverse()` Returns the list of the elements stored in the **BinarySearchTree<T>** in-order. Complexity: O(N)

`T[] PreOrderTraverse()` Returns the list of the elements stored in the **BinarySearchTree<T>** pre-order. Complexity: O(N)

`T[] PostOrderTraverse()` Returns the list of the elements stored in the **BinarySearchTree<T>** post-order. Complexity: O(N)
