using System;

namespace DStruct.BinaryTrees
{
    public class AVLTreeNode<T> : IBinarySearchTreeNodeBase<T>
    {
        public AVLTreeNode<T> Left;
        public AVLTreeNode<T> Right;

        public T Value { get; set; }

        public int LeftChildren { get; set; }
        public int Height { get; set; }

        public int Balance
        {
            get
            {
                int leftHeight  = Left?.Height  ?? 0;
                int rightHeight = Right?.Height ?? 0;

                return rightHeight - leftHeight;
            }
        }

        IBinarySearchTreeNodeBase<T> IBinarySearchTreeNodeBase<T>.Left => Left;
        IBinarySearchTreeNodeBase<T> IBinarySearchTreeNodeBase<T>.Right => Right;

        public AVLTreeNode(T value, int height = 1, int leftChildren = 0)
        {
            Value        = value;
            Height       = height;
            LeftChildren = leftChildren;
        }

        public void UpdateHeight()
        {
            int leftHeight  = Left?.Height  ?? 0;
            int rightHeight = Right?.Height ?? 0;

            Height = Math.Max(leftHeight, rightHeight) + 1;
        }

        public AVLTreeNode<T> RotateLeft()
        {
            var newRoot = Right;

            Right        = newRoot.Left;
            newRoot.Left = this;

            UpdateHeight();
            newRoot.UpdateHeight();

            newRoot.LeftChildren += LeftChildren + 1;

            return newRoot;
        }

        public AVLTreeNode<T> RotateRight()
        {
            var newRoot = Left;

            Left          = newRoot.Right;
            newRoot.Right = this;

            UpdateHeight();
            newRoot.UpdateHeight();

            LeftChildren -= newRoot.LeftChildren + 1;

            return newRoot;
        }

        public AVLTreeNode<T> PerformRotations()
        {
            int balance = Balance;

            if (balance > 1)
            {
                if (Right.Balance < 0)
                {
                    Right = Right.RotateRight();
                }

                return RotateLeft();
            }

            if (balance < -1)
            {
                if (Left.Balance > 0)
                {
                    Left = Left.RotateLeft();
                }

                return RotateRight();
            }

            return this;
        }

        public static T RemoveInOrderSuccessor(ref AVLTreeNode<T> node)
        {
            if (node.Left != null)
            {
                node.LeftChildren--;
                T ret1 = RemoveInOrderSuccessor(ref node.Left);
                node.UpdateHeight();
                node = node.PerformRotations();
                return ret1;
            }

            T ret = node.Value;
            node = node.Right;
            return ret;
        }

        public static T RemoveInOrderPredecessor(ref AVLTreeNode<T> node)
        {
            if (node.Right != null)
            {
                T ret1 = RemoveInOrderPredecessor(ref node.Right);
                node.UpdateHeight();
                node = node.PerformRotations();
                return ret1;
            }

            T ret = node.Value;
            node = node.Left;
            return ret;
        }
    }
}
