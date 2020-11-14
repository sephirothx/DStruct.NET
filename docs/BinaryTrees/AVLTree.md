# AVLTree

Implements: [`IBinarySearchTree<T>`](IBinarySearchTree.md)

Represents a node-based, self-balancing **IBinarySearchTree<T>** enhanced to implement an efficient indexer.

<br>

## Constructors

`AVLTree()` Initializes a new instance of **AVLTree<T>** that is empty.

`AVLTree(IEnumerable<T> collection)` Initializes a new instance of **AVLTree<T>** that contains every item from the input collection.

`AVLTree(IComparer<T> comparer)` Initializes a new instance of **AVLTree<T>** that is empty and uses the specified **IComparer<T>**.

<br>

## Properties

`int Count` Gets the number of elements stored in the **AVLTree<T>**. Complexity: O(1)

`T Min` Gets the minimum value element stored in the **AVLTree<T>**. Complexity: O(LogN)

`T Max` Gets the maximum value element stored in the **AVLTree<T>**. Complexity: O(LogN)

`T this[int index]` Gets the element at the specified index. Complexity: O(LogN)

<br>

## Methods

`int Insert(T value)` Inserts an element into the **AVLTree<T>** and returns its index. Complexity: O(LogN)

`bool Find(T value)` Determines whether the **AVLTree<T>** contains a specific value. Complexity: O(LogN)

`bool Remove(T value)` Removes one occurrence of a specific element from the **AVLTree<T>**. Complexity: O(LogN)

`T[] InOrderTraverse()` Returns the list of the elements stored in the **AVLTree<T>** in-order. Complexity: O(N)

`T[] PreOrderTraverse()` Returns the list of the elements stored in the **AVLTree<T>** pre-order. Complexity: O(N)

`T[] PostOrderTraverse()` Returns the list of the elements stored in the **AVLTree<T>** post-order. Complexity: O(N)
