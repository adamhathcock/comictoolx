using System;
using System.Collections.Generic;

namespace comictoolx
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T> (this IEnumerable<T> items, Action<T> action)
		{
			foreach (T item in items)
			{
				action (item);
			}
		}
	}
}

