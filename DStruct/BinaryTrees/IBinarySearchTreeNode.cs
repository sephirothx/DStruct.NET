using System;
using System.Collections.Generic;
using System.Text;

namespace DStruct.BinaryTrees
{
    internal interface IBinarySearchTreeNode<TValue>
    {
        IBinarySearchTreeNode<TValue> Left { get; }
        IBinarySearchTreeNode<TValue> Right { get; }

        TValue Value { get; }
        int LeftChildren { get; }
    }
}
