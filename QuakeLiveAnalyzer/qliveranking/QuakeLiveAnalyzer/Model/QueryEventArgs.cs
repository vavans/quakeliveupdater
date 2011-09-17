using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuakeLiveAnalyzer.Model
{
	public class QueryEventArgs<T> : EventArgs where T : QueryableObject
	{
		public T Object { get; private set; }

		public QueryEventArgs(T queryObject)
		{
			Object = queryObject;
		}
	}
}
