namespace Console3D.Standard
{
    public class Stack<T> : Collection<T>
    {
        public Stack() : base() { }

        public Stack(int capacity) : base(capacity) { }

        public Stack(params T[] items) : base(items) { }

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