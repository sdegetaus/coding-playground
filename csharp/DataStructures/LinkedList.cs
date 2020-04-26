using CodingPlayground.Utilities;

namespace CodingPlayground
{
    public class LinkedList<T>
    {
        private Node head = null;

        public LinkedList() { }

        public LinkedList(params T[] data)
        {
            Append(data);
        }

        public T Head
        {
            get => head.value;
            set => head.value = value;
        }

        public T Tail
        {
            get
            {
                var node = head;
                while (node.next != null)
                {
                    node = node.next;
                }
                return node.value;
            }
        }

        public int Length
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

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                var node = head;
                int counter = 0;
                while (counter != index && node != null)
                {
                    node = node.next;
                    counter++;
                }

                return node.value;
            }
        }

        public void Append(params T[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (head == null)
                {
                    head = new Node(data[i]);
                    continue;
                }

                var node = head;

                while (node.next != null)
                {
                    node = node.next;
                }
                node.next = new Node(data[i]);
            }
        }

        public void Prepend(T data)
        {
            var newHead = new Node(data);
            newHead.next = head;
            head = newHead;
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

        public void Debug()
        {
            var node = head;
            while (node != null)
            {
                System.Console.WriteLine(node.value);
                node = node.next;
            }
        }

        public class Node : Node<T>
        {
            public Node next;

            public Node(T data)
            {
                this.value = data;
                this.next = null;
            }
        }

    }
}