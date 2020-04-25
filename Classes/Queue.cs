namespace DataStructures
{
    public class Queue<T>
    {
        private List<T> list;

        public Queue()
        {
            list = new List<T>();
        }

        public void Enqueue(T item)
        {
            list.Add(item);
        }

        public T Dequeue()
        {
            return null;
        }

        public void Debug()
        {
            list.Debug();
        }
    }
}