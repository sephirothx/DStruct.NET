# SparseMatrix

Implements: `IEnumerable<T>`

Provides a memory efficient implementation of a matrix in which most of the elements have the default value.

<br>

## Constructors

`SparseMatrix()` Initializes a new instance of **SparseMatrix<T>**.

`SparseMatrix(T defaultValue)` Initializes a new instance of **SparseMatrix<T>** with the specified default element value.

<br>

## Properties

`int Count` Gets the number of elements with value other than the default one.

`int MinX` Gets the minimum X-index of the **SparseMatrix<T>**.

`int MinY` Gets the minimum Y-index of the **SparseMatrix<T>**.

`int MaxX` Gets the maximum X-index of the **SparseMatrix<T>**.

`int MaxY` Gets the maximum Y-index of the **SparseMatrix<T>**.

`T this[int index0, int index1]` Gets or sets the value of the element at the specified coordinate of the **SparseMatrix<T>**. Complexity: O(1)
