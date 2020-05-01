namespace Console3D.Collections
{
    public class LinkedList<T>
    {
        public class LinkedListNode : Node<T>
        {
            public LinkedListNode next;
            public LinkedListNode(T value)
            {
                this.value = value;
                this.next = null;
            }
        }

        private LinkedListNode head = null;

        public LinkedList() { }

        public LinkedList(params T[] data)
        {
            AddLast(data);
        }

        public LinkedListNode First { get => head; }

        public LinkedListNode Last
        {
            get
            {
                var node = head;
                while (node.next != null)
                {
                    node = node.next;
                }
                return node;
            }
        }

        public int Count
        {
            get
            {
                var node = head;

                if (head == null)
                {
                    return 0;
                }

                int counter = head == null ? 0 : 1;
                while (node.next != null)
                {
                    node = node.next;
                    counter++;
                }
                return counter;
            }
        }

        public void AddFirst(params T[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var newHead = new LinkedListNode(data[i]);
                newHead.next = head;
                head = newHead;
            }
        }

        public void AddLast(params T[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (head == null)
                {
                    head = new LinkedListNode(data[i]);
                    continue;
                }

                var node = head;

                while (node.next != null)
                {
                    node = node.next;
                }
                node.next = new LinkedListNode(data[i]);
            }
        }

        public void RemoveFirst()
        {
            if (head == null || head.next == null)
            {
                Clear();
            }
            else
            {
                head = head.next;
            }
        }

        public void RemoveLast()
        {
            if (head == null || head.next == null)
            {
                Clear();
                return;
            }

            var node = head;
            while (node.next != null)
            {
                if (node.next.next == null)
                {
                    node.next = null;
                }
                else
                {
                    node = node.next;
                }
            }
        }

        public void Clear() => head = null;

        public bool Contains(T item) => Find(item).found;

        public SearchResult<T> Find(T item)
        {
            var node = head;
            int count = 0;
            while (node != null)
            {
                if (item.Equals(node.value))
                {
                    return new SearchResult<T>(node.value, count, true);
                }
                node = node.next;
                count++;
            }

            return new SearchResult<T>(item, -1, false);
        }

        public void Debug()
        {
            if (Count == 0)
            {
                System.Console.WriteLine("\nCollection is empty!");
            }
            else
            {
                System.Console.WriteLine("\nItems:");
                var node = head;
                var counter = 0;
                while (node != null)
                {
                    System.Console.WriteLine($"{counter}: {node.value.ToString()}");
                    node = node.next;
                    counter++;
                }
            }
            System.Console.WriteLine($"Count: {Count}\n");
        }

        public override string ToString()
        {
            var node = head;
            string result = "";
            while (node != null)
            {
                result += $"{((result.Length == 0) ? "" : ", ")}({node.value.ToString()})";
                node = node.next;
            }
            return result;
        }

    }
}