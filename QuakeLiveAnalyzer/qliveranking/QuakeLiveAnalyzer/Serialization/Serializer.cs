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
	public static class Serializer
	{
		public static string Serialize<T>(List<T> objects)
		{
			byte[] buffer;

			Serialize<T>(objects, out buffer);

			return Encoding.UTF8.GetString(buffer);
		}

		public static void Serialize<T>(List<T> players, out byte[] serializedContent)
		{
			XmlSerializer xs = new XmlSerializer(typeof(List<T>));

			using (MemoryStream stream = new MemoryStream())
			{
				using (StreamWriter writer = new StreamWriter(stream))
				{
					xs.Serialize(writer, players);

					serializedContent = new byte[stream.Length];
					Array.Copy(stream.GetBuffer(), serializedContent, stream.Length);
				}
			}
		}

		public static List<T> DeSerialize<T>(string content)
		{
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
			{
				return DeSerialize<T>(stream);
			}
		}

		public static List<T> DeSerialize<T>(Stream content)
		{
			List<T> value;

			XmlSerializer xs = new XmlSerializer(typeof(List<T>));

			using (StreamReader reader = new StreamReader(content))
			{
				value = xs.Deserialize(reader) as List<T>;
			}

			return value;
		}
	}
}