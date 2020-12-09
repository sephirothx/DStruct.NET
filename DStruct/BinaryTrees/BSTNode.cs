﻿namespace DStruct.BinaryTrees
{
    public class BSTNode<T> : IBinarySearchTreeNodeBase<T>
    {
        public BSTNode<T> Left;
        public BSTNode<T> Right;

        public T Value { get; set; }
        public int LeftChildren { get; set; }

        IBinarySearchTreeNodeBase<T> IBinarySearchTreeNodeBase<T>.Left => Left;
        IBinarySearchTreeNodeBase<T> IBinarySearchTreeNodeBase<T>.Right => Right;

        public BSTNode()
        {
        }

        public BSTNode(T value, int leftChildren = 0)
        {
            Value        = value;
            LeftChildren = leftChildren;
        }

        public static T RemoveInOrderSuccessor(ref BSTNode<T> node)
        {
            if (node.Left != null)
            {
                node.LeftChildren--;
                return RemoveInOrderSuccessor(ref node.Left);
            }

            T ret = node.Value;
            node = node.Right;
            return ret;
        }

        public static T RemoveInOrderPredecessor(ref BSTNode<T> node)
        {
            if (node.Right != null)
                return RemoveInOrderPredecessor(ref node.Right);

            T ret = node.Value;
            node = node.Left;
            return ret;
        }
    }
}
