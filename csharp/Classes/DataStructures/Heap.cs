using CodingPlayground.Utility;

namespace CodingPlayground
{
    public class Heap : Collection<int>
    {
        public Heap() : this(0) { }
        public Heap(int capacity)
        {
            if (capacity < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            items = new int[capacity];
        }

        public int Peek() => FirstItem;

        public int Poll()
        {
            var item = FirstItem;
            FirstItem = LastItem;
            Count--;
            HeapifyDown();
            return item;
        }

        public void Add(int item)
        {
            AddToEnd(item);
            HeapifyUp();
        }

        private void HeapifyUp()
        {
            int index = Count - 1;
            while (HasParent(index) && Parent(index) > items[index])
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

        private void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                int smallerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && RightChild(index) < LeftChild(index))
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (items[index] < items[smallerChildIndex])
                {
                    break;
                }
                else
                {
                    Swap(index, smallerChildIndex);
                }
                index = smallerChildIndex;
            }
        }

        private int GetLeftChildIndex(int index) => 2 * index + 1;
        private int GetRightChildIndex(int index) => 2 * index + 2;
        private int GetParentIndex(int index) => (index - 1) / 2;
        private bool HasLeftChild(int index) => GetLeftChildIndex(index) < Count;
        private bool HasRightChild(int index) => GetRightChildIndex(index) < Count;
        private bool HasParent(int index) => GetParentIndex(index) >= 0;
        private int LeftChild(int index) => items[GetLeftChildIndex(index)];
        private int RightChild(int index) => items[GetRightChildIndex(index)];
        private int Parent(int index) => items[GetParentIndex(index)];

    }
}