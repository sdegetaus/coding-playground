namespace DataStructures
{
    public class Queue<T> : Utilities.Collection<T>
    {
        public Queue() : this(0) { }

        public Queue(int capacity)
        {
            if (capacity < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            items = new T[capacity];
        }

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