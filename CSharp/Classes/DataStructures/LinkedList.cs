namespace CodingPlayground
{
    public class LinkedList<T>
    {
        public class LinkedListNode : Node<T>
        {
            public LinkedListNode next;

            public LinkedListNode(T data)
            {
                this.value = data;
                this.next = null;
            }
        }

        private LinkedListNode head = null;

        public LinkedList() { }

        public LinkedList(params T[] data)
        {
            AddLast(data);
        }

        public LinkedListNode First
        {
            get => head;
        }

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
                int counter = head == null ? 0 : 1;
                while (node.next != null)
                {
                    node = node.next;
                    counter++;
                }
                return counter;
            }
        }

        public void AddFirst(T data)
        {
            var newHead = new LinkedListNode(data);
            newHead.next = head;
            head = newHead;
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

        public void RemoveAt(int index)
        {
            if (index == 0)
            {
                head = head.next;
                return;
            }

            var node = head;
            var prev = head;

            int counter = 0;

            while (counter != index && node != null)
            {
                prev = node;
                node = node.next;
                counter++;
            }
            prev.next = node.next;
        }

        // TO ADD:
        // Contains
        // Remove First
        // Remove Last
        // Clear
        // Find

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
            var node = head;
            while (node != null)
            {
                System.Console.WriteLine(node.value.ToString());
                node = node.next;
            }
        }

    }
}