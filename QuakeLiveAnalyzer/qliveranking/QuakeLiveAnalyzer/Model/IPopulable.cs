using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace QuakeLiveAnalyzer.Model
{
	public interface IPopulable<T> where T : QueryableObject
	{
		ObservableCollection<T> GetObjects();

		void MergeObjects(IEnumerable<T> objectsToAdd);
	}
}
