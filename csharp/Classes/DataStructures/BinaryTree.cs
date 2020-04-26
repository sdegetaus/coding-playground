namespace CodingPlayground
{
    public class BinarySearchTree
    {
        public Node root { get; private set; }

        public BinarySearchTree(int value)
        {
            root = new Node(value);
        }

        public static void Traverse(Node node)
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

        public class Node : Node<int>
        {
            public Node left, right;

            public Node(int value)
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
                        left = new Node(value);
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
                        right = new Node(value);
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