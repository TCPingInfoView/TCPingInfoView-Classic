using System.Collections;
using System.Collections.Generic;

namespace TCPingInfoView
{
	public class ConcurrentList<T> : IList<T>
	{
		private readonly List<T> m_list = new List<T>();

		private readonly object sync = new object();

		public T this[int index]
		{
			get
			{
				lock (sync)
					return m_list[index];
			}

			set
			{
				lock (sync)
					m_list[index] = value;
			}
		}

		public int Count
		{
			get
			{
				lock (sync)
					return m_list.Count;
			}
		}

		public bool IsReadOnly => false;

		public void Add(T item)
		{
			lock (sync) m_list.Add(item);
		}

		public void Clear()
		{
			lock (sync) m_list.Clear();
		}

		public bool Contains(T item)
		{
			lock (sync) return m_list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			lock (sync) m_list.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			lock (sync)
				return m_list.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			lock (sync) return m_list.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			lock (sync) m_list.Insert(index, item);
		}

		public bool Remove(T item)
		{
			lock (sync) return m_list.Remove(item);
		}

		public void RemoveAt(int index)
		{
			lock (sync) m_list.RemoveAt(index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			lock (sync)
				return m_list.GetEnumerator();
		}
	}
}
