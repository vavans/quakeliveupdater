#region

using QuakeLiveAnalyzer.QuakeLiveJSon;
using System.ComponentModel;
using System.IO;
using System;
using System.Text;

#endregion

namespace QuakeLiveAnalyzer.Model
{
	public class MatchRequester : ASyncRequester<Match>
	{
		public MatchRequester(IPopulable<Match> matchsContainer, int delay, int maxThreads)
			: base(matchsContainer, delay, maxThreads)
		{ }

		protected override void UpdateContainerAfterQuery(Match match, string response)
		{
			match.LastQueryTimestamp = DateTime.UtcNow.Ticks;

			match.Result = response;

			byte[] buffer = EncoderToolkit.EncodeToGZip(response);

			match.ResultBase64 = EncoderToolkit.EncodeToBase64(buffer);

			match.JSonObject = new MatchStatistics(response);
		}
	}
}
