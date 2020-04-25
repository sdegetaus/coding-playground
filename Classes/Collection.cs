namespace DataStructures.Utilities
{
    public abstract class Collection<T>
    {
        protected T[] items;

        public int Capacity { get => items.Length; }

        public int Count { get; protected set; }

        public T First { get => items[0]; }

        public T Last { get => items[Count - 1]; }

        // public virtual T this[int index]
        // {
        //     get =>
        // }
    }
}