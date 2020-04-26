﻿using System;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {

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

        private static void TryQueue()
        {
            var queue = new Queue<int>();
            for (int i = 1; i <= 10; i++)
            {
                queue.Enqueue(i);
            }

            Console.WriteLine(queue.First);
            Console.WriteLine(queue.Last);

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine(queue.Dequeue());
            }
            queue.Debug();
        }

    }
}
