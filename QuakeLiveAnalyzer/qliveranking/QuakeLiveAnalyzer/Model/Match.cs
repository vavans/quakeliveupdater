using System.ComponentModel;
using QuakeLiveAnalyzer.QuakeLiveJSon;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Text;

namespace QuakeLiveAnalyzer.Model
{
	[XmlRoot("Match")]
	[XmlInclude(typeof(QueryableObject))]
	public class Match : QueryableObject
	{
		[XmlAttribute("LastQueryTimestamp")]
		public long LastQueryTimestamp { get; set; }

		[XmlElement("Content")]
		public string ResultBase64 { get; set; }

		[XmlIgnore]
		public string Result { get; set; }

		[XmlIgnore]
		internal MatchStatistics JSonObject { get; set; }

		public Match()
		{ }

		public Match(string id) : base(id)
		{
			Result = null;

			JSonObject = null;
		}

		public override string GetUrlFromId(string id)
		{
			string[] splits = id.Split(new[] { '_' });

			return string.Format(@"http://www.quakelive.com/stats/matchdetails/{0}/{1}/{2}", splits[1], splits[0], splits[2]);
		}

		public override void MergeData(QueryableObject obj)
		{
			Match match = obj as Match;
			if (match == null)
			{
				return;
			}

			if (match.LastQueryTimestamp > LastQueryTimestamp)
			{
				ResultBase64 = match.ResultBase64;
			}

			LastQueryTimestamp = Math.Max(match.LastQueryTimestamp, LastQueryTimestamp);
		}

		public override void UpdateState()
		{
			State = (LastQueryTimestamp == 0) ? Model.State.Waiting : Model.State.Done;

			if (!string.IsNullOrEmpty(ResultBase64))
			{
				byte[] buffer;
				EncoderToolkit.DecodeFromBase64(ResultBase64, out buffer);

				Result = Encoding.UTF8.GetString(EncoderToolkit.DecodeFromGZip(buffer));

				JSonObject = new MatchStatistics(Result);
			}
		}
	}
}