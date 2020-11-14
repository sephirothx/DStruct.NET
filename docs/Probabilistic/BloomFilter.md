# BloomFilter

Represents a probabilistic data structure that is optimal to determine if an object isn't or may be present in a set.

<br>

## Constructors

`BloomFilter(int n, double p)` Initialized a new instance of **BloomFilter** with the specified desired capacity and false-positive probability.

`BloomFilter(int m, int k)` Initialized a new instance of **BloomFilter** with the specified width and depth.

<br>

## Methods

`void Add(object obj)` Adds an object to the **BloomFilter**. Complexity: O(1)

`bool Contains(object obj)` Determines if the specified object isn't or may be in the **BloomFilter**. Complexity: O(1)

`void Clear()` Removes all objects from the **BloomFilter**.
