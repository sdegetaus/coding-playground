using System;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void TryLinkendList()
        {
            var l = new LinkedList<int>(1, 2, 4, 8, 16, 32);
            l.Debug();
            l.Append(64);
            l.Prepend(0);
            l.Debug();
        }
        private static void TryList()
        {
            var list = new List<int>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add(i);
            }
            list.Remove(5);
            list.Debug();
        }
        private static void TryDictionary()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("txt", "notepad.exe");
            dic.Add("bmp", "paint.exe");
            dic.Add("dib", "paint.exe");
            dic.Add("rtf", "wordpad.exe");

            System.Console.WriteLine(dic.ContainsKey("dib"));
            System.Console.WriteLine(dic.ContainsKey("rtf"));
            dic["rtf"] = "hola";
            dic.Remove("dib");
            dic.Remove("txt");

            dic.Debug();
        }
        private static void TryQueue()
        {
            var queue = new Queue<int>();
            for (int i = 1; i <= 10; i++)
            {
                queue.Enqueue(i);
            }

            Console.WriteLine(queue.FirstItem);
            Console.WriteLine(queue.LastItem);

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine(queue.Dequeue());
            }
            queue.Debug();
        }
        private static void TryStack()
        {
            var stack = new Stack<int>();
            for (int i = 10; i <= 30; i += 10)
            {
                stack.Push(i);
            }

            stack.Pop();
            System.Console.WriteLine("\nTop element: " + stack.Peek());

            stack.Debug();
        }
        private static void TryBST()
        {
            var tree = new BinarySearchTree(100);
            tree.root.Insert(5);
            tree.root.Insert(15);
            tree.root.Insert(8);
            tree.Debug();
            System.Console.WriteLine(tree.root.Contains(8));
        }
        private static void TryHeap()
        {
            var heap = new Heap();
            heap.Add(10);
            heap.Add(15);
            heap.Add(20);
            heap.Add(17);
            heap.Add(25);
            heap.Poll();
            heap.Debug();
        }
    }
}
