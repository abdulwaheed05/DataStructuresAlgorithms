using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Heap<T, U> where U : IComparable<U>
    {
        protected List<T> values;
        protected List<U> priorities;

        public Heap()
        {
            this.values = new List<T>();
            this.priorities = new List<U>();
        }

        public virtual void Insert(T data, U priority)
        {
            this.values.Add(data);
            this.priorities.Add(priority);
            this.HeapifyUp(this.priorities.Count-1);
        }

        public virtual T Extract()
        {
            if (this.values.Count == 0) return default(T);

            T item = this.values[0];
            Swap(0, this.values.Count - 1);

            this.values.RemoveAt(this.values.Count - 1);
            this.priorities.RemoveAt(this.priorities.Count - 1);
            this.HeapifyDown(0);

            return item;
        }

        public T GetTop()
        {
            return this.values[0];
        }

        public bool IsEmpty()
        {
            return this.values.Count == 0;
        }

        protected virtual void HeapifyUp(int index)
        {
            int parentIndex = (index + 1) / 2 - 1;
            if (index == 0 || parentIndex < 0) return;

            int comparisonValue = this.Compare(this.priorities[index], (this.priorities[parentIndex]));
            if (comparisonValue > 0)
            {
                Swap(index, parentIndex);
                HeapifyUp(parentIndex);
            }
        }

        protected virtual void HeapifyDown(int index)
        {
            int childIndex = 2 * index + 1;
            int rightChild = 2 * index + 2;

            if (index >= this.priorities.Count || childIndex >= this.priorities.Count) return;
            if (rightChild < this.priorities.Count && this.Compare(this.priorities[rightChild], this.priorities[childIndex]) > 0)
            {
                childIndex = rightChild;
            }

            if (this.Compare(this.priorities[index], this.priorities[childIndex]) < 0)
            {
                Swap(index, childIndex);
                HeapifyDown(childIndex);
            }
        }

        protected virtual int Compare(U first, U second)
        {
            throw new NotImplementedException();
        }

        protected virtual void Swap(int i, int j)
        {
            T tempValue = this.values[i];
            this.values[i] = this.values[j];
            this.values[j] = tempValue;

            U tempPriority = this.priorities[i];
            this.priorities[i] = this.priorities[j];
            this.priorities[j] = tempPriority;
        }
    }

    public class MinHeap<T, U> : Heap<T, U> where U : IComparable<U>
    {
        protected override int Compare(U first, U second)
        {
            return second.CompareTo(first);
        }
    }

    public class MaxHeap<T, U> : Heap<T, U> where U : IComparable<U>
    {
        protected override int Compare(U first, U second)
        {
            return first.CompareTo(second);
        }
    }

    public class MinHeapWithNoDuplicates<T, U> : MinHeap<T, U> where U : IComparable<U>
    {
        Dictionary<T, int> indexLookup;
        public MinHeapWithNoDuplicates()
        {
            this.indexLookup = new Dictionary<T, int>();
        }

        public override void Insert(T data, U priority)
        {
            this.indexLookup.Add(data, this.values.Count);
            base.Insert(data, priority);
        }

        public override T Extract()
        {
            T t = base.Extract();
            this.indexLookup.Remove(t);
            return t;
        }

        protected override void Swap(int i, int j)
        {
            this.indexLookup[this.values[i]] = j;
            this.indexLookup[this.values[j]] = i;
            base.Swap(i, j);
        }

        public void UpdatePriority(T data, U newPriority)
        {
            int index;
            if (!this.indexLookup.TryGetValue(data, out index))
            {
                this.Insert(data, newPriority);
                return;
            }

            U prevPriority = this.priorities[index];
            this.priorities[index] = newPriority;
            if (this.Compare(prevPriority, newPriority) < 0)
            {
                HeapifyUp(index);
            }
            else
            {
                HeapifyDown(index);
            }
        }
    }
}
