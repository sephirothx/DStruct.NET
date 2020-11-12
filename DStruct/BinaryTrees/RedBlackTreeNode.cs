namespace DStruct.BinaryTrees
{
    class RedBlackTreeNode<T>
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

        public bool IsRed => (_info & 1 << 31) != 0;

        public int LeftChildren
        {
            get => _info & ~(1 << 31);
            set => _info = (_info & 1 << 31) | value;
        }

        public bool IsRoot => Parent == null;

        public bool IsLeftChild => Parent?.Left == this;

        public bool HasTwoRedChildren => (Left?.IsRed ?? false) && (Right?.IsRed ?? false);

        public RedBlackTreeNode<T> GrandParent => Parent?.Parent;

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

            tmp.LeftChildren += LeftChildren + 1;

            return tmp;
        }

        public RedBlackTreeNode<T> RotateRight()
        {
            var tmp = Left;

            Left       = tmp.Right;
            tmp.Right  = this;
            tmp.Parent = Parent;
            Parent     = tmp;

            LeftChildren -= tmp.LeftChildren + 1;

            return tmp;
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

        public void Recolor()
        {
            _info ^= 1 << 31;
        }

        public void ColorBlack()
        {
            _info &= ~(1 << 31);
        }
    }
}
