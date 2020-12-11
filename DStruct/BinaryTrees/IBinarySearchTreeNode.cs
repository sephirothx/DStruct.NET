namespace DStruct.BinaryTrees
{
    interface IBinarySearchTreeNode<out T>
    {
        T Value { get; }

        IBinarySearchTreeNode<T> Left  { get; }
        IBinarySearchTreeNode<T> Right { get; }

        int LeftChildren { get; }
    }
}