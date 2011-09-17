using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace QuakeLiveAnalyzer.Model
{
	public static class EncoderToolkit
	{
		public static string EncodeToBase64(byte[] buffer)
		{
			return Convert.ToBase64String(buffer);
		}

		public static string EncodeToBase64(string str)
		{
			byte[] encbuff = Encoding.UTF8.GetBytes(str);

			return Convert.ToBase64String(encbuff);
		}

		public static string DecodeFromBase64(string str)
		{
			byte[] decbuff;

			DecodeFromBase64(str, out decbuff);

			return Encoding.UTF8.GetString(decbuff);
		}

		public static void DecodeFromBase64(string str, out byte[] output)
		{
			output = Convert.FromBase64String(str);
		}

		public static byte[] EncodeToGZip(string content)
		{
			using (MemoryStream output = new MemoryStream())
			{
				byte[] buffer = Encoding.UTF8.GetBytes(content);

				using (GZipStream gzipStream = new GZipStream(output, CompressionMode.Compress))
				{
					gzipStream.Write(buffer, 0, buffer.Length);
				}
				
				return output.ToArray();
			}
		}

		internal static byte[] DecodeFromGZip(byte[] buf)
		{
			using (MemoryStream inputStream = new MemoryStream(buf))
			{
				return DecodeFromGZip(inputStream);
			}
		}

		public static byte[] DecodeFromGZip(Stream inputStream)
		{
			using (MemoryStream output = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
				{
					Pump(gzipStream, output);

					return output.ToArray();
				}
			}
		}

		private static void Pump(Stream input, Stream output)
		{
			int length;
			byte[] bytes = new byte[4096];
			
			while ((length = input.Read(bytes, 0, bytes.Length)) > 0)
			{
				output.Write(bytes, 0, length);
			}
		}
	}
}