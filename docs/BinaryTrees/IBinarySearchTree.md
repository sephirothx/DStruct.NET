# IBinarySearchTree

*This is an interface*

Represents a Binary Tree in which the elements are sorted in order so that operations can use the principle of Binary Search.

<br>

## Properties

`int Count` Gets the number of elements stored in the **IBinarySearchTree<T>**.

`T Min` Gets the minimum value element stored in the **IBinarySearchTree<T>**.

`T Max` Gets the maximum value element stored in the **IBinarySearchTree<T>**.

`T this[int index]` Gets the element at the specified index.

<br>

## Methods

`int Insert(T value)` Inserts an element into the **IBinarySearchTree<T>** and returns its index.

`bool Find(T value)` Determines whether the **IBinarySearchTree<T>** contains a specific value.

`bool Remove(T value)` Removes one occurrence of a specific element from the **IBinarySearchTree<T>**.

`T[] InOrderTraverse()` Returns the list of the elements stored in the **IBinarySearchTree<T>** in-order.

`T[] PreOrderTraverse()` Returns the list of the elements stored in the **IBinarySearchTree<T>** pre-order.

`T[] PostOrderTraverse()` Returns the list of the elements stored in the **IBinarySearchTree<T>** post-order.
