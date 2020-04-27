namespace CodingPlayground
{
    public class BinarySearchTree
    {
        public BSTNode root { get; private set; }

        public BinarySearchTree(int value)
        {
            root = new BSTNode(value);
        }

        public static void Traverse(BSTNode node)
        {
            if (node.left != null)
            {
                Traverse(node.left);
            }

            System.Console.WriteLine(node.value);

            if (node.right != null)
            {
                Traverse(node.right);
            }
        }

        public void Debug()
        {
            Traverse(root);
        }

        public class BSTNode : Node<int>
        {
            public BSTNode left, right;

            public BSTNode(int value)
            {
                this.value = value;
                left = right = null;
            }

            public void Insert(int value)
            {
                if (value <= this.value)
                {
                    if (left == null)
                    {
                        left = new BSTNode(value);
                    }
                    else
                    {
                        left.Insert(value);
                    }
                }
                else
                {
                    if (right == null)
                    {
                        right = new BSTNode(value);
                    }
                    else
                    {
                        right.Insert(value);
                    }
                }
            }

            public bool Contains(int value)
            {
                if (value == this.value)
                {
                    return true;
                }
                else if (value < this.value)
                {
                    if (left == null)
                    {
                        return false;
                    }
                    else
                    {
                        return left.Contains(value);
                    }
                }
                else
                {
                    if (right == null)
                    {
                        return false;
                    }
                    else
                    {
                        return right.Contains(value);
                    }

                }
            }

        }

    }
}