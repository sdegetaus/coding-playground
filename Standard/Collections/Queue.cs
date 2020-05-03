namespace ConsoleGraphics.Collections
{
    public class Queue<T> : Collection<T>
    {
        public Queue() : base() { }

        public Queue(int capacity) : base(capacity) { }

        public Queue(params T[] items) : base(items) { }

        public void Enqueue(T item)
        {
            AddToEnd(item);
        }

        public T Dequeue()
        {
            var item = items[0];
            ShiftLeft(0);
            Count--;
            return item;
        }

        public T Peek()
        {
            if (items.Length == 0) throw new System.NullReferenceException();
            return items[0];
        }

    }
}