namespace CodingPlayground
{
    public class Stack<T> : Collection<T>
    {
        public Stack() : base() { }

        public Stack(int capacity) : base(capacity) { }

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