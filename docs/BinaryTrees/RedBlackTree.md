# RedBlackTree

Implements: [`IBinarySearchTree<T>`](IBinarySearchTree.md)

Represents a node-based, self-balancing **IBinarySearchTree<T>**.

<br>

## Constructors

`RedBlackTree()` Initializes a new instance of **RedBlackTree<T>** that is empty.

`RedBlackTree(IEnumerable<T> collection)` Initializes a new instance of **RedBlackTree<T>** that contains every item from the input collection.

`RedBlackTree(IComparer<T> comparer)` Initializes a new instance of **RedBlackTree<T>** that is empty and uses the specified **IComparer<T>**.

<br>

## Properties

`int Count` Gets the number of elements stored in the **RedBlackTree<T>**. Complexity: O(1)

`T Min` Gets the minimum value element stored in the **RedBlackTree<T>**. Complexity: O(LogN)

`T Max` Gets the maximum value element stored in the **RedBlackTree<T>**. Complexity: O(LogN)

`T this[int index]` Gets the element at the specified index. Complexity: O(LogN)

<br>

## Methods

`int Insert(T value)` Inserts an element into the **RedBlackTree<T>** and returns its index. Complexity: O(LogN)

`bool Find(T value)` Determines whether the **RedBlackTree<T>** contains a specific value. Complexity: O(LogN)

`bool Remove(T value)` Removes one occurrence of a specific element from the **RedBlackTree<T>**. Complexity: O(LogN)

`T[] InOrderTraverse()` Returns the list of the elements stored in the **RedBlackTree<T>** in-order. Complexity: O(N)

`T[] PreOrderTraverse()` Returns the list of the elements stored in the **RedBlackTree<T>** pre-order. Complexity: O(N)

`T[] PostOrderTraverse()` Returns the list of the elements stored in the **RedBlackTree<T>** post-order. Complexity: O(N)
