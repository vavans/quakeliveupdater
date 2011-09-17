using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace QuakeLiveAnalyzer.Model
{
	public abstract class SyncRequester
	{
		public string Query(string url)
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

				request.Host = @"www.quakelive.com";
				request.UserAgent = @"Mozilla/5.0 (Windows NT 5.1; rv:6.0.2) Gecko/20100101 Firefox/6.0.2";
				request.Accept = @"*/*";
				request.Headers.Add(HttpRequestHeader.AcceptLanguage, @"fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3");
				request.Headers.Add(HttpRequestHeader.AcceptEncoding, @"gzip, deflate");
				request.Headers.Add(HttpRequestHeader.AcceptCharset, @"ISO-8859-1,utf-8;q=0.7,*;q=0.7");
				request.Headers.Add("DNT", "1");
				request.ContentType = @"application/x-www-form-urlencoded";
				request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
				request.Referer = @"http://www.quakelive.com/";
				request.KeepAlive = true;

				HttpWebResponse response = request.GetResponse() as HttpWebResponse;

				if (response == null)
				{
					return null;
				}
				
				Stream stream = response.GetResponseStream();

				if (response.ContentEncoding == "gzip")
				{
					return Encoding.UTF8.GetString(EncoderToolkit.DecodeFromGZip(stream));
				}

				MessageBox.Show("Content is not gzip");

				return GetContent(stream);
			}
			catch
			{
				return null;
			}
		}

		private string GetContent(Stream stream)
		{
			string content;

			using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
			{
				content = reader.ReadToEnd();
			}

			return content;
		}
	}
}
