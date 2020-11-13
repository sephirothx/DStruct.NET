# CountMinSketch

Represents a probabilistic data structure that serves as a memory efficient frequency table for objects in a stream of data.

<br>

## Constructors

`CountMinSketch(double epsilon, double delta)` Initializes a new instance of **CountMinSketch** in which the error of a query result is within a factor of *epsilon* with probability *1 - delta*.

`CountMinSketch(int width, int depth)` Initializes a new instance of **CountMinSketch** with the specified width and depth.

<br>

## Methods

`void Add(object obj)` Increments the count of the given object by one. Complexity: O(1)

`void Add(object obj, int count)` Increments the count of the given object by a specified amount. Complexity: O(1)

`int Estimate(object obj)` Estimates the count of an object in the **CountMinSketch**. The returned number is always equal to or greater than the actual count of the object. Complexity: O(1)

`void Clear()` Removes all objects from the **CountMinSketch**.
