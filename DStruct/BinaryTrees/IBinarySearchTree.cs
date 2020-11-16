using System;

namespace DStruct.BinaryTrees
{
    /// <summary>Represents a Binary Tree in which the elements are sorted in order so that operations can use the principle of Binary Search.</summary>
    /// <typeparam name="T">The type of the values stored in the tree.</typeparam>
    public interface IBinarySearchTree<T>
    {
        /// <summary>Gets the number of elements stored in the <see cref="IBinarySearchTree{T}"/>.</summary>
        int Count { get; }

        /// <summary>Gets the minimum value element stored in the <see cref="IBinarySearchTree{T}"/>.</summary>
        T Min { get; }

        /// <summary>Gets the maximum value element stored in the <see cref="IBinarySearchTree{T}"/>.</summary>
        T Max { get; }

        /// <summary>Gets the element at the specified index.</summary>
        /// <param name="index">The index of the element to get from the <see cref="IBinarySearchTree{T}"/>.</param>
        /// <returns>The element at the specified index.</returns>
        T this[int index] { get; }

        /// <summary>Inserts an element into the <see cref="IBinarySearchTree{T}"/> and returns its index.</summary>
        /// <param name="value">The element to add to the <see cref="IBinarySearchTree{T}"/>.</param>
        /// <returns>The index at which the element was placed.</returns>
        int Insert(T value);

        /// <summary>Determines whether the <see cref="IBinarySearchTree{T}"/> contains a specific value.</summary>
        /// <param name="value">The element to locate in the <see cref="IBinarySearchTree{T}"/>.</param>
        /// <returns><c>true</c> if the <see cref="IBinarySearchTree{T}"/> contains <paramref name="value"/>; <c>false</c> otherwise.</returns>
        bool Find(T value);

        /// <summary>Removes one occurrence of a specific element from the <see cref="IBinarySearchTree{T}"/>.</summary>
        /// <param name="value">The element to remove from the <see cref="IBinarySearchTree{T}"/>.</param>
        /// <returns><c>true</c> if the element was successfully removed from the <see cref="IBinarySearchTree{T}"/>; <c>false</c> otherwise.</returns>
        bool Remove(T value);

        /// <summary>Returns the list of the elements stored in the <see cref="IBinarySearchTree{T}"/> in-order.</summary>
        /// <returns>List of in-order elements.</returns>
        T[] InOrderTraverse();

        /// <summary>Returns the list of the elements stored in the <see cref="IBinarySearchTree{T}"/> pre-order.</summary>
        /// <returns>List of pre-order elements.</returns>
        T[] PreOrderTraverse();
        
        /// <summary>Returns the list of the elements stored in the <see cref="IBinarySearchTree{T}"/> post-order.</summary>
        /// <returns>List of post-order elements.</returns>
        T[] PostOrderTraverse();
    }
}
