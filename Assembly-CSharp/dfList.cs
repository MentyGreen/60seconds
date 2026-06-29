using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000069 RID: 105
public class dfList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IDisposable, IPoolable
{
	// Token: 0x0600076D RID: 1901 RVA: 0x00020830 File Offset: 0x0001EA30
	public static void ClearPool()
	{
		Queue<object> obj = dfList<T>.pool;
		lock (obj)
		{
			dfList<T>.pool.Clear();
			dfList<T>.pool.TrimExcess();
		}
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00020880 File Offset: 0x0001EA80
	public static dfList<T> Obtain()
	{
		Queue<object> obj = dfList<T>.pool;
		dfList<T> result;
		lock (obj)
		{
			if (dfList<T>.pool.Count == 0)
			{
				result = new dfList<T>();
			}
			else
			{
				result = (dfList<T>)dfList<T>.pool.Dequeue();
			}
		}
		return result;
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x000208E0 File Offset: 0x0001EAE0
	public static dfList<T> Obtain(int capacity)
	{
		dfList<T> dfList = dfList<T>.Obtain();
		dfList.EnsureCapacity(capacity);
		return dfList;
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x000208F0 File Offset: 0x0001EAF0
	public void ReleaseItems()
	{
		if (!this.isElementTypePoolable)
		{
			throw new InvalidOperationException(string.Format("Element type {0} does not implement the {1} interface", typeof(T).Name, typeof(IPoolable).Name));
		}
		for (int i = 0; i < this.count; i++)
		{
			(this.items[i] as IPoolable).Release();
		}
		this.Clear();
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x00020968 File Offset: 0x0001EB68
	public void Release()
	{
		Queue<object> obj = dfList<T>.pool;
		lock (obj)
		{
			if (this.autoReleaseItems && this.isElementTypePoolable)
			{
				this.autoReleaseItems = false;
				this.ReleaseItems();
			}
			else
			{
				this.Clear();
			}
			dfList<T>.pool.Enqueue(this);
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x000209D4 File Offset: 0x0001EBD4
	internal dfList()
	{
		this.isElementTypeValueType = typeof(T).IsValueType;
		this.isElementTypePoolable = typeof(IPoolable).IsAssignableFrom(typeof(T));
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00020A2B File Offset: 0x0001EC2B
	internal dfList(IList<T> listToClone) : this()
	{
		this.AddRange(listToClone);
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00020A3A File Offset: 0x0001EC3A
	internal dfList(int capacity) : this()
	{
		this.EnsureCapacity(capacity);
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06000775 RID: 1909 RVA: 0x00020A49 File Offset: 0x0001EC49
	// (set) Token: 0x06000776 RID: 1910 RVA: 0x00020A51 File Offset: 0x0001EC51
	public bool AutoReleaseItems
	{
		get
		{
			return this.autoReleaseItems;
		}
		set
		{
			this.autoReleaseItems = value;
		}
	}

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06000777 RID: 1911 RVA: 0x00020A5A File Offset: 0x0001EC5A
	public int Count
	{
		get
		{
			return this.count;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06000778 RID: 1912 RVA: 0x00020A62 File Offset: 0x0001EC62
	internal int Capacity
	{
		get
		{
			return this.items.Length;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06000779 RID: 1913 RVA: 0x00020A6C File Offset: 0x0001EC6C
	public bool IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170001D3 RID: 467
	public T this[int index]
	{
		get
		{
			if (index < 0 || index > this.count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.items[index];
		}
		set
		{
			if (index < 0 || index > this.count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.items[index] = value;
		}
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x0600077C RID: 1916 RVA: 0x00020AB6 File Offset: 0x0001ECB6
	internal T[] Items
	{
		get
		{
			return this.items;
		}
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00020AC0 File Offset: 0x0001ECC0
	public void Enqueue(T item)
	{
		T[] obj = this.items;
		lock (obj)
		{
			this.Add(item);
		}
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00020B04 File Offset: 0x0001ED04
	public T Dequeue()
	{
		T[] obj = this.items;
		T result;
		lock (obj)
		{
			if (this.count == 0)
			{
				throw new IndexOutOfRangeException();
			}
			T t = this.items[0];
			this.RemoveAt(0);
			result = t;
		}
		return result;
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x00020B64 File Offset: 0x0001ED64
	public T Pop()
	{
		T[] obj = this.items;
		T t2;
		lock (obj)
		{
			if (this.count == 0)
			{
				throw new IndexOutOfRangeException();
			}
			T t = this.items[this.count - 1];
			T[] array = this.items;
			int num = this.count - 1;
			t2 = default(T);
			array[num] = t2;
			this.count--;
			t2 = t;
		}
		return t2;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x00020BEC File Offset: 0x0001EDEC
	public dfList<T> Clone()
	{
		dfList<T> dfList = dfList<T>.Obtain(this.count);
		Array.Copy(this.items, 0, dfList.items, 0, this.count);
		dfList.count = this.count;
		return dfList;
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00020C2B File Offset: 0x0001EE2B
	public void Reverse()
	{
		Array.Reverse(this.items, 0, this.count);
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00020C3F File Offset: 0x0001EE3F
	public void Sort()
	{
		Array.Sort<T>(this.items, 0, this.count, null);
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00020C54 File Offset: 0x0001EE54
	public void Sort(IComparer<T> comparer)
	{
		Array.Sort<T>(this.items, 0, this.count, comparer);
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x00020C6C File Offset: 0x0001EE6C
	public void Sort(Comparison<T> comparison)
	{
		if (comparison == null)
		{
			throw new ArgumentNullException("comparison");
		}
		if (this.count > 0)
		{
			using (dfList<T>.FunctorComparer functorComparer = dfList<T>.FunctorComparer.Obtain(comparison))
			{
				Array.Sort<T>(this.items, 0, this.count, functorComparer);
			}
		}
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00020CC8 File Offset: 0x0001EEC8
	public void EnsureCapacity(int Size)
	{
		if (this.items.Length < Size)
		{
			int newSize = Size / 128 * 128 + 128;
			Array.Resize<T>(ref this.items, newSize);
		}
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x00020D00 File Offset: 0x0001EF00
	public void AddRange(dfList<T> list)
	{
		int num = list.count;
		this.EnsureCapacity(this.count + num);
		Array.Copy(list.items, 0, this.items, this.count, num);
		this.count += num;
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x00020D4C File Offset: 0x0001EF4C
	public void AddRange(IList<T> list)
	{
		int num = list.Count;
		this.EnsureCapacity(this.count + num);
		for (int i = 0; i < num; i++)
		{
			T[] array = this.items;
			int num2 = this.count;
			this.count = num2 + 1;
			array[num2] = list[i];
		}
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00020DA0 File Offset: 0x0001EFA0
	public void AddRange(T[] list)
	{
		int num = list.Length;
		this.EnsureCapacity(this.count + num);
		Array.Copy(list, 0, this.items, this.count, num);
		this.count += num;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00020DE1 File Offset: 0x0001EFE1
	public int IndexOf(T item)
	{
		return Array.IndexOf<T>(this.items, item, 0, this.count);
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x00020DF8 File Offset: 0x0001EFF8
	public void Insert(int index, T item)
	{
		this.EnsureCapacity(this.count + 1);
		if (index < this.count)
		{
			Array.Copy(this.items, index, this.items, index + 1, this.count - index);
		}
		this.items[index] = item;
		this.count++;
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00020E54 File Offset: 0x0001F054
	public void InsertRange(int index, T[] array)
	{
		if (array == null)
		{
			throw new ArgumentNullException("items");
		}
		if (index < 0 || index > this.count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		this.EnsureCapacity(this.count + array.Length);
		if (index < this.count)
		{
			Array.Copy(this.items, index, this.items, index + array.Length, this.count - index);
		}
		array.CopyTo(this.items, index);
		this.count += array.Length;
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00020EDC File Offset: 0x0001F0DC
	public void InsertRange(int index, dfList<T> list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("items");
		}
		if (index < 0 || index > this.count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		this.EnsureCapacity(this.count + list.count);
		if (index < this.count)
		{
			Array.Copy(this.items, index, this.items, index + list.count, this.count - index);
		}
		Array.Copy(list.items, 0, this.items, index, list.count);
		this.count += list.count;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00020F7C File Offset: 0x0001F17C
	public void RemoveAll(Predicate<T> predicate)
	{
		int i = 0;
		while (i < this.count)
		{
			if (predicate(this.items[i]))
			{
				this.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00020FB8 File Offset: 0x0001F1B8
	public void RemoveAt(int index)
	{
		if (index >= this.count)
		{
			throw new ArgumentOutOfRangeException();
		}
		this.count--;
		if (index < this.count)
		{
			Array.Copy(this.items, index + 1, this.items, index, this.count - index);
		}
		this.items[this.count] = default(T);
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x00021024 File Offset: 0x0001F224
	public void RemoveRange(int index, int length)
	{
		if (index < 0 || length < 0 || this.count - index < length)
		{
			throw new ArgumentOutOfRangeException();
		}
		if (this.count > 0)
		{
			this.count -= length;
			if (index < this.count)
			{
				Array.Copy(this.items, index + length, this.items, index, this.count - index);
			}
			Array.Clear(this.items, this.count, length);
		}
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0002109C File Offset: 0x0001F29C
	public void Add(T item)
	{
		this.EnsureCapacity(this.count + 1);
		T[] array = this.items;
		int num = this.count;
		this.count = num + 1;
		array[num] = item;
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x000210D4 File Offset: 0x0001F2D4
	public void Add(T item0, T item1)
	{
		this.EnsureCapacity(this.count + 2);
		T[] array = this.items;
		int num = this.count;
		this.count = num + 1;
		array[num] = item0;
		T[] array2 = this.items;
		num = this.count;
		this.count = num + 1;
		array2[num] = item1;
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0002112C File Offset: 0x0001F32C
	public void Add(T item0, T item1, T item2)
	{
		this.EnsureCapacity(this.count + 3);
		T[] array = this.items;
		int num = this.count;
		this.count = num + 1;
		array[num] = item0;
		T[] array2 = this.items;
		num = this.count;
		this.count = num + 1;
		array2[num] = item1;
		T[] array3 = this.items;
		num = this.count;
		this.count = num + 1;
		array3[num] = item2;
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0002119E File Offset: 0x0001F39E
	public void Clear()
	{
		if (!this.isElementTypeValueType)
		{
			Array.Clear(this.items, 0, this.items.Length);
		}
		this.count = 0;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x000211C3 File Offset: 0x0001F3C3
	public void TrimExcess()
	{
		Array.Resize<T>(ref this.items, this.count);
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x000211D8 File Offset: 0x0001F3D8
	public bool Contains(T item)
	{
		if (item == null)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i] == null)
				{
					return true;
				}
			}
			return false;
		}
		EqualityComparer<T> @default = EqualityComparer<T>.Default;
		for (int j = 0; j < this.count; j++)
		{
			if (@default.Equals(this.items[j], item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00021244 File Offset: 0x0001F444
	public void CopyTo(T[] array)
	{
		this.CopyTo(array, 0);
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0002124E File Offset: 0x0001F44E
	public void CopyTo(T[] array, int arrayIndex)
	{
		Array.Copy(this.items, 0, array, arrayIndex, this.count);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00021264 File Offset: 0x0001F464
	public void CopyTo(int sourceIndex, T[] dest, int destIndex, int length)
	{
		if (sourceIndex + length > this.count)
		{
			throw new IndexOutOfRangeException("sourceIndex");
		}
		if (dest == null)
		{
			throw new ArgumentNullException("dest");
		}
		if (destIndex + length > dest.Length)
		{
			throw new IndexOutOfRangeException("destIndex");
		}
		Array.Copy(this.items, sourceIndex, dest, destIndex, length);
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x000212BC File Offset: 0x0001F4BC
	public bool Remove(T item)
	{
		int num = this.IndexOf(item);
		if (num == -1)
		{
			return false;
		}
		this.RemoveAt(num);
		return true;
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x000212DF File Offset: 0x0001F4DF
	public List<T> ToList()
	{
		List<T> list = new List<T>(this.count);
		list.AddRange(this.ToArray());
		return list;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x000212F8 File Offset: 0x0001F4F8
	public T[] ToArray()
	{
		T[] array = new T[this.count];
		Array.Copy(this.items, 0, array, 0, this.count);
		return array;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00021328 File Offset: 0x0001F528
	public T[] ToArray(int index, int length)
	{
		T[] array = new T[this.count];
		if (this.count > 0)
		{
			this.CopyTo(index, array, 0, length);
		}
		return array;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00021358 File Offset: 0x0001F558
	public dfList<T> GetRange(int index, int length)
	{
		dfList<T> dfList = dfList<T>.Obtain(length);
		this.CopyTo(0, dfList.items, index, length);
		return dfList;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0002137C File Offset: 0x0001F57C
	public bool Any(Func<T, bool> predicate)
	{
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x000213B1 File Offset: 0x0001F5B1
	public T First()
	{
		if (this.count == 0)
		{
			throw new IndexOutOfRangeException();
		}
		return this.items[0];
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x000213D0 File Offset: 0x0001F5D0
	public T FirstOrDefault()
	{
		if (this.count > 0)
		{
			return this.items[0];
		}
		return default(T);
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x000213FC File Offset: 0x0001F5FC
	public T FirstOrDefault(Func<T, bool> predicate)
	{
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				return this.items[i];
			}
		}
		return default(T);
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00021444 File Offset: 0x0001F644
	public T Last()
	{
		if (this.count == 0)
		{
			throw new IndexOutOfRangeException();
		}
		return this.items[this.count - 1];
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00021468 File Offset: 0x0001F668
	public T LastOrDefault()
	{
		if (this.count == 0)
		{
			return default(T);
		}
		return this.items[this.count - 1];
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0002149C File Offset: 0x0001F69C
	public T LastOrDefault(Func<T, bool> predicate)
	{
		T result = default(T);
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				result = this.items[i];
			}
		}
		return result;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x000214E4 File Offset: 0x0001F6E4
	public dfList<T> Where(Func<T, bool> predicate)
	{
		dfList<T> dfList = dfList<T>.Obtain(this.count);
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				dfList.Add(this.items[i]);
			}
		}
		return dfList;
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x00021538 File Offset: 0x0001F738
	public int Matching(Func<T, bool> predicate)
	{
		int num = 0;
		for (int i = 0; i < this.count; i++)
		{
			if (predicate(this.items[i]))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x00021574 File Offset: 0x0001F774
	public dfList<TResult> Select<TResult>(Func<T, TResult> selector)
	{
		dfList<TResult> dfList = dfList<TResult>.Obtain(this.count);
		for (int i = 0; i < this.count; i++)
		{
			dfList.Add(selector(this.items[i]));
		}
		return dfList;
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x000215B7 File Offset: 0x0001F7B7
	public dfList<T> Concat(dfList<T> list)
	{
		dfList<T> dfList = dfList<T>.Obtain(this.count + list.count);
		dfList.AddRange(this);
		dfList.AddRange(list);
		return dfList;
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x000215DC File Offset: 0x0001F7DC
	public dfList<TResult> Convert<TResult>()
	{
		dfList<TResult> dfList = dfList<TResult>.Obtain(this.count);
		for (int i = 0; i < this.count; i++)
		{
			dfList.Add((TResult)((object)System.Convert.ChangeType(this.items[i], typeof(TResult))));
		}
		return dfList;
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00021634 File Offset: 0x0001F834
	public void ForEach(Action<T> action)
	{
		int i = 0;
		while (i < this.Count)
		{
			action(this.items[i++]);
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00021664 File Offset: 0x0001F864
	public IEnumerator<T> GetEnumerator()
	{
		return dfList<T>.PooledEnumerator.Obtain(this, null);
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0002166D File Offset: 0x0001F86D
	IEnumerator IEnumerable.GetEnumerator()
	{
		return dfList<T>.PooledEnumerator.Obtain(this, null);
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x00021676 File Offset: 0x0001F876
	public void Dispose()
	{
		this.Release();
	}

	// Token: 0x04000396 RID: 918
	private static Queue<object> pool = new Queue<object>(1024);

	// Token: 0x04000397 RID: 919
	private const int DEFAULT_CAPACITY = 128;

	// Token: 0x04000398 RID: 920
	private T[] items = new T[128];

	// Token: 0x04000399 RID: 921
	private int count;

	// Token: 0x0400039A RID: 922
	private bool isElementTypeValueType;

	// Token: 0x0400039B RID: 923
	private bool isElementTypePoolable;

	// Token: 0x0400039C RID: 924
	private bool autoReleaseItems;

	// Token: 0x02000376 RID: 886
	private class PooledEnumerator : IEnumerator<T>, IEnumerator, IDisposable, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0007C3D4 File Offset: 0x0007A5D4
		public static dfList<T>.PooledEnumerator Obtain(dfList<T> list, Func<T, bool> predicate)
		{
			dfList<T>.PooledEnumerator pooledEnumerator = (dfList<T>.PooledEnumerator.pool.Count > 0) ? dfList<T>.PooledEnumerator.pool.Dequeue() : new dfList<T>.PooledEnumerator();
			pooledEnumerator.ResetInternal(list, predicate);
			return pooledEnumerator;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0007C3FC File Offset: 0x0007A5FC
		public void Release()
		{
			if (this.isValid)
			{
				this.isValid = false;
				dfList<T>.PooledEnumerator.pool.Enqueue(this);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0007C418 File Offset: 0x0007A618
		public T Current
		{
			get
			{
				if (!this.isValid)
				{
					throw new InvalidOperationException("The enumerator is no longer valid");
				}
				return this.currentValue;
			}
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0007C433 File Offset: 0x0007A633
		private void ResetInternal(dfList<T> list, Func<T, bool> predicate)
		{
			this.isValid = true;
			this.list = list;
			this.predicate = predicate;
			this.currentIndex = 0;
			this.currentValue = default(T);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0007C45D File Offset: 0x0007A65D
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0007C465 File Offset: 0x0007A665
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0007C474 File Offset: 0x0007A674
		public bool MoveNext()
		{
			if (!this.isValid)
			{
				throw new InvalidOperationException("The enumerator is no longer valid");
			}
			while (this.currentIndex < this.list.Count)
			{
				dfList<T> dfList = this.list;
				int num = this.currentIndex;
				this.currentIndex = num + 1;
				T arg = dfList[num];
				if (this.predicate == null || this.predicate(arg))
				{
					this.currentValue = arg;
					return true;
				}
			}
			this.Release();
			this.currentValue = default(T);
			return false;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0007C4F6 File Offset: 0x0007A6F6
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0007C4FD File Offset: 0x0007A6FD
		public IEnumerator<T> GetEnumerator()
		{
			return this;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0007C500 File Offset: 0x0007A700
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x04001663 RID: 5731
		private static Queue<dfList<T>.PooledEnumerator> pool = new Queue<dfList<T>.PooledEnumerator>();

		// Token: 0x04001664 RID: 5732
		private dfList<T> list;

		// Token: 0x04001665 RID: 5733
		private Func<T, bool> predicate;

		// Token: 0x04001666 RID: 5734
		private int currentIndex;

		// Token: 0x04001667 RID: 5735
		private T currentValue;

		// Token: 0x04001668 RID: 5736
		private bool isValid;
	}

	// Token: 0x02000377 RID: 887
	private class FunctorComparer : IComparer<T>, IDisposable
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x0007C517 File Offset: 0x0007A717
		public static dfList<T>.FunctorComparer Obtain(Comparison<T> comparison)
		{
			dfList<T>.FunctorComparer functorComparer = (dfList<T>.FunctorComparer.pool.Count > 0) ? dfList<T>.FunctorComparer.pool.Dequeue() : new dfList<T>.FunctorComparer();
			functorComparer.comparison = comparison;
			return functorComparer;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0007C53E File Offset: 0x0007A73E
		public void Release()
		{
			this.comparison = null;
			if (!dfList<T>.FunctorComparer.pool.Contains(this))
			{
				dfList<T>.FunctorComparer.pool.Enqueue(this);
			}
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0007C55F File Offset: 0x0007A75F
		public int Compare(T x, T y)
		{
			return this.comparison(x, y);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0007C56E File Offset: 0x0007A76E
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x04001669 RID: 5737
		private static Queue<dfList<T>.FunctorComparer> pool = new Queue<dfList<T>.FunctorComparer>();

		// Token: 0x0400166A RID: 5738
		private Comparison<T> comparison;
	}
}
