namespace DStruct.BinaryTrees
{
    internal class RedBlackTreeNode<T> : IBinarySearchTreeNode<T>
    {
        public RedBlackTreeNode<T> Left;
        public RedBlackTreeNode<T> Right;
        public RedBlackTreeNode<T> Parent;

        public T Value;

        // The most significant bit represents the color:
        // 1: red
        // 0: black
        // The remaining 31 bits represent the left children.
        // Since the number of left children is always non negative, this cannot cause loss of information.
        private int _info;

        public bool IsRed => (_info >> 31) != 0;

        public int LeftChildren
        {
            get => _info & ~(1 << 31);
            set => _info = (_info & 1 << 31) | value;
        }

        public bool IsRoot => Parent == null;

        public bool IsLeftChild => Parent?.Left == this;
        public bool IsRightChild => Parent?.Right == this;
        public bool HasTwoRedChildren => (Left?.IsRed ?? false) && (Right?.IsRed ?? false);
        public bool HasARedChild => (Left?.IsRed ?? false) || (Right?.IsRed ?? false);
        public RedBlackTreeNode<T> OuterChild => IsLeftChild ? Left : Right;
        public RedBlackTreeNode<T> InnerChild => IsLeftChild ? Right : Left;
        public RedBlackTreeNode<T> Sibling => IsLeftChild ? Parent?.Right : Parent?.Left;
        public RedBlackTreeNode<T> GrandParent => Parent?.Parent;

        IBinarySearchTreeNode<T> IBinarySearchTreeNode<T>.Left => Left;
        IBinarySearchTreeNode<T> IBinarySearchTreeNode<T>.Right => Right;

        T IBinarySearchTreeNode<T>.Value => Value;

        public RedBlackTreeNode(T value, RedBlackTreeNode<T> parent = null, bool isRed = true, int leftChildren = 0)
        {
            Value        = value;
            Parent       = parent;
            LeftChildren = leftChildren;

            if (isRed)
            {
                _info |= 1 << 31;
            }
        }

        public RedBlackTreeNode<T> RotateLeft()
        {
            var tmp = Right;

            Right      = tmp.Left;
            tmp.Left   = this;
            tmp.Parent = Parent;
            Parent     = tmp;

            if (Right != null)
            {
                Right.Parent = this;
            }

            tmp.LeftChildren += LeftChildren + 1;

            return tmp;
        }

        public void RotateLeft2()
        {
            var tmp = Right;

            if (IsLeftChild)
            {
                Parent.Left = tmp;
            }
            else if (Parent != null)
            {
                Parent.Right = tmp;
            }

            Right      = tmp.Left;
            tmp.Left   = this;
            tmp.Parent = Parent;
            Parent     = tmp;

            if (Right != null)
            {
                Right.Parent = this;
            }

            tmp.LeftChildren += LeftChildren + 1;
        }

        public RedBlackTreeNode<T> RotateRight()
        {
            var tmp = Left;

            Left       = tmp.Right;
            tmp.Right  = this;
            tmp.Parent = Parent;
            Parent     = tmp;

            if (Left != null)
            {
                Left.Parent = this;
            }

            LeftChildren -= tmp.LeftChildren + 1;

            return tmp;
        }

        public void RotateRight2()
        {
            var tmp = Left;

            if (IsLeftChild)
            {
                Parent.Left = tmp;
            }
            else if (Parent != null)
            {
                Parent.Right = tmp;
            }

            Left       = tmp.Right;
            tmp.Right  = this;
            tmp.Parent = Parent;
            Parent     = tmp;

            if (Left != null)
            {
                Left.Parent = this;
            }

            LeftChildren -= tmp.LeftChildren + 1;
        }

        public RedBlackTreeNode<T> PerformRotation()
        {
            RedBlackTreeNode<T> ret;

            if (Left?.IsRed ?? false)
            {
                if (Left.Right?.IsRed ?? false)
                {
                    Left = Left.RotateLeft();
                }

                Recolor();
                Left.Recolor();
                ret = RotateRight();
            }
            else
            {
                if (Right.Left?.IsRed ?? false)
                {
                    Right = Right.RotateRight();
                }

                Recolor();
                Right.Recolor();
                ret = RotateLeft();
            }

            return ret;
        }

        public static T RemoveInOrderSuccessor(RedBlackTreeNode<T> node, ref RedBlackTreeNode<T> newRoot)
        {
            if (node.Left != null)
            {
                node.LeftChildren--;
                T ret1 = RemoveInOrderSuccessor(node.Left, ref newRoot);

                return ret1;
            }

            T ret = node.Value;
            node.Delete(ref newRoot);
            return ret;
        }

        public static T RemoveInOrderPredecessor(RedBlackTreeNode<T> node, ref RedBlackTreeNode<T> newRoot)
        {
            if (node.Right != null)
            {
                T ret1 = RemoveInOrderPredecessor(node.Right, ref newRoot);

                return ret1;
            }

            T ret = node.Value;
            node.Delete(ref newRoot);
            return ret;
        }

        public void Recolor()
        {
            _info ^= 1 << 31;
        }

        public void RecolorNodeAndChildren()
        {
            Recolor();
            Left.Recolor();
            Right.Recolor();
        }

        public void ColorBlack()
        {
            _info &= ~(1 << 31);
        }

        public void Delete(ref RedBlackTreeNode<T> newRoot)
        {
            RedBlackTreeNode<T> ret = null;

            if (IsRoot        &&
                Left  == null &&
                Right == null)
            {
                newRoot = null;
                return;
            }

            if (!(IsRed || HasARedChild))
            {
                ret = FixDoubleBlack();
            }

            var child = Left ?? Right;

            if (IsRoot)
            {
                ret = child;
            }

            child?.Substitute();

            if (IsLeftChild)
            {
                Parent.Left = child;
            }
            else if (IsRightChild)
            {
                Parent.Right = child;
            }

            if (ret != null)
            {
                newRoot = ret;
            }
        }

        public RedBlackTreeNode<T> FixDoubleBlack()
        {
            if (IsRoot)
            {
                return this;
            }

            RedBlackTreeNode<T> ret = null;

            var sibling = Sibling;
            var parent  = Parent;

            if (sibling.IsRed)
            {
                if (parent.IsRoot)
                {
                    ret = sibling;
                }

                sibling.Recolor();
                parent.Recolor();
                if (sibling.IsLeftChild)
                {
                    parent.RotateRight2();
                }
                else
                {
                    parent.RotateLeft2();
                }

                FixDoubleBlack();
            }
            else if (sibling.OuterChild?.IsRed ?? false)
            {
                if (parent.IsRoot)
                {
                    ret = sibling;
                }

                sibling.OuterChild.ColorBlack();
                if (parent.IsRed)
                {
                    parent.ColorBlack();
                    sibling.Recolor();
                }

                if (sibling.IsLeftChild)
                {
                    parent.RotateRight2();
                }
                else
                {
                    parent.RotateLeft2();
                }
            }
            else if (sibling.InnerChild?.IsRed ?? false)
            {
                sibling.Recolor();
                sibling.InnerChild.Recolor();
                if (sibling.IsLeftChild)
                {
                    sibling.RotateLeft2();
                }
                else
                {
                    sibling.RotateRight2();
                }

                ret = FixDoubleBlack();
            }
            else
            {
                sibling.Recolor();
                if (parent.IsRed)
                {
                    parent.Recolor();
                }
                else
                {
                    ret = parent.FixDoubleBlack();
                }
            }

            return ret;
        }

        public void Substitute()
        {
            Parent = GrandParent;

            ColorBlack();
        }
    }
}
