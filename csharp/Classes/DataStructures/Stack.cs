namespace CodingPlayground
{
    public class Stack<T> : Collection<T>
    {
        public Stack() : this(0) { }

        public Stack(int capacity)
        {
            if (capacity < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            items = new T[capacity];
        }

        public void Push(T item)
        {
            AddToEnd(item);
        }

        public T Pop()
        {
            var item = LastItem;
            Count--;
            return item;
        }

        public T Peek()
        {
            return LastItem;
        }
    }
}