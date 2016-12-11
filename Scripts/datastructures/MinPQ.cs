using System;
using System.Collections;
using System.Collections.Generic;

namespace PointAndClick.DataStructure {
	
	public class MinPQ<T> : IEnumerable<T> {

		private T[] items;
		private int N;
		private IComparer<T> comparator;

		public MinPQ(int capacity) {
			this.items = new T[capacity + 1];
			this.N = 0;
		}

		public MinPQ() : this(1) {}

		public MinPQ(int capacity, IComparer<T> comparator) : this(capacity) {
			this.comparator = comparator;
		}
		
		public MinPQ(IComparer<T> comparator) : this(1, comparator) {}

		public MinPQ(T[] items) {
			this.N = items.Length;
			this.items = new T[this.N + 1];
			for (int i = 1; i <= N; i++)
				this.items[i+1] = items[i];
			for (int k = (this.N / 2); k >= 1; k--)
				sink(k);
		}

		public bool isEmpty() {
			return this.N == 0;
		}
		
		public int size() {
			Console.WriteLine ("size="+this.N);
			return this.N;
		}

		public T min() {
			if (isEmpty()) throw new Exception("PQ is empty");
			return this.items[1];
		}

		private void resize(int capacity) {
			if (capacity < N) {
				throw new Exception("Capacity of " +  capacity + " is smaller than queue size.");
			}

			T[] temp = new T[capacity];
			for (int i = 1; i <= N; i++) 
				temp[i] = this.items[i];
			this.items = temp;
		}

		public void insert(T x) {
			// double size of array if necessary
			if (N == this.items.Length - 1) resize(2 * this.items.Length);
			
			// add x, and percolate it up to maintain heap invariant
			this.items[++N] = x;
			swim(N);
		}

		public T delMin() {
			if (isEmpty()) 
				throw new Exception("Priority queue underflow");
			exch(1, N);
			T min = this.items[N--];
			sink(1);
			this.items[N+1] = default (T);         // avoid loitering and help with garbage collection

			if ((N > 0) && (N == (this.items.Length - 1) / 4)) 
				resize(this.items.Length  / 2);

			return min;
		}


		private void swim(int k) {
			while (k > 1 && greater(k/2, k)) {
				exch(k, k/2);
				k = k/2;
			}
		}
		
		private void sink(int k) {
			while (2*k <= N) {
				int j = 2*k;
				if (j < N && greater(j, j+1)) 
					j++;
				if (!greater(k, j)) 
					break;
				exch(k, j);
				k = j;
			}
		}

		public bool contains (T o) {
			return indexOf (o) != -1;
		}

		private int indexOf (T o) {
			if (o != null) {
				for (int i = 1; i <= N; i++) {
					if (o.Equals((T) this.items[i])) {
						return i;
					}
				}
			}
			return -1;
		}

		private bool greater(int i, int j) {
			if (comparator == null) {
				return ((IComparable<T>) this.items[i]).CompareTo(this.items[j]) > 0;
			} else {
				return comparator.Compare(this.items[i], this.items[j]) > 0;
			}
		}
		
		private void exch(int i, int j) {
			T swap = this.items[i];
			this.items[i] = this.items[j];
			this.items[j] = swap;
		}
		
		// is pq[1..N] a min heap?
		private bool isMinHeap() {
			return isMinHeap(1);
		}

		// is subtree of pq[1..N] rooted at k a min heap?
		private bool isMinHeap(int k) {
			if (k > N) 
				return true;

			int left = 2*k, right = 2*k + 1;
			if (left  <= N && greater(k, left))  
				return false;
			if (right <= N && greater(k, right)) 
				return false;

			return isMinHeap(left) && isMinHeap(right);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new MinPQEnumerator(this.items, this.comparator);
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// Iterator for MinPQ
		public class MinPQEnumerator : IEnumerator<T> {
			// create a new pq
			private MinPQ<T> copy;
			// Enumerators are positioned before the first element
			// until the first MoveNext() call.
			int position = -1;
			
			public MinPQEnumerator(T[] list, IComparer<T> comparator) {
				Console.WriteLine ("MinPQEnumerator(T[] " + list.Length + ", IComparer<T> " + comparator);
				if (comparator == null) 
					copy = new MinPQ<T>(list.Length);
				else                    
					copy = new MinPQ<T>(list.Length, comparator);
				for (int i = 1; i < list.Length; i++) {
					if (list[i] != null)
						copy.insert(list[i]);
				}
			}
			
			public bool MoveNext() {
				position++;
				return !copy.isEmpty();
			}
			
			public void Reset() {
				position = -1;
			}
			
			object IEnumerator.Current {
				get
				{
					return Current;
				}
			}
			
			public T Current
			{
				get
				{
					try
					{
						return copy.delMin();
					}
					catch (IndexOutOfRangeException)
					{
						throw new InvalidOperationException();
					}
				}
			}

			void IDisposable.Dispose() {
			}
		}
	}
}