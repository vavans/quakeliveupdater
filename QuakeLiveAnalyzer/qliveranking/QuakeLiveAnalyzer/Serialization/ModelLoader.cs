using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Xml.Serialization;
using QuakeLiveAnalyzer.Model;
using System.Collections.Generic;
using System;

namespace QuakeLiveAnalyzer.Serialization
{
	public class ModelLoader<T, Y> where T : IPopulable<Y> where Y : QueryableObject
	{
		private T _requester;

		public ModelLoader(T requester)
		{
			_requester = requester;
		}

		public void Load(string file)
		{
			if (!File.Exists(file))
			{
				return;
			}

			using (FileStream fs = new FileStream(file, FileMode.Open))
			{
				List<Y> list = Serializer.DeSerialize<Y>(fs);

				_requester.MergeObjects(list);
			}
		}

		public void Save(string file)
		{
			using (FileStream fs = new FileStream(file, FileMode.Create))
			{
				byte[] content;

				Serializer.Serialize<Y>(new List<Y>(_requester.GetObjects()), out content);

				fs.Write(content, 0, content.Length);
			}
		}
	}
}