using System;
using System.Collections.Generic;
using System.Text;

namespace DStruct.BinaryTrees
{
    public interface IBinarySearchTreeNodeBase<TValue>
    {
        IBinarySearchTreeNodeBase<TValue> Left { get; }
        IBinarySearchTreeNodeBase<TValue> Right { get; }

        TValue Value { get; }
    }
}
