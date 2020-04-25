using System;

namespace DataStructures
{
    public class LinkedList<T>
    {
        private Node head = null;

        public LinkedList() { }

        public LinkedList(params T[] data)
        {
            Add(data);
        }

        public T Head
        {
            get => head.data;
            set => head.data = value;
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
                return node.data;
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
                    throw new ArgumentOutOfRangeException();
                }

                var node = head;
                int counter = 0;
                while (counter != index && node != null)
                {
                    node = node.next;
                    counter++;
                }

                return node.data;
            }
        }

        public void Add(params T[] data)
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
                System.Console.WriteLine(node.data);
                node = node.next;
            }
        }

        public class Node
        {
            public T data;
            public Node next;

            public Node(T data)
            {
                this.data = data;
                this.next = null;
            }
        }

    }
}